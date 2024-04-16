using Enquizitive.Common;

namespace Enquizitive.Features.Quiz.Commands;

public record UpdateQuizNameCommand(Guid Id, string Name) : ICommand
{
   public void Deconstruct(out Guid id, out string name)
   {
      id = Id;
      name = Name;
   } 
}