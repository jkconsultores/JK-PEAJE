CREATE PROCEDURE [dbo].[USP_EnviaDocumentoFE]
                -- Add the parameters for the stored procedure here
	@NUMERODOCUMENTOEMISOR NVARCHAR(20),  -- Aquí pones tu número de RUC
	@SERIENUMERO NVARCHAR(13), -- Es el número del documento ejem: F001-00000001, no olvidar que debe comenzar con F o B en la serie igual al ejemplo
	@TIPODOCUMENTO NVARCHAR(2) -- 01 para factura, 03 para boleta, 07 para NC, 08 para ND
AS BEGIN
                -- SET NOCOUNT ON added to prevent extra result sets from
                -- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @RA NVARCHAR(15)
	SET @RA = ''
            
	if exists(select * from SPE_EINVOICEHEADER_ADD where serieNumero = @SERIENUMERO  AND tipoDocumento = @TIPODOCUMENTO AND NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR and
	clave = 'totalTributosOpeGratuitas')
		update SPE_EINVOICEHEADER_ADD set valor = (select convert(numeric(18,2), sum(convert(float, importeigv))) from SPE_EINVOICEDETAIL where serieNumero = @SERIENUMERO  AND tipoDocumento = @TIPODOCUMENTO AND                                                                                          NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR)          where serieNumero = @SERIENUMERO  AND tipoDocumento = @TIPODOCUMENTO AND NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR and
		clave = 'totalTributosOpeGratuitas'

	-- Insert statements for procedure here
	UPDATE SPE_EINVOICEHEADER SET bl_estadoRegistro = 'X' WHERE serieNumero = @SERIENUMERO AND NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR AND tipoDocumento = @TIPODOCUMENTO
                
	IF LEFT(@SERIENUMERO, 1) = 'B'
	BEGIN
		SET @RA = (SELECT MAX(ISNULL(I.RESUMENID, '')) FROM SPE_SUMMARY_ITEM I INNER JOIN SPE_SUMMARYHEADER H ON I.RESUMENID = H.RESUMENID 
		WHERE I.numeroCorrelativo = @SERIENUMERO AND I.NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR AND I.tipoDocumento = @TIPODOCUMENTO AND H.BL_ESTADOREGISTRO = 'N')

		UPDATE SPE_SUMMARYHEADER SET bl_estadoRegistro = 'X' WHERE resumenid = @ra
	END
END