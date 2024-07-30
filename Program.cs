using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using API_Organizacion.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrganizacionContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlConnection"), sqlServerOptionsAction: sqlOption =>
    {
        sqlOption.EnableRetryOnFailure(
            maxRetryCount: 20,
            maxRetryDelay: TimeSpan.FromSeconds(15),
            errorNumbersToAdd: null);
    }));

//==========================================================//
builder.Services.AddScoped<IPuestos, PuestosService>();

builder.Services.AddScoped<IParentescos, ParentescosService>();

builder.Services.AddScoped<IBeneficiarios, BeneficiariosService>();

builder.Services.AddScoped<IEmpleadosUsuarios, EmpleadosUsuariosService>();

builder.Services.AddScoped<ILogin, LoginService>();
//==========================================================//

builder.Services.AddCors(option => option.AddPolicy("AllowAnyOrigin",
    builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
