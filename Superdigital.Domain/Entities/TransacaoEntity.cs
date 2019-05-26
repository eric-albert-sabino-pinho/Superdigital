using Superdigital.Domain.VaueObjects;

namespace Superdigital.Domain.Entities
{
    public class TransacaoEntity
    {
        public string ContaOrigem { get; set; }
        public string ContaDestino { get; set; }
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

        public ClienteEntity TransacaoAdapters(TransacaoEntity entrada)
        {
            return new ClienteEntity()
            {
                Conta = entrada.ContaOrigem,
                ContaDestino = entrada.ContaDestino,
                ContaDestinoValorTransacao = entrada.ContaDestinoValorTransacao
            };
        }
    }
}
