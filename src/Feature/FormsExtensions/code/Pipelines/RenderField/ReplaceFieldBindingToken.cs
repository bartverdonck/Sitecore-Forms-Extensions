using Feature.FormsExtensions.Business.FieldBindings;
using Feature.FormsExtensions.Fields.Bindings;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.RenderField
{
    public class ReplaceFieldBindingToken : MvcPipelineProcessor<RenderFieldEventArgs>
    {
        private readonly IFieldBindingMapFactory fieldBindingMapFactory;

        public ReplaceFieldBindingToken(IFieldBindingMapFactory fieldBindingMapFactory)
        {
            this.fieldBindingMapFactory = fieldBindingMapFactory;
        }

        public override void Process(RenderFieldEventArgs args)
        {
            if (!(args.ViewModel is IBindingSettings bindingSettings))
            {
                return;
            }
            if (!bindingSettings.PrefillBindingValue)
            {
                return;
            }
            if (string.IsNullOrEmpty(bindingSettings.BindingToken))
            {
                return;
            }

            var tokenMap = fieldBindingMapFactory.GetFieldBindingTokenMap();
            if (tokenMap == null || !tokenMap.ContainsKey(new FieldBindingTokenKey(bindingSettings.BindingToken)))
                return;

            var tokenHandler = tokenMap[new FieldBindingTokenKey(bindingSettings.BindingToken)];
            if (tokenHandler == null)
            {
                return;
            }

            var value = tokenHandler.GetBindingValue();
            if (!value.HasValue())
            {
                return;
            }

            var property = args.ViewModel.GetType().GetProperty("Value");
            if (value.Value.GetType() != property?.PropertyType)
            {
                return;
            }
            property.SetValue(args.ViewModel, value.Value);
        }


    }
}