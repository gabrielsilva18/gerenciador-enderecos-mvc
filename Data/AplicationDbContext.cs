using GerenciadorEnderecos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEnderecos.Data
{
    public class ApplicationDbContext : DbContext //Classe que representa o contexto do banco de dados, ou seja, a conexão com o banco e as tabelas que serão criadas.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) // Construtor que recebe as opções de configuração do banco de dados e passa para a classe base DbContext.
        {
        }

        // DbSets = tabelas no banco
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // Configurações adicionais do modelo, como chaves estrangeiras, relacionamentos, etc.
            base.OnModelCreating(modelBuilder);

            // Configura o relacionamento entre Endereco e Usuario
            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Enderecos)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Se excluir usuário, exclui os endereços
        }
    }
}