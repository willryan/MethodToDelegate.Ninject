using System;
using System.Linq;
using System.Reflection;
using MethodToDelegate.PartialApplication;
using MethodToDelegate.ReturnDelegate;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace MethodToDelegate.Ninject
{
    public class MethodBindingException : Exception
    {
        public MethodBindingException(string message) : base(message) { }
    }

    public static class NinjectExtensions
    {
        public static IBindingWhenInNamedWithOrOnSyntax<object> ToPartiallyAppliedMethodInfo(
            this IBindingToSyntax<object> binder, MethodInfo methodInfo)
        {
            var betterBinder = binder as BindingBuilder<object>;
            if (betterBinder != null)
            {
                var buildInfo = DelegateHelper.CreateBuildInfo(betterBinder.Binding.Service, methodInfo);
                return binder.ToMethod(ctx =>
                    DelegateHelper.BuildDelegate(buildInfo, (t,attrs) => ctx.Kernel.Get(t)));
            }
            throw new MethodBindingException("cannot determine delegate type to bind");
        }

        public static void BindToDelegateMethods(this IBindingRoot root, params Type[] types)
        {
            types
                .SelectMany(DelegateHelper.GetDelegateTypesAndMethods)
                .Map(BindPartialApplication(root));
        }

        public static void BindReturnDelegates(this IBindingRoot root, params Type[] types)
        {
            types
                .SelectMany(GetReturnDelegates)
                .Map(BindReturnDelegate(root));
        }

        private static Action<DelegateTypeAndMethodInfo> BindPartialApplication(IBindingRoot root) =>
            info => root.Bind(info.DelegateType)
                        .ToPartiallyAppliedMethodInfo(info.MethodInfo);

        private static RegisterableDelegate<IContext>[] GetReturnDelegates(Type t) =>
            ReturnDelegateRegistrar.Register<IContext>(t, (ctx, pi) => 
                ctx.Kernel.Get(pi.ParameterType));
        private static Action<RegisterableDelegate<IContext>> BindReturnDelegate(IBindingRoot root) =>
            delg => root.Bind(delg.MethodInfo.ReturnType)
                        .ToMethod(ctx => delg.Producer(ctx));
    }
}