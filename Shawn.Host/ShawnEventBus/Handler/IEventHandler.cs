using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShawnEventBus.EventData;

namespace ShawnEventBus.Handler
{
    public interface IEventHandler<in TRequest, TResponse>
        where TRequest : IEventData<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
