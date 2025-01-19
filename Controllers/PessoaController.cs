using Microsoft.AspNetCore.Mvc;
using Person.Model;
using Person.Repositories;

namespace Person.Controller
{
    // Data Annotations https://www.macoratti.net/13/12/c_vdda.htm
    [ApiController]
    [Route("Person")]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;

        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpPost]
        public PersonModel Register([FromBody] PersonModel p)
        {
            var obj = _personRepository.RegisterPerson(p);
            return obj;
        }

        [HttpGet]
        public List<PersonModel> Selection()
        {
            return _personRepository.SelectPersons();
        }

        [HttpPut("{codigo}")]
        public PersonModel Update(int codigo, [FromBody] PersonModel P)
        {
            P.Codigo = codigo;

            _personRepository.UpdatePerson(P);

            return P;
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {

            _personRepository.DeletePerson(codigo);
            return Ok();
        }
    }

}