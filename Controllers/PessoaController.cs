using Microsoft.AspNetCore.Mvc;

namespace Person.Controller
{
    // Data Annotations https://www.macoratti.net/13/12/c_vdda.htm
    [ApiController]
    [Route("Person")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public string MyFirstRoute()
        {
            return "Hello world";
        }
    }
}