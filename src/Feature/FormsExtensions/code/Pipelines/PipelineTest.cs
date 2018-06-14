using Sitecore.ExperienceForms.Mvc;
using Sitecore.ExperienceForms.Mvc.Pipelines.GetModel;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderFields;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;
using Sitecore.Mvc.Pipelines;
using Sitecore.Pipelines.RenderField;

namespace Feature.FormsExtensions.Pipelines
{
    public class RenderForm : MvcPipelineProcessor<RenderFormEventArgs>
    {
        public override void Process(RenderFormEventArgs args)
        {
            
        }
    }

    public class RenderFields : MvcPipelineProcessor<RenderFieldsEventArgs>
    {
        private readonly IFormRenderingContext formRenderingContext;

        public RenderFields(IFormRenderingContext formRenderingContext)
        {
            this.formRenderingContext = formRenderingContext;
        }

        public override void Process(RenderFieldsEventArgs args)
        {

        }
    }

    public class RenderField : MvcPipelineProcessor<RenderFieldEventArgs>
    {
        private readonly IFormRenderingContext formRenderingContext;

        public RenderField(IFormRenderingContext formRenderingContext)
        {
            this.formRenderingContext = formRenderingContext;
        }

        public override void Process(RenderFieldEventArgs args)
        {
        }
    }

    public class GetModel : MvcPipelineProcessor<GetModelEventArgs>
    {
        private readonly IFormRenderingContext formRenderingContext;

        public GetModel(IFormRenderingContext formRenderingContext)
        {
            this.formRenderingContext = formRenderingContext;
        }

        public override void Process(GetModelEventArgs args)
        {
        }
    }
}