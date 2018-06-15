using Feature.FormsExtensions.Fields.Prefill;
using Feature.FormsExtensions.Pipelines.PrefillForm;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.RenderField
{
    public class LoadPrefillField : MvcPipelineProcessor<RenderFieldEventArgs>
    {
        private readonly IPrefillTokenMapFactory prefillTokenMapFactory;

        public LoadPrefillField(IPrefillTokenMapFactory prefillTokenMapFactory)
        {
            this.prefillTokenMapFactory = prefillTokenMapFactory;
        }

        public override void Process(RenderFieldEventArgs args)
        {
            if (!(args.ViewModel is IPrefillToken prefillTokenModel))
            {
                return;
            }
            if (!string.IsNullOrEmpty(prefillTokenModel.PrefillToken))
            {
                var tokenMap = prefillTokenMapFactory.GetPrefillTokenMap();
                var tokenHandler = tokenMap[prefillTokenModel.PrefillToken];
                if (tokenHandler != null)
                {
                    var value = tokenHandler.GetTokenValue();
                    if (args.ViewModel is InputViewModel<string> stringInputModel)
                    {
                        stringInputModel.Value = value;
                    }
                }
            }
        }

    }
}