using System;
using System.Collections.Generic;
using System.Text;

namespace Superdigital.Entity.Entities
{
    public class Cliente
    {
        public string Nome { get; set; }

        public string RG { get; set; }
        public string CPFJ { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public string Digito { get; set; }

        public decimal SALDO { get; set; }

        public string ContaDestino { get; set; }

        public decimal Valor { get; set; }
    }
}
