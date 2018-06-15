using System;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.PrefillForm
{
    public class XDbTokenHandlerLoader : MvcPipelineProcessor<PrefillFormArgs>
    {
        public override void Process(PrefillFormArgs args)
        {
            args.TokenHandlers.Add("token.test", new TokenTester());
            args.TokenHandlers.Add("token.test2", new TokenTester());
        }
    }

    public class TokenTester : IPrefillFormTokenHandler {
        public string GetTokenValue()
        {
            return "token test value";
        }
    }
}