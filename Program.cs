using Person.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//string Connection
var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");

// Adicionando Swagger à aplicação
builder.Services.AddEndpointsApiExplorer(); //  identificar os endpoints da API
builder.Services.AddSwaggerGen(); //  gerar a documentação Swagger

// connection with Repository
builder.Services.AddSingleton(new PersonRepository(stringConnection));

var app = builder.Build();

// Usando o Swagger se a aplicação estiver em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger
    app.UseSwaggerUI(); // Habilita a interface do Swagger para explorar os endpoints
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
