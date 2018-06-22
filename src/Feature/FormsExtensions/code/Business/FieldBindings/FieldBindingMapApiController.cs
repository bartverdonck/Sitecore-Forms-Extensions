using System.Linq;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    [ServicesController]
    public class FieldBindingMapApiController : ServicesApiController
    {
        private readonly IFieldBindingMapFactory fieldBindingMapFactory;

        public FieldBindingMapApiController():this(ServiceLocator.ServiceProvider.GetService<IFieldBindingMapFactory>())
        {
        }

        public FieldBindingMapApiController(IFieldBindingMapFactory fieldBindingMapFactory)
        {
            this.fieldBindingMapFactory = fieldBindingMapFactory;
        }

        [HttpGet]
        [Route("sitecore/api/scformsextension/fieldbindingtokenmap")]
        public IHttpActionResult GetFieldBindingTokenMap()
        {
            var keys = fieldBindingMapFactory.GetFieldBindingTokenMap().Keys.ToList();
            return Ok(keys);
        }
    }
}