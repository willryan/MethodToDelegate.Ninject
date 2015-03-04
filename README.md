# MethodToDelegate.Ninject
Ninject extensions for [MethodToDelegate](https://github.com/willryan/MethodToDelegate).

This package adds the extension method ToPartiallyAppliedMethodInfo(), which you can call after Bind():

Bind<MyDelegateType>.ToPartiallyAppliedMethodInfo(methodInfoObject);

Most likely you will use this in conjuction with DelegateHelper.GetDelegateTypesAndMethods, as in DelegateModule.cs in the MethodToDelegate.Ninject.Test directory of this project.
