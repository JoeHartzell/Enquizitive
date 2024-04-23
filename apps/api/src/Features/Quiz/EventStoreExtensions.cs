using Enquizitive.Common;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz;

public static class EventStoreExtensions
{
    public static async Task<Quiz> GetQuizById(this EventStore eventStore, Guid id)
        => await eventStore.GetById<Quiz, IDomainEvent, IEventStoreRecordData>(id, Quiz.Hydrate);
    
    public static async Task SaveQuiz(this EventStore eventStore, Quiz quiz)
        => await eventStore.SaveAggregate<Quiz, IDomainEvent, IEventStoreRecordData>(quiz);
}