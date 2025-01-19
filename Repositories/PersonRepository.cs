namespace Person.Repositories 
{
    public class PersonRepository
    {
        private readonly string ? _stringConnection;
        public PersonRepository(string stringConnection)
        {
            _stringConnection = stringConnection;
            
        }
    }
}