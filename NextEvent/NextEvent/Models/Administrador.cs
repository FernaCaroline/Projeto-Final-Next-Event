using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Identity;

namespace NextEvent.Models;

public class Administrador
{

    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.Now;


}
