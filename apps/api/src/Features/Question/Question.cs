using System.Collections.ObjectModel;
using Enquizitive.Common;
using Enquizitive.Features.Question.Args;
using Enquizitive.Features.Question.DomainEvents;
using FluentValidation;

namespace Enquizitive.Features.Question;

public sealed class Question : Aggregate<IDomainEvent>
{
    private readonly List<AnswerChoice> _answers = [];

    public ReadOnlyCollection<AnswerChoice> Answers => _answers.AsReadOnly();

    public string Text { get; private set; }

    private Question()
    {
    }

    public static Question Hydrate(List<IDomainEvent> events)
    {
        var question = new Question();
        foreach (var e in events)
        {
            question.ApplyEvent(e);
        }

        return question;
    }
    
    public static Question Create(CreateQuestionArgs args)
    {
        var validator = new CreateQuestionArgsValidator();
        validator.ValidateAndThrow(args);

        var question = new Question();
        var @event = new QuestionCreated(
            Id: question.Id,
            Text: args.Text);
        question.RaiseEvent(@event);
        return question;
    } 
    
    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case QuestionCreated e:
                When(e);
                break;
            case QuestionAnswerChoiceTextUpdated e:
                When(e);
                break;
            case QuestionAnswerChoiceCreated e:
                When(e);
                break;
        }

        // Update the version of the aggregate.
        Version = @event.Version;
    }

    public void AddAnswer(CreateAnswerChoiceArgs choiceArgs)
    {
        var validator = new CreateAnswerArgsValidator();
        validator.ValidateAndThrow(choiceArgs);

        var (text, isCorrect, rational) = choiceArgs;
        var @event = new QuestionAnswerChoiceCreated(
            Id: Id,
            Version: NextVersion,
            AnswerId: Guid.NewGuid(),
            Text: text,
            IsCorrect: isCorrect,
            Rational: rational);
        RaiseEvent(@event);
    }

    public void UpdateAnswerText(UpdateAnswerChoiceTextArgs args)
    {
        var validator = new UpdateAnswerTextArgsValidator(_answers);
        validator.ValidateAndThrow(args);

        var (answerId, text) = args;
        var @event = new QuestionAnswerChoiceTextUpdated(
            Id: Id,
            Version: NextVersion,
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            AnswerId: answerId,
            Text: text);
        RaiseEvent(@event);
    }

    private void When(QuestionCreated @event)
    {
        Id = @event.Id;
        Text = @event.Text;
        Version = @event.Version;
    }
    
    private void When(QuestionAnswerChoiceTextUpdated @event)
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
        _answers.Add(new AnswerChoice(@event.AnswerId, @event.Text, @event.IsCorrect, @event.Rational));
    }
}