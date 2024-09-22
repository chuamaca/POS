using POS.Application.Extensions;
using POS.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var Cors = "MyCorsPolicy";


// Add services to the container.
// Configuracion para Injeccion de Dependecnia ==== INICIO
var configuration = builder.Configuration;
builder.Services.AddInjectionInfraestructure(configuration);
// Configuracion para Injeccion de Dependecnia ==== FIN
builder.Services.AddInjectionApplication(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors, builder =>
    {
        //builder.AllowAnyOrigin(); 
        //builder.AllowAnyMethod();
        //builder.AllowAnyHeader();

        builder.WithOrigins("*")  // Asegúrate de agregar el puerto del frontend
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors(Cors);

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

public partial class Program { }