using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methods = type.GetMethods().Where(t => t.Name == method.Name).ToList();
            foreach (var item in methods)
            {
                var methodAtt = item.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                foreach (var att in methodAtt)
                {
                    classAttributes.Add(att);
                }
            }

            return classAttributes.OrderBy(t => t.Priority).ToArray();
        }
    }
}
