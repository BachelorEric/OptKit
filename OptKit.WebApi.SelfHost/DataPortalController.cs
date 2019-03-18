using OptKit.DataPortal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OptKit.WebApi.SelfHost
{
    public class DataPortalController : ApiController
    {
        public string Get()
        {
            return "a";
        }

        [HttpPost]
        public ApiResponse Execute(ApiRequest request)
        {
            return RT.Service.Resolve<IDataPortal>().Execute(request);
        }

        [HttpGet]
        public ApiResponse Execute([FromUri]string controller, [FromUri] string action, [FromUri]object[] args, [FromUri]HybridDictionary context)
        {
            return null;
            //return RT.Service.Resolve<IDataPortal>().Execute(new ApiRequest { Controller = ctl, Action = act, Parameters = args, Context = context });
        }
    }
}
