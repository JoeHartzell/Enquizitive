using System.Security.Cryptography;
using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;

namespace Enquizitive.Features.Quiz;

public sealed class Quiz : Aggregate<IQuizDomainEvent>
{
    /// <inheritdoc cref="Aggregate"/>
    public override string Type => "Quiz";

    /// <summary>
    /// The name of the quiz.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The description of the quiz.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// When the quiz was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// When the quiz was last updated.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// The version of the quiz.
    /// </summary>
    public int Version { get; private set; } = 0;

    public Quiz()
    {
    }

    private Quiz(string name)
    {
        Name = name;
    }

    public static Quiz Hydrate(List<IQuizEventStoreRecord> events)
    {
        var quiz = new Quiz(string.Empty);
        foreach (var e in events)
        {
            switch (e)
            {
                case IQuizDomainEvent domainEvent:
                    quiz.ApplyEvent(domainEvent);
                    break;
                case QuizSnapshot snapshot:
                    quiz.When(snapshot);
                    break;
            }
        }

        return quiz;
    }

    public static Quiz Create(string name, string? description)
    {
        var quiz = new Quiz(name)
        {
            Description = description
        };
        quiz.RaiseEvent(new QuizCreated(quiz.Id, 1, DateTimeOffset.UtcNow.ToUnixTimeSeconds() ,quiz.Name, quiz.Description));
        return quiz;
    }

    protected override void ApplyEvent(IQuizDomainEvent @event)
    {
        _ = @event switch
        {
            QuizCreated e => When(e),
            _ => throw new InvalidOperationException($"Unsupported event {@event.GetType().Name}")
        };
    }

    private Quiz When(QuizCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
        Version = @event.Version;
        var timestamp = DateTimeOffset.FromUnixTimeSeconds(@event.Timestamp);
        CreatedAt = timestamp;
        UpdatedAt = timestamp;
        return this;
    }

    private Quiz When(QuizSnapshot snapshot)
    {
        Id = snapshot.Id;
        Name = snapshot.Data.Name;
        Description = snapshot.Data.Description;
        Version = snapshot.Version;
        CreatedAt = snapshot.Data.CreatedAt;
        UpdatedAt = snapshot.Data.UpdatedAt;
        return this;
    }
}
