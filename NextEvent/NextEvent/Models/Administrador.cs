namespace NextEvent.Models;

public class Administrador
{
    // Construtor padrão (necessário pro Entity Framework)
    public Administrador()
    {
        CriadoEm = DateTime.Now;
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    
    
}