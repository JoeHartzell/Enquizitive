using Enquizitive.Infrastructure;
using MediatR;

namespace Enquizitive.Common;


public interface IDomainEvent : IEventStoreRecordData, INotification
{

}