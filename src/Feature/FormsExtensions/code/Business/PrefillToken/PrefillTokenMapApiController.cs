using System.Linq;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    [ServicesController]
    public class PrefillTokenMapApiController : ServicesApiController
    {
        private readonly IPrefillTokenMapFactory prefillTokenMapFactory;

        public PrefillTokenMapApiController():this(ServiceLocator.ServiceProvider.GetService<IPrefillTokenMapFactory>())
        {
        }

        public PrefillTokenMapApiController(IPrefillTokenMapFactory prefillTokenMapFactory)
        {
            this.prefillTokenMapFactory = prefillTokenMapFactory;
        }

        [HttpGet]
        [Route("sitecore/api/scformsextension/tokenmap")]
        public IHttpActionResult GetTokenMap()
        {
            var keys = prefillTokenMapFactory.GetPrefillTokenMap().Keys.ToList();//.Select(x => new PrefillTokenKey(){Id=x, Name = x});
            return Ok(keys);
        }
    }
}