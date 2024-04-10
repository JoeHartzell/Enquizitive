using System.Text.Json.Serialization;
using Enquizitive.Features.Quiz.DomainEvents;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz;

[JsonDerivedType(typeof(QuizCreated), nameof(QuizCreated))]
[JsonDerivedType(typeof(QuizQuestionCreated), nameof(QuizQuestionCreated))]
[JsonDerivedType(typeof(QuizSnapshot), nameof(QuizSnapshot))]
public interface IQuizEventStoreRecord : IEventStoreRecord
{

}