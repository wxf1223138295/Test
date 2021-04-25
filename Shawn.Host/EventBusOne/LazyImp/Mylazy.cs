using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusOne.LazyImp
{
    public class MyLazy<T> where T : new()
    {
        private bool IsValueCreated { get; set; }

        public T Value
        {
            get
            {
                if (!IsValueCreated)
                {
                    IsValueCreated = true;
                }
                return new T();
            }
        }

        public MyLazy()
        {
            IsValueCreated = false;

        }
    }
}
