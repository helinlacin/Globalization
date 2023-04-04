using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Globalization.Services
{    public class SharedResource
    {

    }


    public class LanguageServices
    {
private readonly IStringLocalizer _localizer; 
        public LanguageServices(IStringLocalizerFactory factory )
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(SharedResource),assemblyName.Name);
        }
        public LocalizedString Getkey(string key) {
            return _localizer[key];
        
        }

    }
}
