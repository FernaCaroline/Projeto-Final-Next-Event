using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal; 

namespace NextEvent.Models;

public class AppDataContext : DbContext
{
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Inscricao> Inscricoes { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=NextEventDB.db");
    }
}
