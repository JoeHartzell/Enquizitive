using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Enquizitive.Features.Quiz;

public sealed class Quiz : Aggregate<IDomainEvent>
{
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

    public Quiz()
    {
    }


    public static Quiz Hydrate(QuizSnapshot snapshot, List<IDomainEvent> events)
    {
        var ordered = events.OrderBy(x => x.Version);

        var quiz = new Quiz()
        {
            Name = snapshot.Name,
            Description = snapshot.Description,
            CreatedAt = snapshot.CreatedAt,
            UpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(snapshot.Timestamp)
        };

        foreach (var e in ordered)
        {
            quiz.ApplyEvent(e);
        }

        return quiz;
    }

    public static QuizSnapshot TakeSnapshot(Quiz quiz)
    {
        return new QuizSnapshot(quiz.Id, quiz.Version, quiz);
    }

    public static Quiz Create(string name, string? description)
    {
        var quiz = new Quiz();
        quiz.RaiseEvent(new QuizCreated(quiz.Id, name, description));

        return quiz;
    }

    public void UpdateName(string name)
    {
        RaiseEvent(new QuizNameUpdated(Id, NextVersion, name));
    }

    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case QuizCreated e:
                When(e);
                break;
            case QuizNameUpdated e:
                When(e);
                break;
        }

        // Update the version of the aggregate.
        Version = @event.Version;
    }

    private void When(QuizNameUpdated @event)
    {
        Name = @event.Name;
        UpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(@event.Timestamp);
    }

    private void When(QuizCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
        var timestamp = DateTimeOffset.FromUnixTimeMilliseconds(@event.Timestamp);
        CreatedAt = timestamp;
        UpdatedAt = timestamp;
    }

    private void When(QuizSnapshot snapshot)
    {
        Id = snapshot.Id;
        Name = snapshot.State.Name;
        Description = snapshot.State.Description;
        Version = snapshot.Version;
        CreatedAt = snapshot.State.CreatedAt;
        UpdatedAt = snapshot.State.UpdatedAt;
    }
}
