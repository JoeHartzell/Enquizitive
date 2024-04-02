using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;
using Enquizitive.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Enquizitive.Features.Quiz;

public static class RouteExtensions
{
    public static WebApplication UseQuizRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/quiz");

        group.MapPost("/get", () => "Hello, World!")
            .WithName("GetQuiz")
            .WithOpenApi()
            .WithTags("quiz");

        group.MapPost("/batch-get", async ([FromServices] IAmazonDynamoDB ddb) =>
            {
                var context = new DynamoDBContext(ddb);
                var filter = new QueryOperationConfig()
                {
                    KeyExpression = new()
                    {
                        ExpressionStatement = "pk = :pk and begins_with(sk, :sk)",
                        ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                        {
                            { ":pk", new Primitive("Quiz#123") },
                            { ":sk", new Primitive("Event#") }
                        }
                    },
                };
                var query = await context.FromQueryAsync<EventStore<IDomainEvent>>(filter).GetRemainingAsync();
                return query;
                // var item = new EventStore<IDomainEvent>
                // {
                //     Key = "Quiz#123",
                //     SortKey = $"Event#1/{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                //     Data = new QuizCreated(Guid.NewGuid(), "Quiz Name", "Quiz Description"),
                //     Type = nameof(QuizCreated)
                // };
                // await context.SaveAsync(item);
                //
                // return item;

                // var @event = new QuizCreated(Guid.NewGuid(), "Quiz Name", "Quiz Description");
                // var @event2 = new QuizQuestionCreated();
                // return new List<EventStore<IDomainEvent>>()
                // {
                //     new()
                //     {
                //         SortKey = $"Event#1/{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                //         Key = $"Quiz#{@event.Id}",
                //         Data = @event,
                //         Type = nameof(QuizCreated)
                //     },
                //     new()
                //     {
                //         SortKey = $"Event#2/{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                //         Key = $"Quiz#{@event.Id}",
                //         Data = @event2,
                //         Type = nameof(QuizQuestionCreated)
                //     }
                // };
            })
            .WithName("BatchGetQuiz")
            .WithOpenApi()
            .WithTags("quiz");

        return app;
    }
}