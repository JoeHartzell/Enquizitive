using Enquizitive.Common;

namespace Enquizitive.Features.Quiz.Commands;

/// <summary>
/// Command to create a quiz
/// </summary>
/// <param name="Request"></param>
public record CreateQuizCommand(string Name, string? Description) : ICommand<Guid>
{
    public void Deconstruct(out string name, out string? description)
    {
        name = Name;
        description = Description;
    }
}