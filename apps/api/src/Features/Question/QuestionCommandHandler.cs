using Enquizitive.Common;
using Enquizitive.Features.Question.Commands;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question;

public class QuestionCommandHandler(EventStore eventStore) : ICommandHandler<CreateQuestionCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = Question.Create(request.Args);
        await eventStore.SaveAggregate<Question, IDomainEvent, IEventStoreRecordData>(question);

        return question.Id;
    }
}