using POS.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Configuracion para Injeccion de Dependecnia ==== INICIO
var configuration = builder.Configuration;
builder.Services.AddInjectionInfraestructure(configuration);
// Configuracion para Injeccion de Dependecnia ==== FIN

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
