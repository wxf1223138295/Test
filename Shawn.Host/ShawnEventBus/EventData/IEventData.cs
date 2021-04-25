using System;
using System.Collections.Generic;
using System.Text;

namespace ShawnEventBus.EventData
{
    public interface IEventData<out TResponse> : IBaseRequest { }
    public interface IBaseRequest { }
}
