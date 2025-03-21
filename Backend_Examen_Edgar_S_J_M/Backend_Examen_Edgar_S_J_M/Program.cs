using Backend_Examen_Edgar_S_J_M.Persistence;
using Backend_Examen_Edgar_S_J_M.Persistence.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<CursoRepository>();
builder.Services.AddScoped<AlumnoRepository>();
builder.Services.AddScoped<ProfesorRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorFront",
        builder => builder.WithOrigins("http://localhost:5010")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("BlazorFront");

app.Run();
