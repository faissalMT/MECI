using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace MonitorCS.Controllers
{

    [Route("usecase/")]
    [ApiController]
    public class UsecaseController: ControllerBase
    {
        [HttpPost]
        public JsonResult PostUsecase(Usecases.UsecaseRequest request)
        {
            return new JsonResult(Depinj.getUsecase(request.usecase).execute(request.arguments));
        }
    }

    [Route("gateway/")]
    [ApiController]
    public class GatewayController: ControllerBase
    {
        [HttpPost]
        public JsonResult PostGateway(Gateways.GatewayRequest request)
        {
          dynamic gateway = Depinj.getGateway(request.gateway);
          var method = gateway.GetType().GetMethod(request.method);
          
          try {
            return new JsonResult(method.Invoke(gateway, request.arguments));
          } catch (Exception) { //Microsoft.CSharp.RuntimeBinderException
            return new JsonResult(gateway.execute(request.method, request.arguments));
          }
        }
    }
}
