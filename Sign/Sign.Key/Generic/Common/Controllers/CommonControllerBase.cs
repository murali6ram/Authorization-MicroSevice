using Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Common.Controllers
{
    public class CommonControllerBase : ControllerBase
    {
        /// <summary>
        /// IConfiguration Property
        /// </summary>
        public readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_configuration">IConfiguration</param>
        protected CommonControllerBase(IConfiguration _configuration) => Configuration = _configuration;

        /// <summary>
        /// Create Action Response
        /// </summary>
        /// <param name="baseResponse">BaseResponse</param>
        /// <returns></returns>
        protected ObjectResult ActionResponse(BaseResponse baseResponse) => StatusCode((int)baseResponse.Code, baseResponse);

        //protected IActionResult ActionResponse(object response) => StatusCode((int)baseResponse.Code, baseResponse);
    }
}
