USE [BIZLINKS_TST]
GO
ALTER PROCEDURE [dbo].[USP_BajaDocumentoFE]
                -- Add the parameters for the stored procedure here
@NUMERODOCUMENTOEMISOR NVARCHAR(20),
@SERIENUMERO NVARCHAR(13), -- Es el número del documento ejem: F001-00000001, no olvidar que debe comenzar con F o B en la serie igual al ejemplo
@TIPODOCUMENTO NVARCHAR(2), -- 01 para factura, 03 para boleta, 07 para NC, 08 para ND
@FECHA NVARCHAR(20),
@FECHARA NVARCHAR(20),
@motivo nvarchar(250),
@resumenid nvarchar(20)
AS
BEGIN
                -- SET NOCOUNT ON added to prevent extra result sets from
                -- interfering with SELECT statements.
                SET NOCOUNT ON;
                --DECLARE @FECHA VARCHAR(10)
                DECLARE @FECHARESUMEN VARCHAR(8)
                DECLARE @RESUMENCOUNT INT
                DECLARE @RA NVARCHAR(15)

    IF @TIPODOCUMENTO = '03'
                BEGIN

                               --SET @FECHA = (SELECT CONVERT(char(10), GetDate(),126))
                               SET @FECHARESUMEN = REPLACE(@FECHA, '-', '')

                               SET @RESUMENCOUNT = (SELECT ISNULL(COUNT(*), 0) FROM SPE_SUMMARYHEADER WHERE fechaGeneracionResumen = @FECHA) + 1

                               SET @RA = 'RC-' + @FECHARESUMEN + '-' + RIGHT('000' + CONVERT(NVARCHAR(3), @RESUMENCOUNT), 3)

                               insert into SPE_SUMMARYHEADER 
                               select numeroDocumentoEmisor, @RA, 6, '-', fechaEmision, @FECHA , '1', 
                               razonSocialEmisor, 'RC', 'N', 0, 'W', NULL, NULL, NULL, NULL, null from SPE_EINVOICEHEADER 
                               where serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO  and numerodocumentoemisor = @NUMERODOCUMENTOEMISOR

                               ---------- PONER EL TOTALIGV O TOTALISC EN 0 SI ES NULL
                               INSERT INTO SPE_SUMMARY_ITEM
                               SELECT numeroDocumentoEmisor, tipoDocumentoEmisor, @RA, numeroFila, tipoDocumento, numeroCorrelativo, tipoDocumentoAdquiriente, numeroDocumentoAdquiriente,
                               numeroCorrBoletaModificada, tipoDocumentoModificado, '3', tipoMoneda, totalIgv, totalIsc, totalOtrosCargos, totalOtrosTributos, totalValorventaOpExoneradasIgv,
                               totalValorVentaOpGratuitas, totalValorVentaOpGravadaConIgv, totalValorVentaOpInafectasIgv, totalVenta, bl_createdAt, regimenPercepcion, tasaPercepcion, montoPercepcion,
                               montoTotalCobroPercepcion, baseImponiblePercepcion, null, null, null
                               FROM SPE_SUMMARY_ITEM where numeroCorrelativo = @SERIENUMERO AND  tipoDocumento = @TIPODOCUMENTO AND ESTADOITEM = '1'
							    and numerodocumentoemisor = @NUMERODOCUMENTOEMISOR
                               
                               UPDATE SPE_SUMMARYHEADER SET BL_ESTADOREGISTRO = 'A' WHERE resumenid = @RA
                               
                END

                IF @TIPODOCUMENTO <> '03'
                BEGIN

                               --SET @FECHA = (SELECT CONVERT(char(10), GetDate(),126))
                               SET @FECHARESUMEN = REPLACE(@FECHA, '-', '')

                               SET @RESUMENCOUNT = (SELECT ISNULL(COUNT(*), 0) FROM SPE_CANCELHEADER WHERE fechaGeneracionResumen = @FECHA) + 1

                               SET @RA = 'RA-' + @FECHARESUMEN + '-' + RIGHT('000' + CONVERT(NVARCHAR(3), @RESUMENCOUNT), 3)

                               insert into SPE_CANCELHEADER 
                               select numeroDocumentoEmisor, @RA, '6', '-', fechaEmision, @FECHA, 1, razonSocialEmisor, 'RA', 'N', 0, 'T', 0, 0,  NULL, GETDATE() 
                               from SPE_EINVOICEHEADER 
                               where serieNumero = @SERIENUMERO  AND tipoDocumento = @TIPODOCUMENTO  and numerodocumentoemisor = @NUMERODOCUMENTOEMISOR

                               INSERT INTO SPE_CANCELDETAIL SELECT numeroDocumentoEmisor, '6', @RA, 1, @motivo, RIGHT(SERIENUMERO, LEN(SERIENUMERO) - CHARINDEX('-', SERIENUMERO)), LEFT(SERIENUMERO, CHARINDEX('-', SERIENUMERO) - 1), tipoDocumento, GETDATE() 
                               from SPE_EINVOICEHEADER 
                               where serieNumero = @SERIENUMERO  AND tipoDocumento = @TIPODOCUMENTO and numerodocumentoemisor = @NUMERODOCUMENTOEMISOR

                               UPDATE SPE_CANCELHEADER SET BL_ESTADOREGISTRO = 'A' where RESUMENID = @RA

                END

END


