using Person.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//string Connection
var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

// connection with Repository
builder.Services.AddSingleton(new PersonRepository(stringConnection));



var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();