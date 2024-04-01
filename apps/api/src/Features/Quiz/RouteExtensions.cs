using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;
using Enquizitive.Infrastructure;

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

        group.MapPost("/batch-get", () =>
            {
                var @event = new QuizCreated(Guid.NewGuid(), "Quiz Name", "Quiz Description");
                var @event2 = new QuizQuestionCreated();
                return new List<EventStore<IDomainEvent>>()
                {
                    new()
                    {
                        SortKey = $"Event#1/{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                        Key = $"Quiz#{@event.Id}",
                        Data = @event,
                        Type = nameof(QuizCreated)
                    },
                    new()
                    {
                        SortKey = $"Event#2/{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                        Key = $"Quiz#{@event.Id}",
                        Data = @event2,
                        Type = nameof(QuizQuestionCreated)
                    }
                };
            })
            .WithName("BatchGetQuiz")
            .WithOpenApi()
            .WithTags("quiz");

        return app;
    }
}