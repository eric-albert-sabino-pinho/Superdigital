using Superdigital.Domain.Entities;
using Superdigital.Domain.Service;
using Superdigital.Domain.Validations;
using Superdigital.Domain.VaueObjects;
using Superdigital.Entity;
using Superdigital.Repository.DataModel;
using System.Linq;

namespace Superdigital.Service.Lancamento
{
    public class TransacaoApp : ApplicationService
    {
        public ValidationResult Lancamento(TransacaoEntity entrada)
        {
            var transacaoValidation = new TransacaoValidation().Valid(entrada);
            if (transacaoValidation.IsValid)
            {
                var contasLancamento = new Transacao().TransacaoAdapters(entrada);

                var clienteContaOrigem = new TransacaoRepository().ConsultarInformacaoCliente(contasLancamento.Conta);

                if (clienteContaOrigem != null)
                {
                    var saldoOrigem = new TransacaoRepository().AtualizarSaldoCliente(entrada.ContaOrigem, (clienteContaOrigem.SALDO - entrada.ContaDestinoValorTransacao));

                    var clienteContaDestino = new TransacaoRepository().ConsultarInformacaoCliente(contasLancamento.ContaDestino);

                    if (clienteContaDestino != null)
                    {
                        var saldodestino = new TransacaoRepository().AtualizarSaldoCliente(entrada.ContaDestino, (clienteContaDestino.SALDO + entrada.ContaDestinoValorTransacao));

                        if (saldoOrigem == 1 && saldodestino == 1)
                        {
                            var logtransacao = new TransacaoRepository().InserirLogTransacao(entrada, (clienteContaOrigem.SALDO - entrada.ContaDestinoValorTransacao), (clienteContaDestino.SALDO + entrada.ContaDestinoValorTransacao));

                            transacaoValidation.Success = true;
                        }
                    }
                }
            }
            else
            {
                transacaoValidation.Errors.ToList().ForEach(item => entrada.AdicionarResultadoDeValidacao(item));
            }
            return transacaoValidation;
        }
    }
}
