using Enquizitive.Common;

namespace Enquizitive.Features.Quiz;

public interface IQuizDomainEvent : IQuizEventStoreRecord, IDomainEvent
{
}