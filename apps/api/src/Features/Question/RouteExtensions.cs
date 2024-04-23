using Enquizitive.Features.Question.Args;
using Enquizitive.Features.Question.Commands;
using Enquizitive.Features.Question.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enquizitive.Features.Question;

public static class RouteExtensions
{
    public static WebApplication UseQuestionRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/question")
            .WithOpenApi()
            .WithTags("Question")
            .WithDescription("Endpoints for managing questions");

        group.MapPost("/create", (
            [FromBody] CreateQuestionRequest body,
            [FromServices] IMediator mediator) =>
        {
            var command = new CreateQuestionCommand(new CreateQuestionArgs(body.Text));
            var id = mediator.Send(command);

            return Results.Ok(id);
        });

        return app;
    }
}