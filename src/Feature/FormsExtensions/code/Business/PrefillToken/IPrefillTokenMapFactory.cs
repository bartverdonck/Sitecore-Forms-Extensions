using System.Collections.Generic;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    public interface IPrefillTokenMapFactory
    {
        Dictionary<string, IPrefillTokenHandler> GetPrefillTokenMap();
    }
}