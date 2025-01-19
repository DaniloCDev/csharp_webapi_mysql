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
    }
}