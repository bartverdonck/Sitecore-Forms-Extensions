using System.Collections.Generic;

namespace Feature.FormsExtensions.Pipelines.PrefillForm
{
    public interface IPrefillTokenMapFactory
    {
        Dictionary<string, IPrefillFormTokenHandler> GetPrefillTokenMap();
    }
}
