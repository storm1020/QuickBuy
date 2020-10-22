using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBuy.Dominio.Entidades
{
    public class Produto : Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public override void Validate()
        {
            if (Id == 0)
                AdicionarCritica("Referência de produto não encontrada.");

            if (!Nome.Any())
                AdicionarCritica("Critica - Nome do produto vazio.");

            if (!Descricao.Any())
                AdicionarCritica("Critica - Descricação do produto vazia.");

            if (Preco < 0)
                AdicionarCritica("Critica - Valor do produto negativo, verificar!");
        }
    }
}
