using System.CodeDom;
using System.Linq;
using MethodToDelegate.PartialApplication;
using Ninject.Infrastructure.Language;
using Ninject.Modules;

namespace MethodToDelegate.Ninject.Test.PartialApplication
{
    public class DelegateModule : NinjectModule
    {
        public override void Load()
        {
            this.BindToDelegateMethods(typeof(DelegateExample), 
                typeof(ActionDelegateExample));
        }
    }
}