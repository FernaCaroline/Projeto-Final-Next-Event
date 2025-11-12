namespace NextEvent.Models;

public class Evento
{
    public Evento()
    {
        CriadoEm = DateTime.Now;
        Ativo = true;
        Nome = string.Empty;
        Descricao = string.Empty;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public bool Ativo { get; set; }
    public DateTime CriadoEm { get; set; }
    public int AdministradorId { get; set; }

}