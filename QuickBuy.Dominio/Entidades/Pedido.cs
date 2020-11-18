using QuickBuy.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBuy.Dominio.Entidades
{
    public class Pedido : Entidade
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime DataPrevisaoEntrega { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string EnderecoCompleto { get; set; }
        public string NumeroEndereco { get; set; }
        public int FormaPagamentoId { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        /// <summary>
        /// Pedido deve ter pelo menos um item de pedido,
        /// ou muitos itens pedidos.
        /// </summary>
        public ICollection<ItemPedido> ItensPedido { get; set; }

        public override void Validate()
        {
            LimparMensagensValidacao();

            if (!ItensPedido.Any())
                AdicionarCritica("Pedido está vazio, selecione itens para seu pedido!");

            if (string.IsNullOrEmpty(CEP))
                AdicionarCritica("Preenchimento - CEP obrigatório, por favor, informe-o!");

            if (string.IsNullOrEmpty(Estado))
                AdicionarCritica("Preenchimento - ESTADO obrigatório, por favor, informe-o!");

            if (string.IsNullOrEmpty(Cidade))
                AdicionarCritica("Preenchimento - CIDADE obrigatório, por favor, informe-o!");

            if (string.IsNullOrEmpty(EnderecoCompleto))
                AdicionarCritica("Preenchimento - ENDEREÇO COMPLETO é obrigatório, por favor, informe-o!");

            if (string.IsNullOrEmpty(NumeroEndereco))
                AdicionarCritica("Preenchimento - NÚMERO ENDEREÇO é obrigatório, por favor, informe-o!");

            if (FormaPagamento.NaoFoiDefinido)
                AdicionarCritica("Por favor, defina um tipo de pagamento!");

        }
    }
}
