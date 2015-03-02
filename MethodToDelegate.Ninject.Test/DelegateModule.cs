using System.Linq;
using Ninject.Infrastructure.Language;
using Ninject.Modules;

namespace MethodToDelegate.Ninject.Test
{
    public class DelegateModule : NinjectModule
    {
        public override void Load()
        {
            new[]
            {
                typeof (DelegateExample),
                typeof (ActionDelegateExample),
                //typeof (GenericDelegateExample) 
            }
            .SelectMany(DelegateHelper.GetDelegateTypesAndMethods)
            .Map(info =>
            {
                Bind(info.DelegateType).ToPartiallyAppliedMethodInfo(info.MethodInfo);
            });
        }
    }
}