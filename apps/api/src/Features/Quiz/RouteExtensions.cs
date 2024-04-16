using Enquizitive.Features.Quiz.Commands;
using Enquizitive.Features.Quiz.DTOs;
using Enquizitive.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enquizitive.Features.Quiz;

public static class RouteExtensions
{
    public static WebApplication UseQuizRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/quiz")
            .WithOpenApi()
            .WithTags("Quiz");

        group.MapPost("/create", async (
                [FromBody] CreateQuizRequest request,
                [FromServices] IMediator mediator,
                [FromServices] IValidator<CreateQuizRequest> validator,
                [FromServices] EventStore eventStore
            ) =>
            {
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }
                
                var command = new CreateQuizCommand(request.Name, request.Description);
                var id = await mediator.Send(command);

                return Results.Ok(id);
            })
            .WithName("CreateQuiz");

        group.MapPost("/get", async (
                [FromBody] BatchGetRequest request,
                [FromServices] EventStore store) =>
            {
                var quiz = await store.GetQuizById(request.Id);
                return Results.Ok(quiz); 
            })
            .WithName("GetQuiz");

        group.MapPost("/update-name", async (
                [FromBody] UpdateQuizNameRequest request,
                [FromServices] IMediator mediator,
                [FromServices] IValidator<UpdateQuizNameRequest> validator
            ) =>
            {
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }

                var command = new UpdateQuizNameCommand(
                    Id: request.Id,
                    Name: request.Name);
                await mediator.Send(command);

                return Results.Ok();
            })
            .WithName("UpdateQuizName");
        
        return app;
    }
}