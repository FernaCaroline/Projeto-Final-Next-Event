using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NextEvent.Models;

public class Participante
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public ICollection<Inscricao>? Inscricoes { get; set; }

    public void DefinirSenha(string senha, IPasswordHasher<Participante> hasher)
        => SenhaHash = hasher.HashPassword(this, senha);

    public bool VerificarSenha(string senha, IPasswordHasher<Participante> hasher)
        => hasher.VerifyHashedPassword(this, SenhaHash, senha) != PasswordVerificationResult.Failed;
}
