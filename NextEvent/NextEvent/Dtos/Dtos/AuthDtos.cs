namespace NextEvent.Dtos;

// Admin
public record AdminRegisterRequest(string Nome, string Email, string Senha);
public record AdminLoginRequest(string Email, string Senha);

// Participante
public record ParticipanteRegisterRequest(string Nome, string Email, string Senha);
public record ParticipanteLoginRequest(string Email, string Senha);

// Resposta comum de login
public record LoginResponse(int Id, string Nome, string Email, string Mensagem);
