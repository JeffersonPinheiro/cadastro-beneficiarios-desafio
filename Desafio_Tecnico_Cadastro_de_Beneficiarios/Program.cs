using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Services;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.UnitOfWork;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Api.Middlewares;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.CrossCutting.Logging;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Workers;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("4tech-db-memory");
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlanosService, PlanoService>();
builder.Services.AddScoped<IBeneficiarioService, BeneficiarioService>();

builder.Services.AddScoped<IBeneficiariosRepository, BeneficiariosRepository>();
builder.Services.AddScoped<IPlanosRepository, PlanosRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHostedService<ExclusaoBeneficiariosWorker>();

builder.Services.AddAutoMapper(typeof(PlanoProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionMiddleware>();
}

app.MapControllers();

app.Run();
