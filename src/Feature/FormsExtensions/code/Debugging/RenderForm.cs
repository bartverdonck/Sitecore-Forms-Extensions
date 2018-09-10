using Sitecore.ExperienceForms.Mvc.Pipelines.GetModel;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderFields;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Debugging
{
    public class RenderForm : MvcPipelineProcessor<RenderFormEventArgs>
    {
        public override void Process(RenderFormEventArgs args)
        {
        }
    }

    public class RenderFields : MvcPipelineProcessor<RenderFieldsEventArgs> {
        public override void Process(RenderFieldsEventArgs args)
        {
        }
    }

    public class RenderField : MvcPipelineProcessor<RenderFieldEventArgs>
    {
        public override void Process(RenderFieldEventArgs args)
        {
            
        }
    }

    public class GetModel : MvcPipelineProcessor<GetModelEventArgs> {
        public override void Process(GetModelEventArgs args)
        {
            
        }
    }

}