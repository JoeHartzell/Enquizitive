using FluentValidation;

namespace Enquizitive.Features.Quiz.DTOs;

public sealed class UpdateQuizNameRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}

public class UpdateQuizNameValidator : AbstractValidator<UpdateQuizNameRequest>
{
    public UpdateQuizNameValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}