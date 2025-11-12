namespace NextEvent.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class Inscricao
{
    public Inscricao()
    {
        DataInscricao = DateTime.Now;

    }
    
    [Column("InscricaoId")]
    public int Id { get; set; }
    public DateTime DataInscricao { get; set; }
    public int ParticipanteId { get; set; }
    public Participante? Participante { get; set; }
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }

}


