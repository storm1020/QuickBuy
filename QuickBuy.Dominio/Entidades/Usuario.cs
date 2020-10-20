using System.Collections;
using System.Collections.Generic;

namespace QuickBuy.Dominio.Entidades
{
    public class Usuario
    {

        public int Id { get; set; }
        public int Email { get; set; }
        public int Senha { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }

        /// <summary>
        /// Um usuário pode ter nenhum ou muitos pedidos.
        /// </summary>
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
