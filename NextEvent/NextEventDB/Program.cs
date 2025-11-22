using NextEvent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("front",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("front");

//  Cadastro do administrador
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
        return Results.Conflict("E-mail já cadastrado!");
    }

    string hashSenha = BCrypt.Net.BCrypt.HashPassword(administrador.Senha);
    administrador.Senha = hashSenha;


    ctx.Administradores.Add(administrador);
    ctx.SaveChanges();
    return Results.Created("", administrador);

});

// Listar administrador
app.MapGet("/api/administrador/listar", ([FromServices] AppDataContext ctx) =>
{

    if (ctx.Administradores.Any())
    {
        return Results.Ok(ctx.Administradores.ToList());
    }

    return Results.NotFound("Nenhum administrador registrado!");


});

//  Login administrador
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

// Atualizar adminstrador
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

    return Results.Ok(resultado);
});

// Deletar admnistrador

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


// Cadastro participante
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


// Listar participante
app.MapGet("/api/participante/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Participantes.Any())
    {
        return Results.Ok(ctx.Participantes.ToList());
    }

    return Results.NotFound("Nenhum participante registrado!");
});


// Login participante
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


// Atualizar participante
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


// Deletar participante
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



// Cadastrar Evento

app.MapPost("/api/evento/cadastrar", ([FromBody] Evento evento, [FromServices] AppDataContext db) =>
{
    
    db.Eventos.Add(evento);
    db.SaveChanges();
    return Results.Created($"/api/evento/{evento.Id}", new
    {
        mensagem = "Evento cadastrado com sucesso!",
        evento
    });
});

// Listar todos os Eventos

app.MapGet("/api/evento/listar", ([FromServices] AppDataContext db) =>
{
    var eventos = db.Eventos.ToList();
    return Results.Ok(eventos);
});


// Buscar evento por ID

app.MapGet("/api/evento/buscar/{id}", (int id, [FromServices] AppDataContext db) =>
{
    var evento = db.Eventos.FirstOrDefault(e => e.Id == id);
    if (evento == null)
        return Results.NotFound(new { mensagem = "Evento não encontrado." });

    return Results.Ok(evento);
});

// Atualiza Evento cadastrado
app.MapPatch("/api/evento/atualizar/{id}", (int id, [FromBody] Evento dados, [FromServices] AppDataContext db) =>
{
    var evento = db.Eventos.FirstOrDefault(e => e.Id == id);
    if (evento == null)
        return Results.NotFound(new { mensagem = "Evento não encontrado." });

    // Só altera o que veio no body (sem sobrescrever o resto)
    if (!string.IsNullOrEmpty(dados.Nome))
        evento.Nome = dados.Nome;
    
    if (!string.IsNullOrEmpty(dados.Descricao))
        evento.Descricao = dados.Descricao;
    
    if (dados.DataInicio != DateTime.MinValue)
        evento.DataInicio = dados.DataInicio;
    
    if (dados.DataFim != DateTime.MinValue)
        evento.DataFim = dados.DataFim;

    db.SaveChanges();
    return Results.Ok(new { mensagem = "Evento atualizado parcialmente com sucesso!", evento });
});




// Encerrar evento
app.MapPut("/api/evento/encerrar/{id}", (int id, [FromServices] AppDataContext db) =>
{
    var evento = db.Eventos.FirstOrDefault(e => e.Id == id);
    if (evento == null)
        return Results.NotFound(new { mensagem = "Evento não encontrado." });

    evento.Ativo = false;
    db.SaveChanges();
    return Results.Ok(new { mensagem = "Evento encerrado com sucesso!" });
});


// Deletar evento
app.MapDelete("/api/evento/deletar/{id}", (int id, [FromServices] AppDataContext db) =>
{
    var evento = db.Eventos.FirstOrDefault(e => e.Id == id);
    if (evento == null)
        return Results.NotFound(new { mensagem = "Evento não encontrado." });

    db.Eventos.Remove(evento);
    db.SaveChanges();
    return Results.Ok(new { mensagem = "Evento removido com sucesso." });
});

// Inscrição
// Create -> Conferir se o partc existe, se o evento existe - se sim, cria, se não, da erro
app.MapPost("/api/inscricao/cadastrar", ([FromBody] Inscricao inscricao, [FromServices] AppDataContext db) =>
{
    //verificação da existência do evento, do participante e de cadastros duplicados
    var evento = db.Eventos.FirstOrDefault(e => e.Id == inscricao.EventoId && e.Ativo);
    if (evento == null)
        return Results.NotFound(new { mensagem = "Evento não encontrado" });

    var participante = db.Participantes.FirstOrDefault(p => p.Id == inscricao.ParticipanteId);
    if (participante == null)
        return Results.NotFound(new { mensagem = "Participante não encontrado" });

    bool inscricaoExistente = db.Inscricoes.Any(i => i.EventoId == inscricao.EventoId && i.ParticipanteId == inscricao.ParticipanteId);
    if (inscricaoExistente)
        return Results.BadRequest(new { mensagem = "Não é possível cadastrar-se novamente no mesmo evento" });

    db.Inscricoes.Add(inscricao);
    db.SaveChanges();
    return Results.Ok(new { mensagem = "Inscrição realizada com sucesso" });
});

// Deletar inscrição
app.MapDelete("/api/inscricao/deletar/{id}", (int id, [FromServices] AppDataContext db) =>
{
    var inscricao = db.Inscricoes.FirstOrDefault(i => i.Id == id);
    if (inscricao == null)
        return Results.NotFound(new { mensagem = "Inscrição não encontrado" });

    db.Inscricoes.Remove(inscricao);
    db.SaveChanges();
    return Results.Ok(new { mensagem = "Inscrição removida" });
});


// Buscar inscrição
app.MapGet("/api/inscricao/buscar/{id}", (int id, [FromServices] AppDataContext db) =>
{
    var inscricao = db.Inscricoes
        .Where(i => i.Id == id)
        .Select( i => new
        {
            Inscricao = i.Id,
            DataDaInscricao = i.DataInscricao,
            Participante = new
            {
                
                ParticipanteId = i.Participante.Id,
                Nome = i.Participante.Nome,
                Email = i.Participante.Email,
            },
            
            Evento = new
            {
                EventoId = i.Evento.Id,
                Nome = i.Evento.Nome,
                DataInicio = i.Evento.DataInicio,
                DataFim = i.Evento.DataFim
            }
        })
        .FirstOrDefault();
    
    if (inscricao == null)
        return Results.NotFound(new { mensagem = "Inscrição não encontrado." });

    return Results.Ok(inscricao);
});









app.Run();