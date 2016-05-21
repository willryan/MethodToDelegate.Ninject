using Ninject.Modules;

namespace MethodToDelegate.Ninject.Test.ReturnDelegate
{
    public class DelegateModule : NinjectModule
    {
        public override void Load()
        {
            this.BindReturnDelegates(typeof(DelegateExample));
        }
    }
}