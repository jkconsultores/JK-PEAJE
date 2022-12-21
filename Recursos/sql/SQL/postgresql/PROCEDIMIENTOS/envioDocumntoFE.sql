create or replace function public.usp_enviodocumentofe(
	in numero_documento_emisor  character(20),
	in serie_numero 			character(13),
	in tipo_documento 			character(2)
)
returns void
as $$
declare 
	r_a character(15);
begin
	if exists(
			select * 
				from public.spe_einvoiceheader_add 
			where serienumero 			= serie_numero 
				and tipodocumento 		= tipo_documento 
				and numerodocumentoemisor = numero_documento_emisor 
				and clave 					= 'totalTributosOpeGratuitas'
		) then
		update public.spe_einvoiceheader_add 
			set valor = (
				select SUM(cast(importeigv as money))
					from spe_einvoicedetail
				where serienumero 			= serie_numero 
					and tipodocumento 		= tipo_documento
					and numerodocumentoemisor = numero_documento_emisor
			)
		where serienumero 			= serie_numero
			and tipodocumento 		= tipo_documento
			and numerodocumentoemisor = numero_documento_emisor
			and clave 					= 'totalTributosOpeGratuitas';
	end if;

	-- insert statements for procedure here
	update public.spe_einvoiceheader 
		set bl_estadoregistro 	= 'A' 
	where serienumero 			= serie_numero 
		and numerodocumentoemisor = numero_documento_emisor 
		and tipodocumento 		= tipo_documento;

	if (substring(serie_numero,1,1) = 'B') then
		r_a := (
					select max(coalesce(i.resumenid, '')) 
					from public.spe_summary_item i 
						inner join public.spe_summaryheader h on i.resumenid = h.resumenid 
					where i.numerocorrelativo 		= serie_numero 
						and i.numerodocumentoemisor = numero_documento_emisor 
						and i.tipodocumento 		= tipo_documento 
						and h.bl_estadoregistro 	= 'N');

		update public.spe_summaryheader set bl_estadoregistro = 'A' where resumenid = r_a;
	end if;
end; $$
language 'plpgsql'