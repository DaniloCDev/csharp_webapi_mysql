using Microsoft.EntityFrameworkCore;
using Person.Model;

namespace Person.Repositories
{
    public class PersonRepository
    {
        private readonly AppDbContext _context;
        public PersonRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<PersonModel> RegisterPerson(PersonModel p)
        {
            await _context.Pessoas.AddAsync(p); // Adiciona a entidade ao contexto
            await _context.SaveChangesAsync();  // Salva alterações no banco
            return p;
        }

        public async Task<List<PersonModel>> SelectPersons()
        {
            return await _context.Pessoas.ToListAsync(); // Retorna todos os registros
        }

        public async Task UpdatePerson(PersonModel p)
        {
            _context.Pessoas.Update(p);  // Marca a entidade como "modificada"
            await _context.SaveChangesAsync(); // Salva alterações
        }
        public async Task DeletePerson(int codigo)
        {
            var person = await _context.Pessoas.FindAsync(codigo); // Busca o registro pelo ID
            if (person != null)
            {
                _context.Pessoas.Remove(person); // Remove a entidade
                await _context.SaveChangesAsync(); // Salva alterações
            }
        }

        public async Task<PersonModel> FindPersonByID(int codigo)
        {
            return await _context.Pessoas.FindAsync(codigo); // Busca o registro pelo ID
        }


        public async Task<bool> ExistPerson(int codigo)
        {
            return await _context.Pessoas.AnyAsync(p => p.Codigo == codigo); // Verifica se existe
        }
    }
}