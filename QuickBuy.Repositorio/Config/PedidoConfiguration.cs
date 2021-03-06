﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickBuy.Dominio.Entidades;

namespace QuickBuy.Repositorio.Config
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.EnderecoCompleto)
                .IsRequired()
                .HasMaxLength(700);

            builder.Property(p => p.NumeroEndereco)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.CEP)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.Cidade)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.DataPedido)
                .IsRequired();

            builder.Property(p => p.DataPrevisaoEntrega)
                .IsRequired();

            builder.Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(p => p.FormaPagamento); // Pagamento deve ter uma forma de pagamento.
        }
    }
}
