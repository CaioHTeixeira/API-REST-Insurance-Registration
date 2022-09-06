using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persistence;

public class DatabaseContext : DbContext {
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }   

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Pessoa>(tabelaPessoa => {
            tabelaPessoa.HasKey(e => e.Id);
            tabelaPessoa.HasMany(e => e.contratos)
            .WithOne()
            .HasForeignKey(e => e.PessoaId);
        });

        builder.Entity<Contrato>(tabelaContrato => {
            tabelaContrato.HasKey(e => e.Id);
        });
    } 
}