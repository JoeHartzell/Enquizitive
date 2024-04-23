using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Enquizitive.Common;
using MediatR;

namespace Enquizitive.Infrastructure;

public class EventStore(IDynamoDBContext context, IAmazonDynamoDB client, IMediator mediator)
{
    public async Task<TAggregate> GetById<TAggregate, TEvent, TSnapshot>(Guid id, Func<TSnapshot, List<TEvent>, TAggregate> hydrate)
        where TAggregate : Aggregate<TEvent>
        where TEvent : IDomainEvent
        where TSnapshot : ISnapshot
    {
        var hashKey = $"{typeof(TAggregate).Name}#{id}";
        var config = new QueryOperationConfig()
        {
            IndexName = EventStoreRecord<IEventStoreRecordData>.TimestampIndex,
            Limit = 50,
            KeyExpression = new Expression()
            {
                ExpressionStatement = "pk = :pk",
                ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":pk", new Primitive(hashKey) }
                }
            },
            // descending order
            BackwardSearch = true
        };

        var results = await context.FromQueryAsync<EventStoreRecord<IEventStoreRecordData>>(config)
            .GetRemainingAsync();

        var events = results
            .Select(x => x.Data)
            .OfType<TEvent>()
            .ToList();

        var snapshot = results
            .Select(x => x.Data)
            .OfType<TSnapshot>()
            .First();

        return hydrate(snapshot, events);
    }

    public async Task SaveAggregate<TAggregate, TEvent, TSnapshot>(TAggregate aggregate, Func<TAggregate, TSnapshot> takeSnapshot)
        where TAggregate : Aggregate<TEvent>
        where TEvent : IDomainEvent
        where TSnapshot : ISnapshot
    {
        var domainEvents = aggregate.Events;
        var domainEventRecords = domainEvents
            .Select(x => new EventStoreRecord<IEventStoreRecordData>()
            {
                Data = x,
                Key = $"{typeof(TAggregate).Name}#{aggregate.Id}",
                SortKey = $"Event#{x.Version}",
                Type = x.GetType().Name,
                Timestamp = x.Timestamp,
                Version = x.Version,
            });

        // Could use a transaction here to ensure that all records are saved or none are saved.
        // The reason we don't is the "double" write capacity units required for transactions.

        foreach (var record in domainEventRecords)
        {
            // We have to use the low-level client here because the high-level
            // context doesn't support conditional expressions.
            var document = context.ToDocument(record);
            var item = document.ToAttributeMap();
            var request = new PutItemRequest
            {
                TableName = "Enquizitive",
                Item = item,
                // This checks that an item with the given primary key for the table doesn't exist.
                // For tables that define a composite primary key, this condition applies to the primary key as a whole.
                ConditionExpression = "attribute_not_exists(sk)"
            };

            await client.PutItemAsync(request);
            await mediator.Publish(record.Data);

            // Take a snapshot every 50 events.
            // We also take a snapshot on the first event.
            if (record.Version % 50 == 0 || record.Version == 1)
            {
                var snapshot = takeSnapshot(aggregate);
                var snapshotRecord = new EventStoreRecord<IEventStoreRecordData>()
                {
                    Data = snapshot,
                    Key = $"{typeof(TAggregate).Name}#{aggregate.Id}",
                    SortKey = $"Snapshot#{snapshot.Version}",
                    Type = "Snapshot",
                    Timestamp = snapshot.Timestamp,
                    Version = snapshot.Version
                };

                var snapshotRequest = new PutItemRequest
                {
                    TableName = "Enquizitive",
                    Item = context.ToDocument(snapshotRecord).ToAttributeMap(),
                    ConditionExpression = "attribute_not_exists(sk)"
                };

                await client.PutItemAsync(snapshotRequest);
            }
        }

        aggregate.ClearEvents();
    }
}