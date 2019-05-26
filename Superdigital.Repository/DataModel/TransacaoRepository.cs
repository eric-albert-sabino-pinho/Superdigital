using Superdigital.Domain.Entities;
using Superdigital.Entity;
using Superdigital.Entity.Interfaces;
using Superdigital.Repository.Repository.Parameters;

namespace Superdigital.Repository.DataModel
{
    public class TransacaoRepository
    {
        private readonly DapperUnitOfWork _unidadeDeTrabalho;

        public TransacaoRepository()
        {
            IUnitOfWork unidadeDeTrabalho = new DapperUnitOfWork();
            _unidadeDeTrabalho = (DapperUnitOfWork)unidadeDeTrabalho;
        }

        public ClienteEntity ConsultarInformacaoCliente(string contas)
        {
            const string proc = "SP_Consultar_Cliente";
            var paramConta = DataHelperParameters.CreateParameter("@ContaCorrente", contas);
            return _unidadeDeTrabalho.Get<ClienteEntity>(proc, paramConta);
        }

        public int AtualizarSaldoCliente(string conta, decimal saldo)
        {
            const string proc = "SP_Atualizar_Cliente";
            var paramConta = DataHelperParameters.CreateParameter("@Saldo", saldo);
            var paramSaldo = DataHelperParameters.CreateParameter("@Conta", conta);
            return _unidadeDeTrabalho.Execute(proc, paramConta, paramSaldo);
        }

        public ClienteEntity InserirLogTransacao(TransacaoEntity transacao, decimal contaOrigemSaldoAtual, decimal contaDestinoSaldoAtual)
        {
            const string proc = "SP_Inserir_Transacao_Cliente";
            var paramContaOrigem = DataHelperParameters.CreateParameter("@ContaOrigem", transacao.ContaOrigem);
            var paramContaDestino = DataHelperParameters.CreateParameter("@ContaDestino", transacao.ContaDestino);
            var paramContaOrigemSaldoAtual = DataHelperParameters.CreateParameter("@ContaOrigemSaldoAtual", contaOrigemSaldoAtual);
            var paramContaDestinoSaldoAtual = DataHelperParameters.CreateParameter("@ContaDestinoSaldoAtual", contaDestinoSaldoAtual);
            var paramContaDestinoValorTransacao = DataHelperParameters.CreateParameter("@ContaDestinoValorTransacao", transacao.ContaDestinoValorTransacao);
            return _unidadeDeTrabalho.Get<ClienteEntity>(proc, paramContaOrigem, paramContaDestino, paramContaOrigemSaldoAtual, paramContaDestinoSaldoAtual, paramContaDestinoValorTransacao);
        }
    }

}
