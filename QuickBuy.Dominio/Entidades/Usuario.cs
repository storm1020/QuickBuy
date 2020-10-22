using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuickBuy.Dominio.Entidades
{
    public class Usuario : Entidade
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        /// <summary>
        /// Um usuário pode ter nenhum ou muitos pedidos.
        /// </summary>
        public ICollection<Pedido> Pedidos { get; set; }

        public override void Validate()
        {
            if (!Email.Any())
                AdicionarCritica("E-mail não informado, por favor, informe-o!");

            if (!Senha.Any())
                AdicionarCritica("Senha não informada, por favor, informe-a!");

            if (!Nome.Any())
                AdicionarCritica("Nome não informado, por favor, informe-o!");

            if (!SobreNome.Any())
                AdicionarCritica("Sobrenome não informado, por favor, informe-o!");
        }
    }
}
