using System.Text.Json.Serialization;
using Enquizitive.Features.Quiz.DomainEvents;
using Enquizitive.Infrastructure;

namespace Enquizitive.Common;

public interface IDomainEvent : IEventStoreRecord
{

}