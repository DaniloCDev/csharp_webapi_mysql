using Microsoft.AspNetCore.Mvc;
using Person.Model;
using Person.Repositories;
using Swashbuckle.AspNetCore.Annotations;


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
        [SwaggerOperation(Summary = "Criando um registro de pessoas", Description = "Retorna os dados enviados com o codigo do dados registrado, não é nescessario enviar o campo codigo")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
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
        [SwaggerOperation(Summary = "Obtém uma lista de todas as  pessoas", Description = "Retorna todos os registros de pessoas na base de dados.")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
        public List<PersonModel> Selection()
        {
            return _personRepository.SelectPersons();
        }

        [HttpGet("{codigo}")]
        [SwaggerOperation(Summary = "Obtém dados de uma pessoa por id", Description = "Retorna todos os registros de pessoas na base de dados.")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
        public IActionResult FindPerson(int codigo)
        {
            var pessoa = _personRepository.FindPersonByID(codigo);

            // Se a pessoa não for encontrada, retorna 404 (Not Found)
            if (pessoa == null)
            {
                return NotFound();
            }

            // Caso contrário, retorna a pessoa encontrada como resposta
            return Ok(pessoa);
        }


        [HttpPut("{codigo}")]
        [SwaggerOperation(Summary = "Atualiza os dados de algum registro", Description = "Retorna os dados atualizados no banco de dados")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Update(int codigo, [FromBody] PersonModel P)
        {

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
        [SwaggerOperation(Summary = "Deleta o registro refente ao id passado", Description = "Retorna status ok")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int codigo)
        {

            if (!_personRepository.ExistPerson(codigo)) // Corrigido para verificar se a pessoa NÃO existe
            {
                return NotFound(new { Message = "Pessoa não existe" });
            }

            _personRepository.DeletePerson(codigo);
            return Ok();
        }
    }

}