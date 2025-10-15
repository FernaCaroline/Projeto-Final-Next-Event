using NextEvent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();



// POST: /api/administrador/cadastrar

app.MapPost("/api/administrador/cadastrar", ([FromBody]Administrador administrador, [FromServices] AppDataContext dbContext) =>
{
    dbContext.Administradores.Add(administrador);
    dbContext.SaveChanges();
});


app.Run();
