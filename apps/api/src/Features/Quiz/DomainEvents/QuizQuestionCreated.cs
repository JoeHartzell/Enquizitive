using System.Text.Json.Serialization;
using Enquizitive.Common;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizQuestionCreated(Guid Id, int Version, long Timestamp) : IQuizDomainEvent
{

}