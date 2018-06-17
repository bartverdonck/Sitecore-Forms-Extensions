using System.Collections.Generic;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    public interface IPrefillTokenMapFactory
    {
        Dictionary<PrefillTokenKey, IPrefillTokenHandler> GetPrefillTokenMap();
    }
}