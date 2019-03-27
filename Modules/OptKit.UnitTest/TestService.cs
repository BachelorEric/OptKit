using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class TestService : RemoteService
    {
        public virtual Model ReturnWithArgumentsMethod(string code, int? qty, int? nullValue)
        {
            return new Model { StringProperty = code, NullableIntProperty = qty };
        }

        public virtual void VoidMethod()
        {
        }

        public virtual void GenericVoidMethod<T>(T arg)
        {

        }

        public virtual T GenericMethod<T>(T arg)
        {
            return arg;
        }
    }
}
