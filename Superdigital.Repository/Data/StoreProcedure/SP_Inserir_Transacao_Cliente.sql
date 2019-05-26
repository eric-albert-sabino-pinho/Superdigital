CREATE PROCEDURE dbo.SP_Inserir_Transacao_Cliente
(
	@ContaOrigem NUMERIC(8),
	@ContaDestino NUMERIC(8),
	@ContaOrigemSaldoAtual DECIMAL(9),
	@ContaDestinoSaldoAtual DECIMAL(9),
	@ContaDestinoValorTransacao DECIMAL(9)
)
AS
BEGIN
	 INSERT INTO Transacao(ContaOrigem,ContaDestino,ContaOrigemSaldoAtual,ContaDestinoSaldoAtual,ContaDestinoValorTransacao) VALUES(@ContaOrigem,@ContaDestino,@ContaOrigemSaldoAtual,@ContaDestinoSaldoAtual,@ContaDestinoValorTransacao)
END
GO