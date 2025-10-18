using System.ComponentModel.DataAnnotations; // para uso do required e length

using Microsoft.AspNetCore.Identity;

namespace NextEvent.Models;

public class Participante
{

    //os campos foram deixados como obrigatórios, através do Required, com um limitador de caracteres, exceto o de senha, por conta do hash.
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.Now;


}
