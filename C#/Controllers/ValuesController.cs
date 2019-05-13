using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MonitorCS.Controllers
{

    [Route("usecase/")]
    [ApiController]
    public class UsecaseController : ControllerBase
    {
        // GET usecase/{usecase}
        [HttpPost()]
        public JsonResult PostUsecase(Usecases.UsecaseRequest request)
        {
            return new JsonResult(Depinj.getUsecase(request.usecase).execute(request.arguments));
        }
    }
}
