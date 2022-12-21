create or replace function public.usp_insertar_tdocumentofe(
	in numero_documento_emisor  character(20),
	in serie_numero 			character(13),
	in tipo_documento 			character(2)
)
returns void
as $$
begin
	

    if exists(
            select * 
                from public.tdocumentofe
            where serie_numero 			= serie_numero 
                and tipo_documento 		= tipo_documento 
                and numerodocumentoemisor = numero_documento_emisor 
        ) then
        null;
    else 
        insert into public.tdocumentofe(
        numerodocumentoemisor, serienumero, tipodocumento)
        values (
                numero_documento_emisor, serie_numero, tipo_documento);
	end if;
end; $$
language 'plpgsql'