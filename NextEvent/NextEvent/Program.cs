using NextEvent.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

//  ADMIN - acesso via POST
app.MapPost("/api/administrador/cadastrar", (
    [FromBody] Administrador administrador,
    [FromServices] AppDataContext ctx) =>
{
    if (string.IsNullOrEmpty(administrador.Email) || string.IsNullOrEmpty(administrador.Senha))
    {
        return Results.BadRequest("Campo de email ou senha não preenchidos!");
    }

    bool resultado = ctx.Administradores.Any(x => x.Email == administrador.Email);
    if (resultado)
    {
        return Results.Conflict("Administrador já cadastrado!");
    }

    string hashSenha = BCrypt.Net.BCrypt.HashPassword(administrador.Senha);
    administrador.Senha = hashSenha;


    ctx.Administradores.Add(administrador);
    ctx.SaveChanges();
    return Results.Created("", administrador);

});

// Listar Admins - método GET

app.MapGet("/api/administrador/listar", ([FromServices] AppDataContext ctx) =>
{

    if (ctx.Administradores.Any())
    {
        return Results.Ok(ctx.Administradores.ToList());
    }

    return Results.NotFound("Nenhum administrador registrado!");


});

//  Login - POST

app.MapPost("/api/administrador/login", (
    [FromBody] Administrador administrador,
    [FromServices] AppDataContext ctx) =>
{
    if (string.IsNullOrEmpty(administrador.Email) || string.IsNullOrEmpty(administrador.Senha))
    {
       return Results.BadRequest("Campo de email ou senha não preenchidos!");
    }

    Administrador? resultado = ctx.Administradores.FirstOrDefault(x => x.Email == administrador.Email);

    if (resultado is null)
    {
        return Results.Unauthorized();
    }

    bool validarSenha = BCrypt.Net.BCrypt.Verify(administrador.Senha, resultado!.Senha);
    if (!validarSenha)
    {
        return Results.Unauthorized();
    }

    return Results.Ok("Login efetuado com sucesso!");


});

// Atualizar adm pelo ID - PUT

app.MapPut("/api/administrador/atualizar/{id}", (
    [FromRoute] int id,
    [FromBody] Administrador administrador,
    [FromServices] AppDataContext ctx) =>
{

    Administrador? resultado = ctx.Administradores.FirstOrDefault(x => x.Id == id);

    if (resultado is null)
    {
        return Results.NotFound("Administrador não encontrado!");
    }

    
    string hashSenha = BCrypt.Net.BCrypt.HashPassword(administrador.Senha);

    resultado.Nome = administrador.Nome;
    resultado.Email = administrador.Email;
    resultado.Senha = hashSenha;

    ctx.Administradores.Update(resultado);
    ctx.SaveChanges();

    return Results.Ok(ctx.Administradores.ToList());
});

// Deletar administrador - DELETE

app.MapDelete("/api/administrador/deletar/{id}", (
    [FromRoute] int id,
    [FromServices] AppDataContext ctx) =>
{
    Administrador? resultado = ctx.Administradores.FirstOrDefault(x => x.Id == id);

    if (resultado is null)
    {
        return Results.NotFound("Administrador não encontrado!");
    }

    ctx.Administradores.Remove(resultado);
    ctx.SaveChanges();

    return Results.Ok("Administrador deletado com sucesso!");
});

//**************PARTICIPANTE:


// CADASTRAR PARTICIPANTE - POST
app.MapPost("/api/participante/cadastrar", (
    [FromBody] Participante participante,
    [FromServices] AppDataContext ctx) =>
{
    if (string.IsNullOrEmpty(participante.Email) || string.IsNullOrEmpty(participante.Senha))
    {
        return Results.BadRequest("Campo de email ou senha não preenchidos!");
    }

    bool resultado = ctx.Participantes.Any(x => x.Email == participante.Email);
    if (resultado)
    {
        return Results.Conflict("Participante já cadastrado!");
    }

    string hashSenha = BCrypt.Net.BCrypt.HashPassword(participante.Senha);
    participante.Senha = hashSenha;

    ctx.Participantes.Add(participante);
    ctx.SaveChanges();
    return Results.Created("", participante);
});


// LISTAR PARTICIPANTES - GET
app.MapGet("/api/participante/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Participantes.Any())
    {
        return Results.Ok(ctx.Participantes.ToList());
    }

    return Results.NotFound("Nenhum participante registrado!");
});


// LOGIN PARTICIPANTE - POST
app.MapPost("/api/participante/login", (
    [FromBody] Participante participante,
    [FromServices] AppDataContext ctx) =>
{
    if (string.IsNullOrEmpty(participante.Email) || string.IsNullOrEmpty(participante.Senha))
    {
        return Results.BadRequest("Campo de email ou senha não preenchidos!");
    }

    Participante? resultado = ctx.Participantes.FirstOrDefault(x => x.Email == participante.Email);

    if (resultado is null)
    {
        return Results.Unauthorized();
    }

    bool validarSenha = BCrypt.Net.BCrypt.Verify(participante.Senha, resultado.Senha);
    if (!validarSenha)
    {
        return Results.Unauthorized();
    }

    return Results.Ok("Login efetuado com sucesso!");
});


// ATUALIZAR PARTICIPANTE PELO ID - PUT
app.MapPut("/api/participante/atualizar/{id}", (
    [FromRoute] int id,
    [FromBody] Participante participante,
    [FromServices] AppDataContext ctx) =>
{
    Participante? resultado = ctx.Participantes.FirstOrDefault(x => x.Id == id);

    if (resultado is null)
    {
        return Results.NotFound("Participante não encontrado!");
    }

    string hashSenha = BCrypt.Net.BCrypt.HashPassword(participante.Senha);

    resultado.Nome = participante.Nome;
    resultado.Email = participante.Email;
    resultado.Senha = hashSenha;

    ctx.Participantes.Update(resultado);
    ctx.SaveChanges();

    return Results.Ok(ctx.Participantes.ToList());
});


// DELETAR PARTICIPANTE - DELETE
app.MapDelete("/api/participante/deletar/{id}", (
    [FromRoute] int id,
    [FromServices] AppDataContext ctx) =>
{
    Participante? resultado = ctx.Participantes.FirstOrDefault(x => x.Id == id);

    if (resultado is null)
    {
        return Results.NotFound("Participante não encontrado!");
    }

    ctx.Participantes.Remove(resultado);
    ctx.SaveChanges();

    return Results.Ok("Participante deletado com sucesso!");
});



app.Run();