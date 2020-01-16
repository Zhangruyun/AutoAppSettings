using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAppSettings
{

    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static object GetInstance(this ServiceFactory factory, Type type) => factory(type);
        public static T GetInstance<T>(this ServiceFactory factory) => (T)factory(typeof(T));
        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory) => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
    }
}
