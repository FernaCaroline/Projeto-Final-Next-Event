using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEvent.Dtos; 
using NextEvent.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDataContext>();

// Hashers
builder.Services.AddScoped<IPasswordHasher<Administrador>, PasswordHasher<Administrador>>();
builder.Services.AddScoped<IPasswordHasher<Participante>, PasswordHasher<Participante>>();

// JSON opcional case-insensitive
builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.PropertyNameCaseInsensitive = true);

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { ok = true }));

//  ADMIN 
app.MapPost("/api/administrador/cadastrar", async (
    [FromBody] AdminRegisterRequest req,
    [FromServices] AppDataContext db,
    [FromServices] IPasswordHasher<Administrador> hasher) =>
{
    var email = req.Email.Trim().ToLower();
    if (await db.Administradores.AnyAsync(a => a.Email.ToLower() == email))
        return Results.Conflict("Email já cadastrado.");

    var adm = new Administrador { Nome = req.Nome.Trim(), Email = email };
    adm.SenhaHash = hasher.HashPassword(adm, req.Senha);

    db.Administradores.Add(adm);
    await db.SaveChangesAsync();

    return Results.Created($"/api/administrador/{adm.Id}", new { adm.Id, adm.Nome, adm.Email, Mensagem = "Administrador cadastrado com sucesso!" });
});

app.MapPost("/api/administrador/login", async (
    [FromBody] AdminLoginRequest req,
    [FromServices] AppDataContext db,
    [FromServices] IPasswordHasher<Administrador> hasher) =>
{
    var email = req.Email.Trim().ToLower();
    var adm = await db.Administradores.FirstOrDefaultAsync(a => a.Email.ToLower() == email);
    if (adm is null) return Results.NotFound("Administrador não encontrado.");

    var ok = hasher.VerifyHashedPassword(adm, adm.SenhaHash, req.Senha) != PasswordVerificationResult.Failed;
    return ok
        ? Results.Ok(new LoginResponse(adm.Id, adm.Nome, adm.Email, "Login bem-sucedido!"))
        : Results.Unauthorized();
});

// PARTICIPANTE
app.MapPost("/api/participante/cadastrar", async (
    [FromBody] ParticipanteRegisterRequest req,
    [FromServices] AppDataContext db,
    [FromServices] IPasswordHasher<Participante> hasher) =>
{
    var email = req.Email.Trim().ToLower();
    if (await db.Participantes.AnyAsync(p => p.Email.ToLower() == email))
        return Results.Conflict("Email já cadastrado.");

    var p = new Participante { Nome = req.Nome.Trim(), Email = email };
    p.SenhaHash = hasher.HashPassword(p, req.Senha);

    db.Participantes.Add(p);
    await db.SaveChangesAsync();

    return Results.Created($"/api/participante/{p.Id}", new { p.Id, p.Nome, p.Email, Mensagem = "Participante cadastrado com sucesso!" });
});

app.MapPost("/api/participante/login", async (
    [FromBody] ParticipanteLoginRequest req,
    [FromServices] AppDataContext db,
    [FromServices] IPasswordHasher<Participante> hasher) =>
{
    var email = req.Email.Trim().ToLower();
    var p = await db.Participantes.FirstOrDefaultAsync(x => x.Email.ToLower() == email);
    if (p is null) return Results.NotFound("Participante não encontrado.");

    var ok = hasher.VerifyHashedPassword(p, p.SenhaHash, req.Senha) != PasswordVerificationResult.Failed;
    return ok
        ? Results.Ok(new LoginResponse(p.Id, p.Nome, p.Email, "Login bem-sucedido!"))
        : Results.Unauthorized();
});

app.Run();
