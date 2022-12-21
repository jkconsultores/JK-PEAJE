CREATE PROCEDURE [dbo].[USP_DetalleFE]
                -- Add the parameters for the stored procedure here
	@NUMERODOCUMENTOEMISOR NVARCHAR(20),  -- Aquí pones tu número de RUC
	@SERIENUMERO NVARCHAR(13), -- Es el número del documento ejem: F001-00000001, no olvidar que debe comenzar con F o B en la serie igual al ejemplo
	@TIPODOCUMENTO NVARCHAR(2), -- 01 para factura, 03 para boleta, 07 para NC, 08 para ND
	@TIPODOCUMENTOEMISOR NVARCHAR(1), -- Ahí envía el texto “6” (seis) 
	@NUMEROORDENITEM NVARCHAR(4), -- poner un numero correlativo puede ser 1 o 0001
	@CANTIDAD NVARCHAR(25), --Cantidad
	@CODIGOPRODUCTO NVARCHAR(30),  --Codigo del producto
	@CODIGORAZONEXONERACION NVARCHAR(2),  --Si es ítem gravado = “10” si es exonerado  = “20”, inafecto = “30”, gratuito = “35”, exportación = “40”
	@DESCRIPCION NVARCHAR(1700),  --Descripcion del item
	@IMPORTEDESCUENTO NVARCHAR(25) = null,  -- Descuento de la linea
	@importeTotalSinImpuesto NVARCHAR(15),  -- Importe Total sin impuestos de la linea
	@importeUnitarioConImpuesto NVARCHAR(25),  --Importe Unitario con Impuestos
	@importeUnitarioSinImpuesto NVARCHAR(25),  --Importe Unitario Sin impuestos
	@CODIGOIMPORTEREFERENCIAL NVARCHAR(15) = NULL,  -- Solo si es grauita pone “02” sino NULL
	@IMPORTEREFERENCIAL NVARCHAR(15),  -- Solo si es gratuito va el monto de la linea
	@UNIDADMEDIDA NVARCHAR(5),  -- poner “NIU”
	@codigoImporteUnitarioConImpuesto NVARCHAR(2), -- poner “01”
	@ImporteIGV NVARCHAR(15), --Poner el importe del IGV 
	@ImporteISC NVARCHAR(15), --Poner el importe de ISC
	@importeCargo nvarchar(15), -- Recargo al consumo,
	@codigoProductoSUNAT NVARCHAR(30), -- Codigo Universal SUNAT
	@montoBaseIgv NVARCHAR(15), -- Monto Base afecto al IGV
	@tasaIGV NVARCHAR(15), -- Porcentaje del IGV
	@importeTotalImpuestos NVARCHAR(15), -- Total de Impuestos
	@importeBaseDescuento NVARCHAR(15), -- Base antes del descuento
	@factorDescuento NVARCHAR(4), -- Porcentaje del descuento 
	@textoAuxiliar250_1 NVARCHAR(250),
	@textoAuxiliar250_2 NVARCHAR(250),
	@textoAuxiliar250_3 NVARCHAR(250)
AS BEGIN
                -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        DELETE FROM SPE_EINVOICEDETAIL where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR and serieNumero = @SERIENUMERO and tipoDocumento = @TIPODOCUMENTO AND numeroOrdenItem = @NUMEROORDENITEM
 
		INSERT INTO SPE_EINVOICEDETAIL(numeroDocumentoEmisor, numeroOrdenItem, serieNumero, tipoDocumento, tipoDocumentoEmisor, cantidad, codigoProducto, codigoRazonExoneracion,
		descripcion, importeDescuento, importeTotalSinImpuesto, importeUnitarioConImpuesto, importeUnitarioSinImpuesto, codigoImporteReferencial, importeReferencial, unidadMedida,
		codigoImporteUnitarioConImpues, importeIgv, importeIsc, importeCargo, codigoProductoSUNAT, montoBaseIgv, tasaIGV, importeTotalImpuestos, 
		importeBaseDescuento, factorDescuento, codigoAuxiliar250_1, codigoAuxiliar250_2, codigoAuxiliar250_3, textoAuxiliar250_1, textoAuxiliar250_2, textoAuxiliar250_3)
		SELECT @NUMERODOCUMENTOEMISOR, 
		@numeroOrdenItem, 
		@serieNumero, 
		@tipoDocumento, 
		@TIPODOCUMENTOEMISOR, 
		@cantidad, 
		@codigoProducto, 
		@CodigoRazonExoneracion,
		@descripcion, 
		case when isnull(@importeDescuento, '') = '' then NULL else @importeDescuento end, 
		@importeTotalSinImpuesto, 
		@importeUnitarioConImpuesto, 
		@importeUnitarioSinImpuesto, 
		case when isnull(@importereferencial, '') in ('', '0', '0.00', ' ') then NULL else @codigoImporteReferencial end, 
		case when isnull(@importereferencial, '') in ('', '0', '0.00', ' ') then NULL else @importereferencial end, 
		@unidadMedida,
		@codigoImporteUnitarioConImpuesto, 
		@ImporteIGV, 
		CASE WHEN CONVERT(FLOAT, ISNULL(@ImporteISC, 0)) = 0 THEN NULL ELSE @ImporteISC END,
		case when isnull(@importeCargo, '') = '' then NULL else @importeCargo end,
		case when isnull(@codigoproductoSUNAT, '') = '' then NULL else @codigoproductoSUNAT end, 
		@montoBaseIgv, 
		@tasaIGV, 
		@importeTotalImpuestos, 
		case when isnull(@importeBaseDescuento, '') in ('', '0', '0.00', ' ') then NULL else @importeBaseDescuento end , 
		case when isnull(@factorDescuento, '') in ('', '0', '0.00', ' ') then NULL else @factorDescuento end ,
		case when isnull(@textoAuxiliar250_1, '') = '' then NULL else '9107' end, 
		case when isnull(@textoAuxiliar250_2, '') = '' then NULL else '8998' end, 
		case when isnull(@textoAuxiliar250_3, '') = '' then NULL else '8100' end, 
		case when isnull(@textoAuxiliar250_1, '') = '' then NULL else @textoAuxiliar250_1 end,
		case when isnull(@textoAuxiliar250_2, '') = '' then NULL else @textoAuxiliar250_2 end,
		case when isnull(@textoAuxiliar250_3, '') = '' then NULL else @textoAuxiliar250_3 end
END