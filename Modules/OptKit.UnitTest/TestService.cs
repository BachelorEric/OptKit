using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class TestService : RemoteService
    {
        public virtual Model ReturnMethod(string code)
        {
            return new Model { StringProperty = code };
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
