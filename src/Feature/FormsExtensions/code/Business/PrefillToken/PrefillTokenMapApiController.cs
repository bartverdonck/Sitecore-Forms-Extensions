using System.Linq;
using System.Web.Http;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    [ServicesController]
    public class PrefillTokenMapApiController : ServicesApiController
    {
        private readonly IPrefillTokenMapFactory prefillTokenMapFactory;

        public PrefillTokenMapApiController(IPrefillTokenMapFactory prefillTokenMapFactory)
        {
            this.prefillTokenMapFactory = prefillTokenMapFactory;
        }

        [HttpGet]
        [Route("sitecore/api/scformsextension/tokenmap")]
        public IHttpActionResult GetTokenMap()
        {
            var keys = prefillTokenMapFactory.GetPrefillTokenMap().Keys.Select(x => new PrefillTokenDto(){Id=x, Name = x});
            return Ok(keys);
        }
    }

    public class PrefillTokenDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}