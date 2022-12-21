create or replace function public.usp_detallefe(
    in numero_documento_emisor              character(20),
    in serie_numero                         character(13),
    in tipo_documento                       character(2),
    in tipo_documento_emisor                character(1),
    in numero_orden_item                    character(4),
    in _cantidad                            character(25),
    in codigo_producto                      character(30),
    in codigo_razon_exoneracion             character(2),
    in _descripcion                         character(1700),
    in importe_descuento                    character(25),
    in importe_total_sin_impuesto           character(15),
    in importe_unitario_con_impuesto        character(25),
    in importe_unitario_sin_impuesto        character(25),
    in codigo_importe_referencial           character(15),
    in importe_referencial                  character(15),
    in unidad_medida                        character(5),
    in codigo_importe_unitario_con_impuesto character(2),
    in importe_igv                          character(15),
    in importe_isc                          character(15),
    in importe_cargo                        character(15),
    in codigo_producto_sunat                character(30),
    in monto_base_igv                       character(15),
    in tasa_ig                              character(15),
    in importe_total_impuestos              character(15),
    in importe_base_descuento               character(15),
    in factor_descuento                     character(4),
    in texto_auxiliar250_1                  character(250),
    in texto_auxiliar250_2                  character(250),
    in texto_auxiliar250_3                  character(250)
)
returns void
as $$
begin
    delete from public.spe_einvoicedetail
        where numerodocumentoemisor   = numero_documento_emisor 
            and serienumero           = serie_numero 
            and tipodocumento         = tipo_documento 
            and numeroordenitem       = numero_orden_item;
    insert into public.spe_einvoicedetail( numerodocumentoemisor, 
                                    numeroordenitem,
                                    serienumero,
                                    tipodocumento,
                                    tipodocumentoemisor,
                                    cantidad,
                                    codigoproducto,
                                    codigorazonexoneracion,
                                    descripcion,
                                    importedescuento, 
                                    importetotalsinimpuesto,
                                    importeunitarioconimpuesto,
                                    importeunitariosinimpuesto,
                                    codigoimportereferencial, 
                                    importereferencial, 
                                    unidadmedida, 
                                    codigoimporteunitarioconimpues, 
                                    importeigv, 
                                    importeisc, 
                                    importecargo, 
                                    codigoproductosunat, 
                                    montobaseigv, 
                                    tasaigv, 
                                    importetotalimpuestos, 
                                    importebasedescuento, 
                                    factordescuento, 
                                    codigoauxiliar250_1, 
                                    codigoauxiliar250_2, 
                                    codigoauxiliar250_3, 
                                    textoauxiliar250_1, 
                                    textoauxiliar250_2, 
                                    textoauxiliar250_3)
		select  numero_documento_emisor, 
                numero_orden_item, 
                serie_numero, 
                tipo_documento, 
                tipo_documento_emisor, 
                _cantidad, 
                codigo_producto, 
                codigo_razon_exoneracion,
                _descripcion, 
                case 
                    when (coalesce(importe_descuento, '') = '') 
                        then null 
                        else importe_descuento 
                end, 
                importe_total_sin_impuesto, 
                importe_unitario_con_impuesto, 
                importe_unitario_sin_impuesto, 
                case 
                    when (coalesce(importe_referencial, '') in ('', '0', '0.00', ' ')) 
                        then null 
                        else '02' 
                end, 
                case 
                    when (coalesce(importe_referencial, '') in ('', '0', '0.00', ' ')) 
                        then null 
                        else importe_referencial
                end, 
                unidad_medida,
                codigo_importe_unitario_con_impuesto, 
                importe_igv, 
                case 
                    --when (cast(coalesce(importe_isc, 0) as decimal) = 0)  
                    when (coalesce(importe_isc, '0') = '0')  
                        then null 
                        else importe_isc 
                end,
                case 
                    when (coalesce(importe_cargo, '') = '') 
                        then null 
                        else importe_cargo 
                end,
                case 
                    when (coalesce(codigo_producto_sunat, '') = '') 
                        then null 
                        else codigo_producto_sunat 
                end, 
                monto_base_igv, 
                tasa_ig, 
                importe_total_impuestos, 
                case 
                    when (coalesce(importe_base_descuento, '') in ('', '0', '0.00', ' ')) 
                        then null 
                        else importe_base_descuento 
                end , 
                case 
                    when (coalesce(factor_descuento, '') in ('', '0', '0.00', ' ')) 
                        then null 
                        else factor_descuento 
                end ,
                case 
                    when (coalesce(texto_auxiliar250_1, '') = '') 
                        then null 
                        else '9107' 
                end, 
                case 
                    when (coalesce(texto_auxiliar250_2, '') = '') 
                        then null 
                        else '8998' 
                end, 
                case 
                    when (coalesce(texto_auxiliar250_3, '') = '') 
                        then null 
                        else '8100' 
                end, 
                case 
                    when (coalesce(texto_auxiliar250_1, '') = '') 
                        then null 
                        else texto_auxiliar250_1 
                end,
                case 
                    when (coalesce(texto_auxiliar250_2, '') = '') 
                        then null 
                        else texto_auxiliar250_2 
                end,
                case 
                    when (coalesce(texto_auxiliar250_3, '') = '') 
                        then null 
                        else texto_auxiliar250_3 
                end;
end; $$
language 'plpgsql'