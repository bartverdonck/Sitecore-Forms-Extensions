using Feature.FormsExtensions.Business;
using Feature.FormsExtensions.Business.FileUpload;
using Feature.FormsExtensions.Business.ReCaptcha;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Repository;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Feature.FormsExtensions
{
    public class FormsComponentConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFormFieldConverter, FormFieldConverter>();
            serviceCollection.AddSingleton<IXDbService, XDbService>();
            serviceCollection.AddSingleton<IXDbContactRepository, XDbContactRepository>();
            serviceCollection.AddSingleton<IReCaptchaService, ReCaptchaService>(provider=>new ReCaptchaService(Sitecore.Configuration.Settings.GetSetting("GoogleCaptchaPrivateKey")));
            serviceCollection.AddSingleton<IFileUploadStorageProvider, FileSystemFileUploadStorageProvider>();
        }
    }
}