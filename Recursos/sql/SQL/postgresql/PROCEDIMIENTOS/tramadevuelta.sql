CREATE OR REPLACE FUNCTION public.usp_tramadevuelta(
	numero_documento_emisor character,
	serie_numero character,
	tipo_documento character)
    RETURNS TABLE(_mensaje text) 
    LANGUAGE 'plpgsql'

AS $BODY$
declare estadoTemp CHARACTER(1);
begin
    -- return query
    --     select
    --         'OK|20522547957|03|B511|00794609|1.48|9.70|2022-05-05|0|-|OFS5f5oeHo0QFXQaHSRBNNn49W4=|JI1XcU0++CFQJVXYoP+Hola4TGH6rLknx2kcQOq6Dbgk1Tdj6jl6B4GQ8Jd3waFSr81rRZR+qIVQb3jLuFx3bsV/4yFtVnTwtKafBID9//NjJd8z/6XbPuj/TxLPkzZFD54TLAOHmPd9nB8I185zJoTaldVLavJo7jSbs1vG/eftAcEK8iTaiPkeMMKOjkVDVGVUkdqUhudTqVBnkhaH+Uw/8lTZa5WnXMYwTRMML5OqDhfAGXyXBa5kVZlvySVt1kxh0lduq/y5GaT4d3z4Qwq5ieZmW6GQvp9EI+e/BlrITLt2eRIBE9K2WSCedujoaRS7IgOCqEOBYvpbkOp5uw==|http://covisol2205.acepta.pe/v01/7AC82432C3649A5117638F953759F4CBB8887678?k=f28cc78c716c8a461e0f1c8caff46e08|PDF|}'
	-- 	from public.spe_einvoice_response;
        
    estadoTemp := (select bl_estadoregistro from spe_einvoiceheader where numerodocumentoemisor = numero_documento_emisor and serienumero = serie_numero);
    if  estadoTemp like 'L' then
        return query
        select 
            cast('OK|' as character(2)) ||
            cast(coalesce( er.numerodocumentoemisor, '' )       as character(20))|| cast('|' as character(2)) ||
            cast(coalesce( er.tipodocumento, '' )               as character(5))|| cast('|' as character(2)) ||
            cast(substring( er.serienumero, 1,4)                as character(5))|| cast('|' as character(2)) ||
            cast(substring( er.serienumero, 6,8)                as character(9))|| cast('|' as character(2)) ||
            cast(coalesce( eh.totalimpuestos,'')                as character(10))|| cast('|' as character(2)) ||
            cast(coalesce( eh.totalprecioventa, '')             as character(10))|| cast('|' as character(2)) ||
            cast(coalesce( eh.fechaemision, '')                 as character(10))|| cast('|' as character(2)) ||
            cast(coalesce( eh.tipodocumentoadquiriente, '')     as character(5))|| cast('|' as character(2)) ||
            cast(coalesce( eh.numerodocumentoadquiriente, '')   as character(20))|| cast('|' as character(2)) ||
            cast(coalesce( er.bl_hashfirma , '')                as character(500))|| cast('|' as character(2)) ||
            cast(coalesce( er.bl_firma , '')                      as character(500))|| cast('|' as character(2)) ||
            cast(coalesce( er.bl_url_pdf, '')                     as character(200))|| cast('|' as character(2)) ||
            cast(('[URL_PDF]|') as character(11)) ||
            cast('' as character(1))

        from public.spe_einvoice_response er
            inner join public.spe_einvoiceheader eh on er.numerodocumentoemisor = eh.numerodocumentoemisor and er.serienumero = eh.serienumero
        where er.numerodocumentoemisor = numero_documento_emisor 
            and er.serienumero = serie_numero
			and er.tipodocumento = tipo_documento;
    end if;
    if estadoTemp like 'E' then
        return query
        select 
            cast('ERROR|[DATOS_DOC]|' as character(18)) || descripcionerror
            from spe_error_log order by fecharegistro desc limit(1);
    end if;
     
    if estadoTemp LIKE 'N' then
        return query
        select
            cast('ERROR|[DATOS_DOC]|NO ESTA ACTIVO EL SERVICIO BIZLINKS.' as character(55))
            from public.spe_einvoice_response;
    end if;
    --FALTA ESLE
end; 
$BODY$;
