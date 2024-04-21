using FluentValidation;

namespace Enquizitive.Features.Quiz.DTOs;

public sealed record UpdateQuizNameRequest(Guid Id, string Name)
{
}

public class UpdateQuizNameValidator : AbstractValidator<UpdateQuizNameRequest>
{
    public UpdateQuizNameValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}