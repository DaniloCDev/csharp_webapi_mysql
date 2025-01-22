using Microsoft.EntityFrameworkCore;
using Person.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//string Connection
builder.Services.AddScoped<PersonRepository>(); 
// Adicionando Swagger à aplicação
builder.Services.AddEndpointsApiExplorer(); //  identificar os endpoints da API
builder.Services.AddSwaggerGen(); //  gerar a documentação Swagger

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))
    )
);

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
