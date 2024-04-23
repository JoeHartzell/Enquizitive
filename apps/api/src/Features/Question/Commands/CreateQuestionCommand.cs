using Enquizitive.Common;
using Enquizitive.Features.Question.Args;

namespace Enquizitive.Features.Question.Commands;

public record CreateQuestionCommand(CreateQuestionArgs Args) : ICommand<Guid>
{
}