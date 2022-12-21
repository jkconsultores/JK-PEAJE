CREATE PROCEDURE [dbo].[USP_CabeceraFE]
       -- Add the parameters for the stored procedure here
	@NUMERODOCUMENTOEMISOR NVARCHAR(20),  -- Aquí pones tu número de RUC
	@SERIENUMERO NVARCHAR(13), -- Es el número del documento ejem: F001-00000001, no olvidar que debe comenzar con F o B en la serie igual al ejemplo
	@TIPODOCUMENTO NVARCHAR(2), -- 01 para factura, 03 para boleta, 07 para NC, 08 para ND
	@TIPODOCUMENTOEMISOR NVARCHAR(1), -- Ahí envía el texto “6” (seis) 
	@BL_ESTADOREGISTRO NVARCHAR(1), -- poner la letra “N” en mayúsculas
	@BL_REINTENTO INT, -- poner el numero 0 (cero)
	@BL_ORIGEN NVARCHAR(1), -- poner la letra “W” 
	@BL_HASFILERESPONSE INT, --poner el numero 0 (cero)
	@CORREOADQUIRIENTE NVARCHAR(100), --correo electrónico para enviar la notificación del documento electrónico
	@CORREOEMISOR NVARCHAR(100), -- un correo emisor puedes poner “-“ (guion) si no deseas usar ninguno
	@DEPARTAMENTOEMISOR NVARCHAR(30), --El departamento “LIMA” por ejemplo
	@DIRECCIONEMISOR NVARCHAR(100), --Dirección del Emisor
	@DISTRITOEMISOR NVARCHAR(30), -- Distrito del Emisor
	@FECHAEMISION NVARCHAR(10), --Fecha de emisión del documento en formato “YYYY-MM-DD”
	@NOMBRECOMERCIALEMISOR NVARCHAR(100), -- Nombre comercial
	@NUMERODOCUMENTOADQUIRIENTE NVARCHAR(50), -- Numero de documento del adquiriente, RUC o DNI según sea necesario
	@PAISEMISOR NVARCHAR(20), -- Poner “PE”
	@PROVINCIAEMISOR NVARCHAR(30), -- Provincia del emisor
	@RAZONSOCIALADQUIRIENTE NVARCHAR(100), --Razón social o nombre del adquiriente
	@RAZONSOCIALEMISOR NVARCHAR(100), -- Razón social del emisor
	@serieNumeroAfectado nvarchar(13), -- Número del documento afectado en el caso de NC o ND
	@codigoLeyenda_1 NVARCHAR(4), -- poner el número “1000”
	@textoLeyenda_1 NVARCHAR(200), -- poner en texto el monto del documento debe incluir el texto “Son:” y luego el monto
	@tipoDocumentoAdquiriente NVARCHAR(1), --  si es empresa va “6” si es persona natural va “1” si es exportación va “0” (cero)
	@tipoMoneda NVARCHAR(3), --“PEN” o “USD”
	@totalIGV NVARCHAR(15), -- igv del documento 2 decimales
	@totalISC NVARCHAR(15), --total isc del documento 2 decimales
	@totalOtrosCargos NVARCHAR(15), -- total otros cargos 2 decimales
	@totalOtrosTributos NVARCHAR(15), -- total de otros tributos 2 decimales
	@totalValorVentaNetoOpExonerada NVARCHAR(15), -- total ventas neta de operaciones exoneradas
	@totalValorVentaNetoOpGratuitas NVARCHAR(15), --total ventas neta de operaciones gratuitas
	@totalValorVentaNetoOpGravadas NVARCHAR(15), --total ventas neta de operaciones gravadas 
	@totalValorVentaNetoOpNoGravada NVARCHAR(15), --total ventas neta de operaciones No gravadas
	@totalvalorVentaNetoOpExporta NVARCHAR(15), -- total ventas neta de Exportacion
	@totalVenta NVARCHAR(15), -- Total de la venta
	@ubigeoEmisor NVARCHAR(6), --codigo ubigeo (ver códigos de ubigeo sunat 150101 es lima centro ejem)
	@urbanizacion NVARCHAR(25), --poner “-“ si no aplica
	@tipoDocumentoAfectado Nvarchar(2), -- si es NC o ND se indica 01 si afecta una factura, 03 si afecta una boleta
	@MotivoNCND as Nvarchar(500), -- si es NC o ND va aquí el motivo
	@TipoNCND as Nvarchar(3), -- Tipo según tabla Excel de la NC o ND
	@tipocambio Nvarchar(20), -- tipo de cambio, mandatorio si es boleta en dólares
	@direccionAdquiriente nvarchar(200),
	@totalImpuestos nvarchar(15), -- Total de Impuestos
	@codigoAuxiliar40_1 nvarchar(15), -- Poner 9011 
	@textoAuxiliar40_1 nvarchar(40), -- Poner 18%
	@tipoOperacion nvarchar(4), -- Poner 0101, Catalogo 51
	@horaEmision nvarchar(8), -- formato hh:mm:ss
	@codigoLocalAnexoEmisor nvarchar(15),  -- Codigo SUNAT del local, en su defecto poner 0000
	@GUIAREMISION NVARCHAR(15),
	@ORDENCOMPRA NVARCHAR(1000),
	@TIPOGUIAREMISION NVARCHAR(2),
	@formapago nvarchar(100),
	@ubigeoAdquiriente nvarchar(30),
	@urbanizacionAdquiriente nvarchar(30),
	@provinciaAdquiriente nvarchar(30),
	@departamentoAdquiriente nvarchar(30),
	@distritoAdquiriente nvarchar(30),
	@paisAdquiriente nvarchar(30),
	@codigoDescuento nvarchar(30),
	@montoBaseDescuentoGlobal nvarchar(15), -- monto base del descuento, monto previo al descuento (validar con Bizlinks)
	@porcentajeDsctoGlobal nvarchar(4),
	@descuentosGlobales nvarchar(15),
	@TOTALDESCUENTOS NVARCHAR(15), --  Total del descuento  en 2 decimales
	@CODIGODETRACCION NVARCHAR(15),
	@PORCENTAJEDETRACCION NVARCHAR(15), -- Porcentaje de detracción en 2 decimales , sino aplica poner NULL
	@TOTALDETRACCION NVARCHAR(15), -- Total de la detracción en 2 decimales
	@BANCONACION NVARCHAR(100),
	@CODIGOFORMAANTICIPO NVARCHAR(10),
	@PORCENTAJEPERCEPCION NVARCHAR(15), --Porcentaje de percepción en 2 decimales, si no aplica poner NULL
	@TOTALVENTACONPERCEPCION NVARCHAR(15),
	@BASEIMPONIBLEPERCEPCION NVARCHAR(15), -- SI tiene percepción aquí viene el monto del documento total , ponerlo en sin comas y en 2 decimales
	@REGIMENPERCEPCION NVARCHAR(15),
	@TOTALPERCEPCION NVARCHAR(15),
	@TOTALRETENCION NVARCHAR(15),
	@PORCENTAJERETENCION NVARCHAR(15), -- Porcentaje de retención en 2 decimales, si no aplica poner NULL
	@totalDocumentoAnticipo NVARCHAR(15), -- Total de anticipos
	@totaldsctoglobalesanticipo NVARCHAR(15), -- Total del descuento anticipo con IGV
	@porcentajeDsctoGlobalAnticipo NVARCHAR(15), -- Porcentaje del anticipo
	@codigoSerieNumeroAfectado AS NVARCHAR(15),
	@textoleyenda_2 AS NVARCHAR(100),
	@facturaPagoNegociable Nvarchar(1), -- Debe ir 0 = contado, 1 = crédito
	@montoNetoPendiente NVARCHAR(15), -- Si es al crédito va el valor que pasa al crédito (total venta menos detracciones y pagos contado)
	@montoPagoCuota1 NVARCHAR(15), --  Va el monto de la cuota Nro 1 del crédito si existe sino en blanco
	@montoPagoCuota2 NVARCHAR(15), --  Va el monto de la cuota Nro 2 del crédito si existe sino en blanco
	@montoPagoCuota3 NVARCHAR(15), --  Va el monto de la cuota Nro 3 del crédito si existe sino en blanco
	@montoPagoCuota4 NVARCHAR(15), --  Va el monto de la cuota Nro 4 del crédito si existe sino en blanco
	@montoPagoCuota5 NVARCHAR(15), --  Va el monto de la cuota Nro 5 del crédito si existe sino en blanco
	@montoPagoCuota6 NVARCHAR(15), --  Va el monto de la cuota Nro 6 del crédito si existe sino en blanco
	@montoPagoCuota7 NVARCHAR(15), --  Va el monto de la cuota Nro 7 del crédito si existe sino en blanco
	@montoPagoCuota8 NVARCHAR(15), --  Va el monto de la cuota Nro 8 del crédito si existe sino en blanco
	@montoPagoCuota9 NVARCHAR(15), --  Va el monto de la cuota Nro 9 del crédito si existe sino en blanco
	@montoPagoCuota10 NVARCHAR(15), --  Va el monto de la cuota Nro 10 del crédito si existe sino en blanco
	@montoPagoCuota11 NVARCHAR(15),--  Va el monto de la cuota Nro 11 del crédito si existe sino en blanco
	@montoPagoCuota12 NVARCHAR(15), --  Va el monto de la cuota Nro 12 del crédito si existe sino en blanco
	@fechaPagoCuota1 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 1 del crédito si existe sino en blanco
	@fechaPagoCuota2 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 2 del crédito si existe sino en blanco
	@fechaPagoCuota3 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 3 del crédito si existe sino en blanco
	@fechaPagoCuota4 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 4 del crédito si existe sino en blanco
	@fechaPagoCuota5 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 5 del crédito si existe sino en blanco
	@fechaPagoCuota6 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 6 del crédito si existe sino en blanco
	@fechaPagoCuota7 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 7 del crédito si existe sino en blanco
	@fechaPagoCuota8 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 8 del crédito si existe sino en blanco
	@fechaPagoCuota9 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 9 del crédito si existe sino en blanco
	@fechaPagoCuota10 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 10 del crédito si existe sino en blanco
	@fechaPagoCuota11 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 11 del crédito si existe sino en blanco
	@fechaPagoCuota12 NVARCHAR(15), --  Va la fecha de pago de la cuota Nro 12 del crédito si existe sino en blanco
	@textoAuxiliar250_1 NVARCHAR(250),
	@textoAuxiliar250_2 NVARCHAR(250),
	@textoAuxiliar250_3 NVARCHAR(250),
	@textoAuxiliar250_4 NVARCHAR(250),
	@textoAuxiliar250_5 NVARCHAR(250),
	@textoAuxiliar250_6 NVARCHAR(250),
	@textoAuxiliar250_7 NVARCHAR(250),
	@textoAuxiliar250_8 NVARCHAR(250),
	@textoAuxiliar250_9 NVARCHAR(250),
	@textoAuxiliar250_10 NVARCHAR(250),
	@textoAuxiliar250_11 NVARCHAR(250),
	@textoAuxiliar250_12 NVARCHAR(250),
	@textoAuxiliar250_13 NVARCHAR(250),
	@textoAuxiliar250_14 NVARCHAR(250),
	@textoAuxiliar250_15 NVARCHAR(250),
	@textoAuxiliar250_16 NVARCHAR(250),
	@textoAuxiliar250_17 NVARCHAR(250),
	@textoAuxiliar250_18 NVARCHAR(250),
	@textoAuxiliar250_19 NVARCHAR(250),
	@textoAuxiliar250_20 NVARCHAR(250),
	@textoAuxiliar500_1 NVARCHAR(250),
	@textoAuxiliar500_2 NVARCHAR(250),
	@textoAuxiliar500_3 NVARCHAR(250),
	@textoAuxiliar500_4 NVARCHAR(250),
	@montoBaseRetencion NVARCHAR(100), -- solo si tiene retencion
	@fechavencimiento NVARCHAR(20), -- fecha de vencimiento
	@totalvalorventa NVARCHAR(13),
	@totalprecioventa NVARCHAR(13)
AS BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON;

        delete from SPE_EINVOICEHEADER where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR AND serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO
        delete from SPE_EINVOICEDETAIL where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR AND serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO
        delete from SPE_EINVOICEHEADER_ADD where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR AND serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO
        delete from SPE_EINVOICE_ANTICIPO where numero_Documento_Emisor = @NUMERODOCUMENTOEMISOR AND serie_Numero = @SERIENUMERO AND tipo_Documento = @TIPODOCUMENTO

             
        set @CORREOADQUIRIENTE = '-'  -- ESTO SOLO SE USA PARA PRUEBAS PARA NO ENVIAR EL CORREO AL CLIENTE

		INSERT INTO SPE_EINVOICEHEADER(numeroDocumentoEmisor, serieNumero, tipoDocumento, tipoDocumentoEmisor, bl_estadoRegistro, bl_reintento, bl_origen, bl_hasFileResponse, 
		correoAdquiriente, correoEmisor, departamentoEmisor, direccionemisor, distritoemisor, fechaemision, nombreComercialEmisor, numerodocumentoadquiriente, paisemisor,
		provinciaemisor, razonsocialadquiriente, razonsocialemisor, numeroDocumentoReferenciaPrinc, codigoleyenda_1, textoleyenda_1, tipodocumentoadquiriente, tipomoneda,
		totalIgv, totalIsc, totalOtrosCargos, totalOtrosTributos, totalValorVentaNetoOpExonerada, totalValorVentaNetoOpGratuitas, totalvalorventanetoopgravadas,
		totalvalorventanetoopnogravada, totalvalorventanetoopExporta, totalventa, ubigeoemisor, urbanizacion, tipoDocumentoReferenciaPrincip, MotivoDocumento,
		totalImpuestos, codigoAuxiliar40_1, textoAuxiliar40_1, tipoOperacion, horaEmision, codigolocalAnexoEmisor,numeroDocumentoReferencia_1,
		tipoReferencia_1, montoBaseDescuentoGlobal,  porcentajeDsctoGlobal,  totalDescuentos, descuentosGlobales, codigoDetraccion, porcentajeDetraccion,
		totalDetraccion, numeroCtaBancoNacion, porcentajePercepcion, totalventaconpercepcion, baseimponiblepercepcion, totalpercepcion, 
		totaldocumentoanticipo, totalDsctoGlobalesAnticipo, porcentajeDsctoGlobalAnticipo, codigoAuxiliar250_1, codigoAuxiliar250_2, codigoAuxiliar250_3, codigoAuxiliar250_4, codigoAuxiliar250_5, codigoAuxiliar250_6, 
		codigoAuxiliar250_7, codigoAuxiliar250_8, codigoAuxiliar250_9, codigoAuxiliar250_10, codigoAuxiliar250_11, codigoAuxiliar250_12, codigoAuxiliar250_13, 
		codigoAuxiliar250_14, codigoAuxiliar250_15, codigoAuxiliar250_16, codigoAuxiliar250_17, codigoAuxiliar250_18, codigoAuxiliar250_19,  
		codigoAuxiliar250_20,codigoAuxiliar500_1,codigoAuxiliar500_2,codigoAuxiliar500_3,codigoAuxiliar500_4,
		textoAuxiliar250_1, textoAuxiliar250_2, textoAuxiliar250_3, textoAuxiliar250_4, textoAuxiliar250_5, textoAuxiliar250_6, 
		textoAuxiliar250_7, textoAuxiliar250_8, textoAuxiliar250_9, textoAuxiliar250_10, textoAuxiliar250_11, textoAuxiliar250_12, 
		textoAuxiliar250_13, textoAuxiliar250_14, textoAuxiliar250_15, textoAuxiliar250_16, textoAuxiliar250_17, textoAuxiliar250_18, 
		textoAuxiliar250_19, textoAuxiliar250_20, textoAuxiliar500_1, textoAuxiliar500_2,textoAuxiliar500_3,textoAuxiliar500_4,
		codigoSerieNumeroAfectado, totalvalorventa, totalprecioventa)
		SELECT @numeroDocumentoEmisor, @serieNumero, @tipoDocumento, @tipoDocumentoEmisor, @bl_estadoRegistro, @bl_reintento, @bl_origen, @bl_hasFileResponse, 
		case when isnull(@correoAdquiriente, '') in ('', ' ') then '-' else @CORREOADQUIRIENTE end, 
		@correoEmisor, @departamentoEmisor, @direccionemisor, @distritoemisor, @fechaemision, 
		case when isnull(@nombreComercialEmisor, '') = '' then NULL else @nombreComercialEmisor end, @numerodocumentoadquiriente, @paisemisor,
		@provinciaemisor, @razonsocialadquiriente, @razonsocialemisor, 
		CASE WHEN ISNULL(@serienumeroafectado, '') = '' THEN NULL ELSE @serienumeroafectado END , 
		@codigoleyenda_1, @textoleyenda_1, @tipodocumentoadquiriente, @tipomoneda,
		CASE WHEN ISNULL(@totalIgv, '') in ('', ' ') THEN NULL ELSE @totalIgv END, 
		CASE WHEN ISNULL(@totalIsc, '') in ('', ' ', '0.00', '0')  THEN NULL ELSE @totalIsc END, 
		CASE WHEN ISNULL(@totalOtrosCargos, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalOtrosCargos END , 
		CASE WHEN ISNULL(@totalOtrosTributos, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalOtrosTributos END, 
		CASE WHEN ISNULL(@totalValorVentaNetoOpExonerada, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalValorVentaNetoOpExonerada END ,
		CASE WHEN ISNULL(@totalValorVentaNetoOpGratuitas, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalValorVentaNetoOpGratuitas END , 
		CASE WHEN ISNULL(@totalvalorventanetoopgravadas, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalvalorventanetoopgravadas END ,
		CASE WHEN ISNULL(@totalvalorventanetoopnogravada, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalvalorventanetoopnogravada END , 
		CASE WHEN ISNULL(@totalvalorventanetoopExporta, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalvalorventanetoopExporta END , 
		CASE WHEN ISNULL(@totalventa, '') = '' THEN NULL ELSE @totalventa END , 
		case when isnull(@ubigeoemisor, '') = '' then '510101' else @ubigeoEmisor end,
		CASE WHEN ISNULL(@urbanizacion, '') = '' then '-' else @urbanizacion end, 
		CASE WHEN ISNULL(@tipodocumentoAfectado, '') = '' THEN NULL ELSE @tipodocumentoAfectado END , 
		CASE WHEN ISNULL(@MOTIVONCND, '') = '' THEN NULL ELSE @MOTIVONCND END ,
		CASE WHEN ISNULL(@totalImpuestos, '') = '' THEN NULL ELSE @totalImpuestos END , 
		@codigoAuxiliar40_1, @textoAuxiliar40_1, @tipoOperacion, @horaEmision, @codigolocalAnexoEmisor,
		CASE WHEN ISNULL(@GUIAREMISION, '') = '' THEN NULL ELSE @GUIAREMISION END ,
		CASE WHEN ISNULL(@TIPOGUIAREMISION, '') = '' THEN NULL ELSE @TIPOGUIAREMISION END , 
		CASE WHEN ISNULL(@montoBaseDescuentoGlobal, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @montoBaseDescuentoGlobal END , 
		CASE WHEN ISNULL(@porcentajeDsctoGlobal, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @porcentajeDsctoGlobal END , 
		CASE WHEN ISNULL(@descuentosGlobales, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @descuentosGlobales END , 
		CASE WHEN ISNULL(@totalDescuentos, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalDescuentos END , 
		CASE WHEN ISNULL(@codigoDetraccion, '') = '' THEN NULL ELSE @codigoDetraccion END , 
		CASE WHEN ISNULL(@porcentajeDetraccion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @porcentajeDetraccion END ,
		CASE WHEN ISNULL(@totalDetraccion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalDetraccion END , 
		CASE WHEN ISNULL(@BancoNacion, '') = '' THEN NULL ELSE @BancoNacion END , 
		CASE WHEN ISNULL(@porcentajePercepcion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @porcentajePercepcion END , 
		CASE WHEN ISNULL(@totalventaconpercepcion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalventaconpercepcion END , 
		CASE WHEN ISNULL(@baseimponiblepercepcion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @baseimponiblepercepcion END , 
		CASE WHEN ISNULL(@totalpercepcion, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalpercepcion END , 
		CASE WHEN ISNULL(@totalDocumentoAnticipo, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totalDocumentoAnticipo END ,
		CASE WHEN ISNULL(@totaldsctoglobalesanticipo, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @totaldsctoglobalesanticipo END ,
		CASE WHEN ISNULL(@porcentajeDsctoGlobalAnticipo, '') in ('', ' ', '0', '0.00')  THEN NULL ELSE @porcentajeDsctoGlobalAnticipo END ,
		case when isnull(@textoAuxiliar250_1, '') = '' then NULL else '9157' end,
		case when isnull(@textoAuxiliar250_2, '') = '' then NULL else '9660' end,
		case when isnull(@textoAuxiliar250_3, '') = '' then NULL else '9935' end,
		case when isnull(@textoAuxiliar250_4, '') = '' then NULL else '9032' end,
		case when isnull(@textoAuxiliar250_5, '') = '' then NULL else '9218' end,
		case when isnull(@textoAuxiliar250_6, '') = '' then NULL else '9412' end,
		case when isnull(@textoAuxiliar250_7, '') = '' then NULL else '8044'  end,
		case when isnull(@textoAuxiliar250_8, '') = '' then NULL else '9484' end,
		case when isnull(@textoAuxiliar250_9, '') = '' then NULL else '8319' end,
		case when isnull(@textoAuxiliar250_10, '') = '' then NULL else '9092' end,
		case when isnull(@textoAuxiliar250_11, '') = '' then NULL else '9143' end,
		case when isnull(@textoAuxiliar250_12, '') = '' then NULL else '8570' end,
		case when isnull(@textoAuxiliar250_13, '') = '' then NULL else '9597' end,
		case when isnull(@textoAuxiliar250_14, '') = '' then NULL else '9994' end,
		case when isnull(@textoAuxiliar250_15, '') = '' then NULL else '9568' end,
		case when isnull(@textoAuxiliar250_16, '') = '' then NULL else '9841'  end,
		case when isnull(@textoAuxiliar250_17, '') = '' then NULL else '9569' end,
		case when isnull(@textoAuxiliar250_18, '') = '' then NULL else '9839' end,
		case when isnull(@textoAuxiliar250_19, '') = '' then NULL else '9420' end,
		case when isnull(@textoAuxiliar250_20, '') = '' then NULL else '8274' end,
		case when isnull(@textoAuxiliar500_1, '') = '' then NULL else '8275' end,
		case when isnull(@textoAuxiliar500_2, '') = '' then NULL else '8292' end,
		case when isnull(@textoAuxiliar500_3, '') = '' then NULL else '9115' end,
		case when isnull(@textoAuxiliar500_4, '') = '' then NULL else '8046' end,
		case when isnull(@textoAuxiliar250_1, '') = '' then NULL else @textoAuxiliar250_1 end,
		case when isnull(@textoAuxiliar250_2, '') = '' then NULL else @textoAuxiliar250_2 end,
		case when isnull(@textoAuxiliar250_3, '') = '' then NULL else @textoAuxiliar250_3 end,
		case when isnull(@textoAuxiliar250_4, '') = '' then NULL else @textoAuxiliar250_4 end,
		case when isnull(@textoAuxiliar250_5, '') = '' then NULL else @textoAuxiliar250_5 end,
		case when isnull(@textoAuxiliar250_6, '') = '' then NULL else @textoAuxiliar250_6 end,
		case when isnull(@textoAuxiliar250_7, '') = '' then NULL else @textoAuxiliar250_7 end,
		case when isnull(@textoAuxiliar250_8, '') = '' then NULL else @textoAuxiliar250_8 end,
		case when isnull(@textoAuxiliar250_9, '') = '' then NULL else @textoAuxiliar250_9 end,
		case when isnull(@textoAuxiliar250_10, '') = '' then NULL else @textoAuxiliar250_10 end,
		case when isnull(@textoAuxiliar250_11, '') = '' then NULL else @textoAuxiliar250_11 end,
		case when isnull(@textoAuxiliar250_12, '') = '' then NULL else @textoAuxiliar250_12 end,
		case when isnull(@textoAuxiliar250_13, '') = '' then NULL else @textoAuxiliar250_13 end,
		case when isnull(@textoAuxiliar250_14, '') = '' then NULL else @textoAuxiliar250_14 end,
		case when isnull(@textoAuxiliar250_15, '') = '' then NULL else @textoAuxiliar250_15 end,
		case when isnull(@textoAuxiliar250_16, '') = '' then NULL else @textoAuxiliar250_16 end,
		case when isnull(@textoAuxiliar250_17, '') = '' then NULL else @textoAuxiliar250_17 end,
		case when isnull(@textoAuxiliar250_18, '') = '' then NULL else @textoAuxiliar250_19 end,
		case when isnull(@textoAuxiliar250_19, '') = '' then NULL else @textoAuxiliar250_19 end,
		case when isnull(@textoAuxiliar250_20, '') = '' then NULL else @textoAuxiliar250_20 end,
		case when isnull(@textoAuxiliar500_1, '') = '' then NULL else @textoAuxiliar500_1 end,
		case when isnull(@textoAuxiliar500_2, '') = '' then NULL else @textoAuxiliar500_2 end,
		case when isnull(@textoAuxiliar500_3, '') = '' then NULL else @textoAuxiliar500_3 end,
		case when isnull(@textoAuxiliar500_4, '') = '' then NULL else @textoAuxiliar500_4 end,
		case when isnull(@serieNumeroAfectado, '') = '' then NULL else @codigoSerieNumeroAfectado end,
		@totalvalorventa, @totalprecioventa

        IF @TOTALRETENCION NOT IN ('', ' ', '0', '0.00') 
        BEGIN
            Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave,                                                    valor)
            SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'importeOpeRetencion',                                                                                 @montoBaseRetencion

            Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave,                                                    valor)
            SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'porcentajeRetencion',                                                                                    @PORCENTAJERETENCION

            Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave,                                                    valor)
            SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'importeRetencion',                                                                                                         @TOTALRETENCION

        END

       IF ISNULL(@ORDENCOMPRA, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'ordenCompra', @ordencompra

       IF ISNULL(@direccionAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'direccionAdquiriente', @direccionAdquiriente

       IF ISNULL(@ubigeoAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'ubigeoAdquiriente', @ubigeoAdquiriente

       IF ISNULL(@urbanizacionAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'urbanizacionAdquiriente', @urbanizacionAdquiriente

       IF ISNULL(@provinciaAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'provinciaAdquiriente', @provinciaAdquiriente
       
       IF ISNULL(@departamentoAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'departamentoAdquiriente', @departamentoAdquiriente

       IF ISNULL(@distritoAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'distritoAdquiriente', @distritoAdquiriente

       IF ISNULL(@paisAdquiriente, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'paisAdquiriente', @paisAdquiriente
       
       IF ISNULL(@textoAuxiliar250_3, '') <> '' 
              Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
              SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaVencimiento', @textoAuxiliar250_3

		IF ISNULL(@TOTALDETRACCION, '') not in ('', ' ', '0', '0.00') 
		begin
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'formaPago', '001'

            UPDATE SPE_EINVOICEHEADER SET codigoLeyenda_2 = '2006', textoLeyenda_2 = 'Operación sujeta a detracción'  where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR AND serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO
        end

		if isnull(@TOTALPERCEPCION , '') in ('', ' ' , '0', '0.00', '0.000')
			update SPE_EINVOICEHEADER SET  totalPercepcion = NULL, baseImponiblePercepcion = NULL, porcentajePercepcion = NULL, totalVentaConPercepcion = NULL, regimenPercepcion = NULL where numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR AND serieNumero = @SERIENUMERO AND tipoDocumento = @TIPODOCUMENTO

		IF ISNULL(@facturaPagoNegociable, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'formaPagoNegociable', @facturaPagoNegociable
                                               
		IF ISNULL(@montoNetoPendiente, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoNetoPendiente', @montoNetoPendiente

		IF ISNULL(@montoPagoCuota1, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota1', @montoPagoCuota1

		IF ISNULL(@montoPagoCuota2, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota2', @montoPagoCuota2

		IF ISNULL(@montoPagoCuota3, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota3', @montoPagoCuota3

		IF ISNULL(@montoPagoCuota4, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota4', @montoPagoCuota4

		IF ISNULL(@montoPagoCuota5, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota5', @montoPagoCuota5

		IF ISNULL(@montoPagoCuota6, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota6', @montoPagoCuota6

		IF ISNULL(@montoPagoCuota7, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota7', @montoPagoCuota7

		IF ISNULL(@montoPagoCuota8, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota8', @montoPagoCuota8

		IF ISNULL(@montoPagoCuota9, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota9', @montoPagoCuota9

		IF ISNULL(@montoPagoCuota10, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota10', @montoPagoCuota10

		IF ISNULL(@montoPagoCuota11, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota11', @montoPagoCuota11

		IF ISNULL(@montoPagoCuota12, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'montoPagoCuota12', @montoPagoCuota12

		IF ISNULL(@fechaPagoCuota1, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota1', @fechaPagoCuota1

		IF ISNULL(@fechaPagoCuota2, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota2', @fechaPagoCuota2

		IF ISNULL(@fechaPagoCuota3, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota3', @fechaPagoCuota3

		IF ISNULL(@fechaPagoCuota4, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota4', @fechaPagoCuota4

		IF ISNULL(@fechaPagoCuota5, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota5', @fechaPagoCuota5

		IF ISNULL(@fechaPagoCuota6, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota6', @fechaPagoCuota6

		IF ISNULL(@fechaPagoCuota7, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota7', @fechaPagoCuota7

		IF ISNULL(@fechaPagoCuota8, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota8', @fechaPagoCuota8

		IF ISNULL(@fechaPagoCuota9, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota9', @fechaPagoCuota9

		IF ISNULL(@fechaPagoCuota10, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota10', @fechaPagoCuota10

		IF ISNULL(@fechaPagoCuota11, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota11', @fechaPagoCuota11

		IF ISNULL(@fechaPagoCuota12, '') <> ''
			Insert into SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serieNumero, tipoDocumento, clave, valor)
			SELECT @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'fechaPagoCuota12', @fechaPagoCuota12

		if isnull(@totalValorVentaNetoOpGratuitas, '') not in ('', ' ', '0', '0.00')
		BEGIN
			update spe_einvoiceheader set codigoleyenda_2 = '1002', textoleyenda_2 = 'TRANSFERENCIA GRATUITA DE UN BIEN O SERVICIO PRESTADO FRATUITAMENTE'
			where serieNumero = @SERIENUMERO and tipodocumento = @TIPODOCUMENTO and numeroDocumentoEmisor = @NUMERODOCUMENTOEMISOR
                
			INSERT INTO SPE_EINVOICEHEADER_ADD (tipoDocumentoEmisor, numeroDocumentoEmisor, serienumero, tipodocumento, clave, valor)
			select @TIPODOCUMENTOEMISOR, @NUMERODOCUMENTOEMISOR, @SERIENUMERO, @TIPODOCUMENTO, 'totalTributosOpeGratuitas', @totalIGV
		END

 	-- YA NO SE USA
    --    IF @TIPODOCUMENTO = '03' OR @tipoDocumentoAfectado = '03'
    --    BEGIN

    --           IF NOT EXISTS(SELECT * FROM SPE_SUMMARY_ITEM I INNER JOIN SPE_SUMMARYHEADER H ON I.RESUMENID = H.RESUMENID WHERE
    --           I.numeroCorrelativo = @SERIENUMERO AND I.NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR AND I.tipoDocumento = @TIPODOCUMENTO
    --           AND H.BL_ESTADOREGISTRO = 'N' )
    --           BEGIN

    --                  DECLARE @FECHA VARCHAR(10)
    --                  DECLARE @FECHARESUMEN VARCHAR(8)
    --                  DECLARE @RESUMENCOUNT INT
    --                  DECLARE @RA NVARCHAR(15)

    --                  SET @FECHA = (SELECT CONVERT(char(10), GetDate(),126))
    --                  SET @FECHARESUMEN = REPLACE(@FECHA, '-', '')

    --                  SET @RESUMENCOUNT = (SELECT ISNULL(COUNT(*), 0) FROM SPE_SUMMARYHEADER WHERE fechaGeneracionResumen = @FECHA) + 1

    --                  SET @RA = 'RC-' + @FECHARESUMEN + '-' + RIGHT('000' + CONVERT(NVARCHAR(3), @RESUMENCOUNT), 3)

    --                  insert into SPE_SUMMARYHEADER 
    --                  select numeroDocumentoEmisor, @RA, 6, '-', fechaEmision, @FECHA , '1', 
    --                  razonSocialEmisor, 'RC', 'N', 0, 'W', NULL, NULL, NULL, NULL, NULL from SPE_EINVOICEHEADER 
    --                  where serieNumero = @SERIENUMERO AND NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR AND tipoDocumento = @TIPODOCUMENTO

    --                  ---------- PONER EL TOTALIGV O TOTALISC EN 0 SI ES NULL

                
    --                 INSERT INTO SPE_SUMMARY_ITEM
    --                 SELECT numeroDocumentoEmisor, '6', @RA, 1, tipoDocumento, serieNumero, tipoDocumentoAdquiriente, numeroDocumentoAdquiriente,
    --                 numeroDocumentoReferenciaPrinc, tipoDocumentoReferenciaPrincip, '1', tipoMoneda, CONVERT(FLOAT, round(isnull(totalIgv, '0'), 2))  , CONVERT(FLOAT, round(isnull(totalIsc, '0'), 2)) , 
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalOtrosCargos, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalOtrosCargos, 0), 2)) END, 
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalOtrosTributos, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalOtrosTributos, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalValorVentaNetoOpExonerada, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalValorVentaNetoOpExonerada, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalValorVentaNetoOpGratuitas, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalValorVentaNetoOpGratuitas, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalValorVentaNetoOpGravadas, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalValorVentaNetoOpGravadas, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalValorVentaNetoOpNoGravada, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalValorVentaNetoOpNoGravada, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalVenta, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalVenta, 0), 2)) END, NULL, 
    --                         case when isnull(regimenPercepcion, '') = '' and isnull(porcentajepercepcion, '') <> '' then '01' else regimenPercepcion end,
    --                         porcentajePercepcion,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalPercepcion, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalPercepcion, 0), 2)) END,
    --                         CASE WHEN CONVERT(FLOAT, ISNULL(totalVentaConPercepcion, 0)) = 0 THEN NULL ELSE  CONVERT(FLOAT, round(ISNULL(totalVentaConPercepcion, 0), 2)) END,
    --                 CASE WHEN CONVERT(FLOAT, ISNULL(totalPercepcion, 0)) = 0      then NULL else CONVERT(float, round(isnull(totalVenta, 0), 2)) end, NULL, NULL, NULL
    --                 from SPE_EINVOICEHEADER where serieNumero = @SERIENUMERO AND NUMERODOCUMENTOEMISOR = @NUMERODOCUMENTOEMISOR AND tipoDocumento = @TIPODOCUMENTO
              
    --             END
    --    END    
END