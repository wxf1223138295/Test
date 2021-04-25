using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Windsor;
using ShawnEventBus.EventData;
using ShawnEventBus.Handler;
using ShawnEventBus.Store;

namespace ShawnEventBus.EventBus
{
    public class EventBus: IEventBus
    {
        public readonly IEventStore _InMemoryEventStore;
        public IWindsorContainer IocContainer { get; private set; }
        public EventBus()
        {
            IocContainer = new WindsorContainer();
            _InMemoryEventStore =new InMemoryEventStore(IocContainer);
           
        }
        /// <summary>
        /// 有返回值的必然 1 对  1
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TResponse> Send<TResponse>(IEventData<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var requestType = request.GetType();
            //获取映射的EventHandler
             var handlerTypes = _InMemoryEventStore.GetHandlersForEvent(requestType);
            //var objInstance =
            //    Activator.CreateInstance(
            //        typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)));
            //var handler = (RequestHandlerWrapper<TResponse>)objInstance;

      



            return Task.FromResult(default(TResponse));
        }
    }
}
