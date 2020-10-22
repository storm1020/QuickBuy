using QuickBuy.Dominio.Enumerados;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBuy.Dominio.ObjetoDeValor
{
    public class FormaPagamento
    {
        public int Id { get; set; }

        public String Nome { get; set; }

        public String Descricao { get; set; }

        public bool EhBoleto {        
            get { return Id == (int)TipoFormaPagamentoEnum.Boleto; }
        }

        public bool EhCartao {
            get { return Id == (int)TipoFormaPagamentoEnum.CartaCredito; }
        }

        public bool EhDeposito {
            get { return Id == (int)TipoFormaPagamentoEnum.Deposito; }
        }

        public bool NaoFoiDefinido {
            get { return Id == (int)TipoFormaPagamentoEnum.NaoDefinido; }
        }
    }
}
