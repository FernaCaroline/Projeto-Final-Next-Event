namespace NextEvent.Models;

public class Evento
{
    
    
    
    public int Id { get; set; }
    public string Nome { get; set; } = String.Empty;
    public string Descricao { get; set; } = String.Empty;
    public DateTime DataInicio  { get; set; }
    public DateTime DataFim  { get; set; }
    public bool Ativo   { get; set; } =  true;
    public DateTime CriadoEm  { get; set; } =  DateTime.Now;
    
    // Relacionamentos
    public int AdministradorId { get; set; }
    public Administrador Administrador { get; set; }
    
    public ICollection<Inscricao>? Inscricoes { get; set; }
    
}