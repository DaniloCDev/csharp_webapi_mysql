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
        public IActionResult Register([FromBody] PersonModel p)
        {
            if (string.IsNullOrEmpty(p.Nome))
            {
                return BadRequest(new { Message = "Nome é obrigatório." });
            }

            if (string.IsNullOrEmpty(p.Cidade))
            {
                return BadRequest(new { Message = "Cidade é obrigatória." });
            }

            if (p.Idade <= 0 || p.Idade > 120)
            {
                return BadRequest(new { Message = "Idade deve ser um valor maior que 0 e menor 120." });
            }

            var obj = _personRepository.RegisterPerson(p);
            return Created(string.Empty, obj);

        }

        [HttpGet]
        public List<PersonModel> Selection()
        {
            return _personRepository.SelectPersons();
        }

        [HttpPut("{codigo}")]
        public IActionResult Update(int codigo, [FromBody] PersonModel P)
        {

            Console.WriteLine(P);
            P.Codigo = codigo;

            if (!_personRepository.ExistPerson(codigo)) // Corrigido para verificar se a pessoa NÃO existe
            {
                return NotFound(new { Message = "Pessoa não existe" });
            }
            if (string.IsNullOrEmpty(P.Nome))
            {
                return BadRequest(new { Message = "Nome é obrigatório." });
            }

            if (string.IsNullOrEmpty(P.Cidade))
            {
                return BadRequest(new { Message = "Cidade é obrigatória." });
            }

            if (P.Idade <= 0 || P.Idade > 120)
            {
                return BadRequest(new { Message = "Idade deve ser um valor maior que 0 e menor 120." });
            }

            _personRepository.UpdatePerson(P);

            return Ok(P);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {

            _personRepository.DeletePerson(codigo);
            return Ok();
        }
    }

}