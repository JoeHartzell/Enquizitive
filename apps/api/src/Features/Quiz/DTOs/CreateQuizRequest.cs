namespace Enquizitive.Features.Quiz.DTOs;

public sealed class CreateQuizRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}