using MySql.Data.MySqlClient;
using Person.Model;

namespace Person.Repositories
{
    public class PersonRepository
    {
        private readonly string? _stringConnection;
        public PersonRepository(string stringConnection)
        {
            _stringConnection = stringConnection;

        }

        public PersonModel RegisterPerson(PersonModel p)
        {
            using var connection = new MySqlConnection(_stringConnection);
            connection.Open();

            string commandSql = "INSERT INTO pessoas (nome, cidade, idade) VALUES (@nome, @cidade, @idade);";
            commandSql += "SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(commandSql, connection);

            command.Parameters.AddWithValue("@nome", p.Nome);
            command.Parameters.AddWithValue("@cidade", p.Cidade);
            command.Parameters.AddWithValue("@idade", p.Idade);

            int codigoGerado = Convert.ToInt32(command.ExecuteScalar());

            p.Codigo = codigoGerado;
            return p;
        }

        public List<PersonModel> SelectPersons()
        {
            List<PersonModel> persons = [];
            using var connection = new MySqlConnection(_stringConnection);
            connection.Open();

            using var commandSql = new MySqlCommand("SELECT * FROM pessoas", connection);
            using var registers = commandSql.ExecuteReader();

            while (registers.Read())
            {
                persons.Add(new PersonModel
                {
                    Codigo = registers.GetInt32("codigo"),
                    Nome = registers.GetString("Nome"),
                    Cidade = registers.GetString("Cidade"),
                    Idade = registers.GetInt32("idade"),
                });
            }

            return persons;
        }

        public void UpdatePerson(PersonModel p)
        {
            using var connection = new MySqlConnection(_stringConnection);
            connection.Open();

            using var commandSql = new MySqlCommand("UPDATE pessoas SET nome = @nome, cidade = @cidade, idade = @idade WHERE codigo = @codigo", connection);

            commandSql.Parameters.AddWithValue("@codigo", p.Codigo);
            commandSql.Parameters.AddWithValue("@nome", p.Nome);
            commandSql.Parameters.AddWithValue("@cidade", p.Cidade);
            commandSql.Parameters.AddWithValue("@idade", p.Idade);

            commandSql.ExecuteNonQuery();
        }

        public void DeletePerson(int codigo)
        {
            using var connection = new MySqlConnection(_stringConnection);
            connection.Open();

            using var commandSql = new MySqlCommand("DELETE FROM pessoas WHERE codigo = @codigo", connection);
            commandSql.Parameters.AddWithValue("@codigo", codigo);
            commandSql.ExecuteNonQuery();
        }


        public bool ExistPerson (int codigo)
        {
            using var connection = new MySqlConnection(_stringConnection);
            connection.Open();

            using var commandSql = new MySqlCommand("SELECT COUNT(*) FROM pessoas WHERE codigo = @codigo", connection);
            commandSql.Parameters.AddWithValue("@codigo", codigo);
            int count = Convert.ToInt32(commandSql.ExecuteScalar());

            return count > 0;
        }
    }
}