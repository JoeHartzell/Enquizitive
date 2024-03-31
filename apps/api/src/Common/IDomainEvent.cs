using System.Text.Json.Serialization;
using Enquizitive.Features.Quiz.DomainEvents;

namespace Enquizitive.Common;

[JsonDerivedType(typeof(QuizCreated), nameof(QuizCreated))]
[JsonDerivedType(typeof(QuizQuestionCreated), nameof(QuizQuestionCreated))]
public interface IDomainEvent
{

}