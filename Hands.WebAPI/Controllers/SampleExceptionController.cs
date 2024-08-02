using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hands.WebAPI.Controllers
{
    public class SampleExceptionController : ApiController
    {


        public IHttpActionResult GetSampleException()
        {


            throw new Exception("test exception");
            return null;

        }
    }
}
