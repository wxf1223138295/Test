using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShawnEventBus.EventData;

namespace ShawnEventBus.EventBus
{
    public interface IEventBus
    {
        Task<TResponse> Send<TResponse>(IEventData<TResponse> request, CancellationToken cancellationToken = default);
    }
}
