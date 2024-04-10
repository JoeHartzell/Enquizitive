using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;
using Enquizitive.Features.Quiz.DTOs;
using Enquizitive.Features.Quiz.Validators;
using Enquizitive.Infrastructure;
using FluentValidation;
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

        group.MapPost("/create", async (
            [FromBody] CreateQuizRequest request,
            [FromServices] IValidator<CreateQuizRequest> validator,
            [FromServices] IDynamoDBContext context
            ) =>
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            var quiz = Quiz.Create(request.Name, request.Description);

            var snapshot = new QuizSnapshot(
                quiz.Id,
                quiz.Version,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                quiz
            );
            var domainEvents = quiz.Events
                .Select(x => new EventStore<IQuizEventStoreRecord>()
                {
                    Data = x,
                    Key = $"Quiz#{quiz.Id}",
                    SortKey = $"Event#{x.Version}",
                    Type = x.GetType().Name,
                    Timestamp = x.Timestamp,
                    Version = x.Version,
                });


            var batchWrite = context.CreateBatchWrite<EventStore<IQuizEventStoreRecord>>(new DynamoDBOperationConfig()
            {
            });
            batchWrite.AddPutItems(domainEvents);
            batchWrite.AddPutItem(
                new EventStore<IQuizEventStoreRecord>()
                {
                    Data = snapshot,
                    Key = $"Quiz#{quiz.Id}",
                    SortKey = $"Snapshot#{quiz.Version}",
                    Type = "Snapshot",
                    Timestamp = snapshot.Timestamp,
                    Version = snapshot.Version
                });
            await batchWrite.ExecuteAsync();

            return Results.Created($"/quiz/{quiz.Id}", quiz);
        });

        group.MapPost("/batch-get", async (
                [FromBody] BatchGetRequest request,
                [FromServices] IDynamoDBContext ddb) =>
            {
                var filter = new QueryOperationConfig()
                {
                    KeyExpression = new()
                    {
                        ExpressionStatement = "pk = :pk",
                        ExpressionAttributeValues = new()
                        {
                            [":pk"] = $"Quiz#{request.Id}",
                        }
                    }
                };
                var query = ddb.FromQueryAsync<EventStore<IQuizEventStoreRecord>>(filter);
                var result = await query.GetRemainingAsync();
                var events = result.Select(x => x.Data).ToList();

                return Quiz.Hydrate(events);
            })
            .WithName("BatchGetQuiz")
            .WithOpenApi()
            .WithTags("quiz");

        return app;
    }
}