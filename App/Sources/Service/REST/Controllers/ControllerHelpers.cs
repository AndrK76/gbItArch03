using AndrK.ZavPostav.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AndrK.ZavPostav.REST.Controllers
{
    public static class ControllerHelpers
    {
        public static HttpResponseMessage ErrResponse(this string msg, ApiController controller, int status = 0)
        {
            var result = new RestResult();
            result.Status = status;
            result.Message = msg;
            return controller.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public static HttpResponseMessage ErrResponse(this Exception err, ApiController controller)
        {
            var result = new RestResult();
            result.Status = 0;
            result.Message = err.Message;
            return controller.Request.CreateResponse(HttpStatusCode.InternalServerError, result);
        }

        public static HttpResponseMessage SuccessResponse(this string msg, ApiController controller)
        {
            var result = new RestResult();
            result.Status = 1;
            result.Message = msg;
            return controller.Request.CreateResponse(HttpStatusCode.OK, result);
        }



    }
}