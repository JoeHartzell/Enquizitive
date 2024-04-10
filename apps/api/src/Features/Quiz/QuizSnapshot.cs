using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz;

public record QuizSnapshot(Guid Id, int Version, long Timestamp, Quiz Data) : Snapshot<Quiz>(Id, Version, Timestamp, Data), IQuizEventStoreRecord
{

}