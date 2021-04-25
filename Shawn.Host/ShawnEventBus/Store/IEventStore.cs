using System;
using System.Collections.Generic;
using System.Text;
using ShawnEventBus.EventData;

namespace ShawnEventBus.Store
{
    public interface IEventStore
    {
        void AddRegister(Type eventData, Type eventHandler);

        Type GetHandlersForEvent(Type t);
    }
}
