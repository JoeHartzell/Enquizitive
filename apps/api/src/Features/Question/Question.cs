using System.Collections.ObjectModel;
using Enquizitive.Common;
using Enquizitive.Features.Question.Args;
using Enquizitive.Features.Question.DomainEvents;
using FluentValidation;

namespace Enquizitive.Features.Question;

public sealed class Question : Aggregate<IDomainEvent>
{
    private readonly List<Answer> _answers = [];

    public ReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    public string Text { get; private set; }
    public int Version { get; private set; }

    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case QuestionAnswerTextUpdated e:
                When(e);
                break;
            case QuestionAnswerChoiceCreated e:
                When(e);
                break;
        }

        // Update the version of the aggregate.
        Version = @event.Version;
    }

    public void AddAnswer(CreateAnswerArgs args)
    {
        var validator = new CreateAnswerArgsValidator();
        validator.ValidateAndThrow(args);

        var (text, isCorrect, rational) = args;
        var @event = new QuestionAnswerChoiceCreated(
            Id: Id,
            Version: NextVersion,
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            AnswerId: Guid.NewGuid(),
            Text: text,
            IsCorrect: isCorrect,
            Rational: rational);
        RaiseEvent(@event);
    }

    public void UpdateAnswerText(UpdateAnswerTextArgs args)
    {
        var validator = new UpdateAnswerTextArgsValidator(_answers);
        validator.ValidateAndThrow(args);

        var (answerId, text) = args;
        var @event = new QuestionAnswerTextUpdated(
            Id: Id,
            Version: NextVersion,
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            AnswerId: answerId,
            Text: text);
        RaiseEvent(@event);
    }

    private int NextVersion => Version + 1;

    private void When(QuestionAnswerTextUpdated @event)
    {
        var index = _answers.FindIndex(x => x.Id == @event.AnswerId);
        var answer = _answers[index];
        _answers[index] = answer with
        {
            Text = @event.Text
        };
    }

    private void When(QuestionAnswerChoiceCreated @event)
    {
        _answers.Add(new Answer(@event.AnswerId, @event.Text, @event.IsCorrect, @event.Rational));
    }
}