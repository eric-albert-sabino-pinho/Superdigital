﻿CREATE PROCEDURE SP_Atualizar_Cliente
(
	@Saldo DECIMAL(9),
	@Conta NUMERIC(8)
)
AS
BEGIN
	UPDATE dbo.CLIENTE SET SALDO = @Saldo
	WHERE CONTA = @Conta
END
