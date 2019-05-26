CREATE TABLE [dbo].[Transacao] (
    [Id]                         INT         NOT NULL IDENTITY (1,1),
    [ContaOrigem]                NUMERIC (8) NULL,
    [ContaDestino]               NUMERIC (8) NULL,
    [ContaOrigemSaldoAtual]      DECIMAL (9) NULL,
    [ContaDestinoSaldoAtual]     DECIMAL (9) NULL,
    [ContaDestinoValorTransacao] DECIMAL (9) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
