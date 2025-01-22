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
        [SwaggerOperation(Summary = "Obtém uma lista de todas as pessoas", Description = "Retorna todos os registros de pessoas na base de dados.")]
        [ProducesResponseType(typeof(IEnumerable<PersonModel>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Selection()
        {
            var persons = await _personRepository.SelectPersons();

            if (persons == null || !persons.Any())
                return NotFound(); // Retorna 404 se a lista estiver vazia ou nula

            return Ok(persons); // Retorna 200 com a lista de pessoas
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
        [SwaggerOperation(Summary = "Atualiza os dados de um registro", Description = "Atualiza os dados de uma pessoa existente e retorna os dados atualizados.")]
        [ProducesResponseType(typeof(PersonModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int codigo, [FromBody] PersonModel p)
        {
            // Atribuir o código à entidade
            p.Codigo = codigo;

            // Verificar se o registro existe
            if (!await _personRepository.ExistPerson(codigo)) 
            {
                return NotFound(new { Message = "Registro não encontrado." });
            }

            // Validação de entrada
            if (string.IsNullOrWhiteSpace(p.Nome))
            {
                return BadRequest(new { Message = "Nome é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(p.Cidade))
            {
                return BadRequest(new { Message = "Cidade é obrigatória." });
            }

            if (p.Idade <= 0 || p.Idade > 120)
            {
                return BadRequest(new { Message = "Idade deve ser maior que 0 e menor que 120." });
            }

            // Atualizar o registro
            await _personRepository.UpdatePerson(p); 

            // Retornar o registro atualizado
            return Ok(new
            {
                Message = "Registro atualizado com sucesso.",
                Person = p
            });
        }


        [HttpDelete("{codigo}")]
        [SwaggerOperation(Summary = "Remove um registro", Description = "Deleta o registro de uma pessoa com base no código informado.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int codigo)
        {
            // Verifica se a pessoa existe
            if (!await _personRepository.ExistPerson(codigo)) 
            {
                return NotFound(new { Message = "Pessoa não existe" });
            }

            await _personRepository.DeletePerson(codigo); 

            return Ok(new { Message = "Pessoa deletada com sucesso." });
        }
    }

}