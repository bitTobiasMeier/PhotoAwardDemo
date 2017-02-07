using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace PhotoAward.Platform.Controller
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("Ping")]
        public string Ping()
        {
            return string.Format(CultureInfo.InvariantCulture, "Hello at photo club: {0}", DateTime.Now.ToString("dd MMM yyyy HH:mm:ss")) ;
        }

        
    }
}
