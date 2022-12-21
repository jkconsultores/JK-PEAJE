CREATE PROCEDURE [dbo].[usp_tramaDevuelta](
	@numero_documento_emisor varchar(20),
	@serie_numero varchar(13),
    @tipo_doc varchar(2)
)
AS
BEGIN
	DECLARE @estadoDoc NVARCHAR(1);
	SET @estadoDoc = (SELECT bl_estadoregistro FROM SPE_EINVOICE_RESPONSE WHERE numerodocumentoemisor = @numero_documento_emisor and serienumero = @serie_numero);

	IF @estadoDoc = 'E'
		SELECT TOP(1)
			CONCAT('ERROR|[DATOS_DOC]|',el.DESCRIPCIONERROR) AS Error
        from spe_error_log el order by fecharegistro desc;

	if @estadoDoc = 'N' 
        select
            'ERROR|[DATOS_DOC]|NO ESTA ACTIVO EL SERVICIO BIZLINKS.' as Error
        from SPE_CANCEL_RESPONSE;

    IF @estadoDoc = 'L'
        IF @tipo_doc = '01'
            SELECT 
                CONCAT( 'OK|',
                        ISNULL( SR.numeroDocumentoEmisor, '' ),'|',
                        ISNULL( SR.tipoDocumentoEmisor, '' ),'|',
                        SUBSTRING( SR.resumenId, 1,2),'|',
                        SR.resumenId,'|',
                        ISNULL(EH.totalImpuestos,''),'|',
                        ISNULL(EH.totalPrecioVenta, ''),'|',
                        ISNULL(EH.fechaEmision, ''),'|',
                        ISNULL(EH.tipoDocumentoAdquiriente, ''),'|',
                        ISNULL(EH.numeroDocumentoAdquiriente, ''),'|',
                        ISNULL( SR.bl_hashFirma , ''),'|',
                        ISNULL( SR.bl_firma , ''),'|',
                        ISNULL( SR.bl_url_pdf, ''),'|',
                        '[URL_PDF]|',
                        --CONCAT( '[URL_PDF]','-', SR.numeroDocumentoEmisor,'-',SR.tipoDocumentoEmisor,'-',SR.resumenId, '.pdf' ),'|',
                        '') AS Firma
            FROM SPE_CANCEL_RESPONSE SR
                INNER JOIN [SPE_EINVOICEHEADER] EH ON SR.numeroDocumentoEmisor = EH.numeroDocumentoEmisor AND SR.resumenId = EH.serieNumero
            WHERE SR.numeroDocumentoEmisor = @numero_documento_emisor
                AND SR.resumenId = @serie_numero;
        ELSE 
            SELECT
                CONCAT( 'OK|',
                        ISNULL( SR.numeroDocumentoEmisor, '' ),'|',
                        ISNULL( SR.tipoDocumentoEmisor, '' ),'|',
                        SUBSTRING( SR.resumenId, 1,2),'|',
                        SR.resumenId,'|',
                        ISNULL(EH.totalImpuestos,''),'|',
                        ISNULL(EH.totalPrecioVenta, ''),'|',
                        ISNULL(EH.fechaEmision, ''),'|',
                        ISNULL(EH.tipoDocumentoAdquiriente, ''),'|',
                        ISNULL(EH.numeroDocumentoAdquiriente, ''),'|',
                        ISNULL( SR.bl_hashFirma , ''),'|',
                        ISNULL( SR.bl_firma , ''),'|',
                        ISNULL( SR.bl_url_pdf, ''),'|',
                        '[URL_PDF]|',
                        --CONCAT( '[URL_PDF]','-', SR.numeroDocumentoEmisor,'-',SR.tipoDocumentoEmisor,'-',SR.resumenId, '.pdf' ),'|',
                        '') AS Firma
            FROM SPE_SUMMARY_RESPONSE SR
                INNER JOIN [SPE_EINVOICEHEADER] EH ON SR.numeroDocumentoEmisor = EH.numeroDocumentoEmisor AND SR.resumenId = EH.serieNumero
            WHERE SR.numeroDocumentoEmisor = @numero_documento_emisor
                AND SR.resumenId = @serie_numero;
END