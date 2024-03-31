using Amazon.DynamoDBv2.DataModel;
using Enquizitive.Common;
using Enquizitive.Features.Quiz.DomainEvents;

namespace Enquizitive.Features.Quiz;

[DynamoDBTable("Enquizitive")]
public sealed class Quiz : Aggregate
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
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// When the quiz was last updated.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; } = DateTimeOffset.UtcNow;

    private Quiz(string name)
    {
        Name = name;
    }

    public static Quiz Hydrate(List<IDomainEvent> events)
    {
        var quiz = new Quiz(string.Empty);
        events.ForEach(e => quiz.ApplyEvent(e));
        return quiz;
    }

    public static Quiz Create(string name, string? description)
    {
        var quiz = new Quiz(name)
        {
            Description = description
        };
        quiz.RaiseEvent(new QuizCreated(quiz.Id, quiz.Name, quiz.Description));
        return quiz;
    }

    protected override void ApplyEvent(IDomainEvent @event)
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
        return this;
    }
}
