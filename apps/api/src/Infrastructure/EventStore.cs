using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Enquizitive.Common;
using MediatR;

namespace Enquizitive.Infrastructure;

public class EventStore(IDynamoDBContext context, IAmazonDynamoDB client, IMediator mediator)
{
    public async Task<TAggregate> GetById<TAggregate, TEvent, TRecordData>(Guid id, Func<List<IDomainEvent>, TAggregate> hydrate)
        where TAggregate : Aggregate<TEvent>
        where TEvent : IDomainEvent, TRecordData
        where TRecordData : IEventStoreRecordData
    { 
        var hashKey = $"{typeof(TAggregate).Name}#{id}";
        var results = await context.QueryAsync<EventStoreRecord<TRecordData>>(hashKey)
            .GetRemainingAsync();

        var events = results
            .Select(x => x.Data)
            .OfType<IDomainEvent>()
            .ToList();
        
        return hydrate(events);
    }
    
    public async Task SaveAggregate<TAggregate, TEvent, TRecordData>(TAggregate aggregate) 
        where TAggregate : Aggregate<TEvent>
        where TEvent : IDomainEvent, TRecordData 
        where TRecordData : IEventStoreRecordData
    {
        var domainEvents = aggregate.Events;
        var domainEventRecords = domainEvents
            .Select(x => new EventStoreRecord<TRecordData>()
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
        }
        
        aggregate.ClearEvents();
    }
}