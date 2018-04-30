using Feature.FormsExtentions.Business;
using Feature.FormsExtentions.XDb;
using Feature.FormsExtentions.XDb.Repository;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Feature.FormsExtentions
{
    public class FormsComponentConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFormFieldConverter, FormFieldConverter>();
            serviceCollection.AddSingleton<IXDbService, XDbService>();
            serviceCollection.AddSingleton<IXDbContactRepository, XDbContactRepository>();
        }
    }
}