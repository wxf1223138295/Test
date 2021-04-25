using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ShawnEventBus.EventData;
using ShawnEventBus.Handler;

namespace ShawnEventBus.Store
{
    public class InMemoryEventStore: IEventStore
    {
        private static readonly object lockobj = new object();

        public  ConcurrentDictionary<Type, Type> _eventObjMapperOneToOne;
        public IWindsorContainer _IocContainer { get; set; }
        public InMemoryEventStore(IWindsorContainer IocContainer)
        {
            _IocContainer = IocContainer;
            _eventObjMapperOneToOne = new ConcurrentDictionary<Type, Type>();
        }
        public bool HasRegisterForEvent(Type eventData)
        {
            return _eventObjMapperOneToOne.ContainsKey(eventData);
        }
        public void AddRegister(Type eventData, Type eventHandler)
        {
            lock (lockobj)
            {
                if (!HasRegisterForEvent(eventData))
                {
                    var handlerInterface = eventHandler.GetInterface("IEventHandler`2");
                    _IocContainer.Register(Component.For(handlerInterface, eventHandler));


                        _eventObjMapperOneToOne.TryAdd(eventData, eventHandler);
                }
            }
        }

        public Type GetHandlersForEvent(Type t)
        {
            if (HasRegisterForEvent(t))
            {
                var result= _eventObjMapperOneToOne[t];

                return result;
            }

            return default;
        }

    }
}
