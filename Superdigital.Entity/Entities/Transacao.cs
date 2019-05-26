using Superdigital.Domain.Entities;
using Superdigital.Domain.VaueObjects;
using Superdigital.Entity.Entities;

namespace Superdigital.Entity
{
    public class Transacao
    {
        public string ContaOrigem { get; set; }
        public string ContaDestino { get; set; }
        public decimal Valor { get; set; }
        public ValidationResult ValidationResult { get; private set; }
        public decimal ContaOrigemSaldoAtual { get; set; }
        public decimal ContaDestinoSaldoAtual { get; set; }
        public decimal ContaDestinoValorTransacao { get; set; }

        public virtual void AdicionarResultadoDeValidacao(ValidationError resultado)
        {
            if (ValidationResult == null)
                ValidationResult = new ValidationResult();

            ValidationResult.Add(resultado);
        }

        public Cliente TransacaoAdapters(TransacaoEntity entrada)
        {
            return new Cliente()
            {
                Conta = entrada.ContaOrigem,
                ContaDestino = entrada.ContaDestino,
                Valor = entrada.ContaDestinoValorTransacao
            };
        }
    }
}
