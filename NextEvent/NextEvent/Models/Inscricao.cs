namespace NextEvent.Models;

public class Inscricao
{
    public int Id { get; set; }
    
    public int ParticipanteId { get; set; }
    public Participante? Participante { get; set; }
    
    public int InscricaoId { get; set; }
    public Evento? Evento { get; set; }
    
    public DateTime DataInscricao { get; set; } =  DateTime.Now;
}