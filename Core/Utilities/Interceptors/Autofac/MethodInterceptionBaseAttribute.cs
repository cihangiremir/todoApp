using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors.Autofac
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]//Bu Attribute Classın En tepesinde kullanılabileceğini söylüyoruz.
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor//Interceptorın amacı araya girmektir.
    {
        public int Priority { get; set; }//Sıralama için
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
