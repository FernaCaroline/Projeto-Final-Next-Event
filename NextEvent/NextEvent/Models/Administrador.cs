using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NextEvent.Models;

public class Administrador
{
    public Administrador()
    {
        CriadoEm = DateTime.Now;
    }

    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    // Agora com hash — nada de senha em texto puro
    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; }

    public void DefinirSenha(string senha, IPasswordHasher<Administrador> hasher)
        => SenhaHash = hasher.HashPassword(this, senha);

    public bool VerificarSenha(string senha, IPasswordHasher<Administrador> hasher)
        => hasher.VerifyHashedPassword(this, SenhaHash, senha) != PasswordVerificationResult.Failed;
}
