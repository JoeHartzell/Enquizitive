using System.Text.Json.Serialization;
using Enquizitive.Features.Question.DomainEvents;
using Enquizitive.Features.Quiz;
using Enquizitive.Features.Quiz.DomainEvents;

namespace Enquizitive.Common;

// Questions
[JsonDerivedType(typeof(QuestionCreated), nameof(QuestionCreated))]
// [JsonDerivedType(typeof(QuestionAnswerChoiceCreated), nameof(QuestionAnswerChoiceCreated))]
// [JsonDerivedType(typeof(QuestionAnswerChoiceRationalUpdated), nameof(QuestionAnswerChoiceRationalUpdated))]
// [JsonDerivedType(typeof(QuestionAnswerChoiceTextUpdated), nameof(QuestionAnswerChoiceTextUpdated))]
// Quizzes
[JsonDerivedType(typeof(QuizCreated), nameof(QuizCreated))]
[JsonDerivedType(typeof(QuizNameUpdated), nameof(QuizNameUpdated))]
[JsonDerivedType(typeof(QuizQuestionCreated), nameof(QuizQuestionCreated))]
[JsonDerivedType(typeof(QuizSnapshot), nameof(QuizSnapshot))]
public interface IEventStoreRecordData
{
   /// <summary>
   /// Aggregate identifier.
   /// </summary>
   Guid Id { get; } 
   
   /// <summary>
   /// Aggregate version.
   /// </summary>
   int Version { get; }
   
   /// <summary>
   /// Timestamp of the record.
   /// </summary>
   long Timestamp { get; }
}