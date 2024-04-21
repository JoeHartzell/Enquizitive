using Enquizitive.Common;
using Enquizitive.Features.Quiz.Commands;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz;

public class QuizCommandHandler(EventStore eventStore) :
    ICommandHandler<CreateQuizCommand, Guid>,
    ICommandHandler<UpdateQuizNameCommand>
{
    public async Task<Guid> Handle(CreateQuizCommand command, CancellationToken cancellationToken)
    {
        var (name, description) = command;
        var quiz = Quiz.Create(name, description);

        await eventStore.SaveQuiz(quiz);
        return quiz.Id;
    }

    public async Task Handle(UpdateQuizNameCommand request, CancellationToken cancellationToken)
    {
        var (id, name) = request;
        var quiz = await eventStore.GetQuizById(id);
        quiz.UpdateName(name);

        await eventStore.SaveQuiz(quiz);
    }
}