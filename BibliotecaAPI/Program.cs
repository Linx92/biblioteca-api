using BibliotecaAPI.Datos;
using BibliotecaAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// �rea de servicios
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(Program)
);
builder.Services.AddControllers().AddJsonOptions(options =>options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer("name = DefaultConnection"));


var app = builder.Build();

// if (app.Environment.IsDevelopment()) 
// {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//Área de middleware
app.Logger.LogInformation(
    "Entorno actual: {Environment}",
    app.Environment.EnvironmentName
);
app.UseLoguearRespuestaHTTP();

app.MapControllers();

app.Run();
