using Microsoft.AspNetCore.Mvc;
using Person.Model;

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


        [HttpPost]
        public PersonModel ManipularModelPerson([FromBody] PersonModel p)
        {
            return p;
        }
    }
}