using Microsoft.EntityFrameworkCore;
using QuickBuy.Dominio.Entidades;
using QuickBuy.Dominio.ObjetoDeValor;
using QuickBuy.Repositorio.Config;

namespace QuickBuy.Repositorio.Contexto
{
    public class QuickBuyContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedidos { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }

        public QuickBuyContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Classes de mapeamento:
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPedidoConfiguration());
            modelBuilder.ApplyConfiguration(new FormaPagamentoConfiguration());

            // CARGA INICIAL - Inserção através do Migration
            modelBuilder.Entity<FormaPagamento>().HasData(
                new FormaPagamento()
                {
                    Id = 1,
                    Nome = "Boleto",
                    Descricao = "Forma de Pagamento Boleto."
                },
                new FormaPagamento()
                {
                    Id = 2,
                    Nome = "Cartão de Credito",
                    Descricao = "Forma de Pagamento Cartão de Crédito."
                },
                new FormaPagamento()
                {
                    Id = 3,
                    Nome = "Depósito",
                    Descricao = "Forma de Pagamento Depósito."
                }
                );

            // CARGA INICIAL
            modelBuilder.Entity<Produto>().HasData(
                new Produto()
                {
                    Id = 1,
                    Descricao = "Video-game produzido pela empresa Sony, lançado em 2020.",
                    Nome = "Playstation - 5",
                    Preco = 5000
                },
                new Produto()
                {
                    Id = 2,
                    Descricao = "Mixer de mão Mondial",
                    Nome = "Mixer Mondial",
                    Preco = 150
                }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
