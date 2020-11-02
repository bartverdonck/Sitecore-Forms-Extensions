using Feature.FormsExtensions.Fields.ReCaptcha;
using Feature.FormsExtensions.Fields.ValueResolvers;
using Feature.FormsExtensions.SubmitActions.SendEmail;
using Feature.FormsExtensions.SubmitActions.SendEmail.FileAttachment;
using Feature.FormsExtensions.SubmitActions.SendEmail.Tokens;
using Feature.FormsExtensions.ValueProviders;
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
            serviceCollection.AddSingleton<IXDbService, XDbService>();
            serviceCollection.AddSingleton<IXDbContactRepository, XDbContactRepository>();
            serviceCollection.AddSingleton<IReCaptchaService, ReCaptchaService>(provider=>new ReCaptchaService(Sitecore.Configuration.Settings.GetSetting("GoogleCaptchaPrivateKey")));
            serviceCollection.AddSingleton<CurrentContactContactIdentifierHandler, CurrentContactContactIdentifierHandler>();
            serviceCollection.AddSingleton<FieldValueContactIdentifierHandler, FieldValueContactIdentifierHandler>();
            serviceCollection.AddSingleton<FixedAddressContactIdentifierHandler, FixedAddressContactIdentifierHandler>();
            serviceCollection.AddSingleton<IFieldValueBinderMapFactory, FieldValueBinderMapFactory>();
            serviceCollection.AddSingleton<FileAttachmentTokenBuilder, FileAttachmentTokenBuilder>();
            serviceCollection.AddSingleton<IMailTokenBuilder, MailTokenBuilder>();
            serviceCollection.AddSingleton<IFormsFieldValueResolver, FormsFieldValueResolver>();
        }
    }
}