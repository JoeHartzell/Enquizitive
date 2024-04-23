using Enquizitive.Common;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question;

public static class EventStoreExtensions
{
    public static async Task<Question> GetQuestionById(this EventStore eventStore, Guid id)
        => await eventStore.GetById<Question, IDomainEvent, IEventStoreRecordData>(id, Question.Hydrate);
    
    public static async Task SaveQuestion(this EventStore eventStore, Question question)
        => await eventStore.SaveAggregate<Question, IDomainEvent, IEventStoreRecordData>(question);
}