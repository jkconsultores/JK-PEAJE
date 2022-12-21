create or replace function public.usp_cabecerafe_fase1(    numero_documento_emisor              character(20),
                                                            serie_numero                         character(13),
                                                            tipo_documento                       character(2),
                                                            tipo_documento_emisor                character(1),
                                                            bl_estado_registro                   character(1),
                                                            bl_reintento                         integer,
                                                            bl_origen                            character(1),
                                                            bl_has_file_response                 integer,
                                                            correo_adquiriente                   character(100),
                                                            correo_emisor                        character(100),
                                                            departamento_emisor                  character(30),
                                                            direccion_emisor                     character(100),
                                                            distrito_emisor                      character(30),
                                                            fecha_emisor                         character(10),
                                                            nombre_comercial_emisor              character(100),
                                                            numero_documento_adquiriente         character(50),
                                                            pais_emisor                          character(20),
                                                            provincia_emisor                     character(30),
                                                            razon_social_adquiriente             character(100),
                                                            razon_social_emisor                  character(100),
                                                            serie_numero_afectado                character(13),
                                                            codigo_leyenda_1                     character(4),
                                                            texto_leyenda_1                      character(200),
                                                            tipo_documento_adquiriente           character(1),
                                                            tipo_moneda                          character(3),
                                                            total_igv                            character(15),
                                                            total_isc                            character(15),
                                                            total_otro_cargos                    character(15),
                                                            total_otros_tributos                character(15),
                                                            total_valor_venta_neto_op_exonerada  character(15),
                                                            total_valor_venta_neto_op_gratuitas  character(15),
                                                            total_valor_venta_neto_op_gravadas   character(15),
                                                            total_valor_venta_neto_op_no_gravada character(15),
                                                            total_valor_venta_neto_op_exporta    character(15),
                                                            total_venta                          character(15),
                                                            ubigeo_emisor                        character(6),
                                                            _urbanizacion                        character(25),
                                                            tipo_documento_afectado              character(2),
                                                            motivo_nc_nd                         character(500),
                                                            tipo_nc_nd                           character(3),
                                                            tipo_cambio                          character(20),
                                                            direccion_adquiriente                character(200),
                                                            total_impuestos                      character(15),
                                                            codigo_auxiliar40_1                  character(15),
                                                            texto_auxiliar40_1                   character(40),
                                                            tipo_operacion                       character(4),
                                                            hora_emision                         character(8),
                                                            codigo_local_anexo_emisor            character(15),
                                                            guia_remision                        character(15),
                                                            orden_compra                         character(1000),
                                                            tipo_guia_remision                   character(2),
                                                            forma_pago                           character(100),
                                                            ubigeo_adquiriente                   character(30),
                                                            urbanizacion_adquiriente             character(30),
                                                            provincia_adquiriente                character(30),
                                                            departamento_adquiriente             character(30),
                                                            distrito_adquiriente                 character(30),
                                                            pais_adquiriente                     character(30),
                                                            codigo_descuento                     character(30),
                                                            monto_base_descuento_global          character(15),
                                                            porcentaje_dscto_global              character(4),
                                                            descuentos_globales                  character(15),
                                                            total_descuentos                     character(15),
                                                            codigo_detraccion                    character(15),
                                                            porcentaje_detraccion                character(15),
                                                            total_detraccion                     character(15),
                                                            banco_nacion                         character(100),
                                                            codigo_forma_anticipo                character(10),
                                                            porcentaje_percepcion                character(15),
                                                            total_venta_con_percepcion           character(15),
                                                            base_imponible_percepcion            character(15),
                                                            regimen_percepcion                   character(15),
                                                            total_percepcion                     character(15),
                                                            total_retencion                      character(15),
                                                            porcentaje_retencion                 character(15),
                                                            total_documento_anticipo             character(15),
                                                            total_dscto_globales_anticipo          character(15),
                                                            porcentaje_dscto_global_anticipo     character(15),
                                                            codigo_serie_numero_afectado         character(15),
                                                            texto_leyenda_2                      character(100),
                                                            factura_pago_negociable              character(1),
                                                            monto_neto_pendiente                 character(15),
                                                            monto_base_retencion                 character(100),
                                                            fecha_vencimiento                    character(20),
                                                            total_valor_venta                    character(13),
                                                            total_precio_venta                   character(13)
                                                        )
returns void
as $$
declare 
    _fecha character(10);
    fecha_resumen character(8);
    resumen_count integer;
    r_a character(15);
begin
    delete from public.spe_einvoiceheader
        where numerodocumentoemisor   = numero_documento_emisor 
            and serienumero           = serie_numero 
            and tipodocumento         = tipo_documento;

    delete from public.spe_einvoicedetail
        where numerodocumentoemisor   = numero_documento_emisor 
            and serienumero           = serie_numero 
            and tipodocumento         = tipo_documento;

    delete from public.spe_einvoiceheader_add
        where numerodocumentoemisor   = numero_documento_emisor 
            and serienumero           = serie_numero 
            and tipodocumento         = tipo_documento;

    delete from public.spe_einvoiceheader_add
        where numero_documento_emisor   = numero_documento_emisor 
            and serie_numero           = serie_numero 
            and tipo_documento         = tipo_documento;

    insert into public.spe_einvoiceheader( numerodocumentoemisor, 
                                             serienumero, 
                                             tipodocumento, 
                                             tipodocumentoemisor, 
                                             bl_estadoregistro, 
                                             bl_reintento, 
                                             bl_origen, 
                                             bl_hasfileresponse, 
                                             correoadquiriente, 
                                             correoemisor, 
                                             departamentoemisor, 
                                             direccionemisor, 
                                             distritoemisor, 
                                             fechaemision, 
                                             nombrecomercialemisor, 
                                             numerodocumentoadquiriente, 
                                             paisemisor,
                                             provinciaemisor, 
                                             razonsocialadquiriente,
                                             razonsocialemisor, 
                                             numerodocumentoreferenciaprinc, 
                                             codigoleyenda_1, 
                                             textoleyenda_1, 
                                             tipodocumentoadquiriente, 
                                             tipomoneda,
                                             totaligv,
                                             totalisc, 
                                             totalotroscargos, 
                                             totalotrostributos, 
                                             totalvalorventanetoopexonerada, 
                                             totalvalorventanetoopgratuitas, 
                                             totalvalorventanetoopgravadas,
                                             totalvalorventanetoopnogravada, 
                                             totalvalorventanetoopexporta, 
                                             totalventa, 
                                             ubigeoemisor, 
                                             urbanizacion, 
                                             tipodocumentoreferenciaprincip, 
                                             motivodocumento,
                                             totalimpuestos, 
                                             codigoauxiliar40_1, 
                                             textoauxiliar40_1, 
                                             tipooperacion, 
                                             horaemision, 
                                             codigolocalanexoemisor,
                                             numerodocumentoreferencia_1,
                                             tiporeferencia_1, 
                                             montobasedescuentoglobal,  
                                             porcentajedsctoglobal,  
                                             totaldescuentos, 
                                             descuentosglobales, 
                                             codigodetraccion, 
                                             porcentajedetraccion,
                                             totaldetraccion, 
                                             numeroctabanconacion, 
                                             porcentajepercepcion, 
                                             totalventaconpercepcion, 
                                             baseimponiblepercepcion, 
                                             totalpercepcion, 
                                             totaldocumentoanticipo, 
                                             totaldsctoglobalesanticipo, 
                                             porcentajedsctoglobalanticipo, 
                                             codigoserienumeroafectado, 
                                             totalvalorventa, 
                                             totalprecioventa
                                )
            select  numero_documento_emisor, 
                    serie_numero, 
                    tipo_documento, 
                    tipo_documento_emisor, 
                    bl_estado_registro, 
                    bl_reintento, 
                    bl_origen, 
                    bl_has_file_response, 
                    case when coalesce(correo_adquiriente, '')                    in ('', ' ') then '-' else correo_adquiriente end, 
                    correo_emisor, 
                    departamento_emisor, 
                    direccion_emisor, 
                    distrito_emisor, 
                    fecha_emisor, 
                    case when coalesce(nombre_comercial_emisor, '')                = '' then null else nombre_comercial_emisor end, 
                    numero_documento_adquiriente, 
                    pais_emisor,
                    provincia_emisor, 
                    razon_social_adquiriente, 
                    razon_social_emisor, 
                    case when coalesce(serie_numero_afectado, '')                  = '' then null else serie_numero_afectado end , 
                    codigo_leyenda_1, 
                    texto_leyenda_1, 
                    tipo_documento_adquiriente, 
                    tipo_moneda,
                    case when coalesce(total_igv, '')                             in ('', ' ')  then '0.00' else total_igv end, 
                    case when coalesce(total_isc, '')                             in ('', ' ', '0.00', '0')  then null else total_isc end, 
                    case when coalesce(total_otro_cargos, '')                     in ('', ' ', '0', '0.00')  then null else total_otro_cargos end , 
                    case when coalesce(total_otros_tributos, '')                  in ('', ' ', '0', '0.00')  then null else total_otros_tributos end, 
                    case when coalesce(total_valor_venta_neto_op_exonerada, '')   in ('', ' ', '0', '0.00')  then null else total_valor_venta_neto_op_exonerada end ,
                    case when coalesce(total_valor_venta_neto_op_gratuitas, '')   in ('', ' ', '0', '0.00')  then null else total_valor_venta_neto_op_gratuitas end , 
                    case when coalesce(total_valor_venta_neto_op_gravadas, '')    in ('', ' ', '0', '0.00')  then null else total_valor_venta_neto_op_gravadas end ,
                    case when coalesce(total_valor_venta_neto_op_no_gravada, '')  in ('', ' ', '0', '0.00')  then null else total_valor_venta_neto_op_no_gravada end , 
                    case when coalesce(total_valor_venta_neto_op_exporta, '')     in ('', ' ', '0', '0.00')  then null else total_valor_venta_neto_op_exporta end , 
                    case when coalesce(total_venta, '')                            = '' then null else total_venta end , 
                    case when coalesce(ubigeo_emisor, '')                          = '' then '510101' else ubigeo_emisor end,
                    case when coalesce(_urbanizacion, '')                          = '' then '-' else _urbanizacion end, 
                    case when coalesce(tipo_documento_afectado, '')                = '' then null else tipo_documento_afectado end , 
                    case when coalesce(motivo_nc_nd, '')                           = '' then null else motivo_nc_nd end ,
                    case when coalesce(total_impuestos, '')                        in ('', ' ')  then '0.00' else total_impuestos end , 
                    codigo_auxiliar40_1, 
                    texto_auxiliar40_1, 
                    tipo_operacion, 
                    hora_emision, 
                    codigo_local_anexo_emisor,
                    case when coalesce(guia_remision, '')                          = '' then null else guia_remision end ,
                    case when coalesce(tipo_guia_remision, '')                     = '' then null else tipo_guia_remision end , 
                    case when coalesce(monto_base_descuento_global, '')           in ('', ' ', '0', '0.00')  then null else monto_base_descuento_global end , 
                    case when coalesce(porcentaje_dscto_global, '')               in ('', ' ', '0', '0.00')  then null else porcentaje_dscto_global end , 
                    case when coalesce(descuentos_globales, '')                   in ('', ' ', '0', '0.00')  then null else descuentos_globales end , 
                    case when coalesce(total_descuentos, '')                      in ('', ' ', '0', '0.00')  then null else total_descuentos end , 
                    case when coalesce(codigo_detraccion, '')                      = '' then null else codigo_detraccion end , 
                    case when coalesce(porcentaje_detraccion, '')                 in ('', ' ', '0', '0.00')  then null else porcentaje_detraccion end ,
                    case when coalesce(total_detraccion, '')                      in ('', ' ', '0', '0.00')  then null else total_detraccion end , 
                    case when coalesce(banco_nacion, '')                           = '' then null else banco_nacion end , 
                    case when coalesce(porcentaje_percepcion, '')                 in ('', ' ', '0', '0.00')  then null else porcentaje_percepcion end , 
                    case when coalesce(total_venta_con_percepcion, '')            in ('', ' ', '0', '0.00')  then null else total_venta_con_percepcion end , 
                    case when coalesce(base_imponible_percepcion, '')             in ('', ' ', '0', '0.00')  then null else base_imponible_percepcion end , 
                    case when coalesce(total_percepcion, '')                      in ('', ' ', '0', '0.00')  then null else total_percepcion end , 
                    case when coalesce(total_documento_anticipo, '')              in ('', ' ', '0', '0.00')  then null else total_documento_anticipo end ,
                    case when coalesce(total_dscto_globales_anticipo, '')         in ('', ' ', '0', '0.00')  then null else total_dscto_globales_anticipo end ,
                    case when coalesce(porcentaje_dscto_global_anticipo, '')      in ('', ' ', '0', '0.00')  then null else porcentaje_dscto_global_anticipo end ,
                    case when coalesce(serie_numero_afectado, '')                  = '' then null else codigo_serie_numero_afectado end,
                    total_valor_venta, 
                    total_precio_venta;

    
    if total_retencion not in ('', ' ', '0', '0.00') then
        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'importeOpeRetencion',
                                                    monto_base_retencion;

        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'porcentajeRetencion', 
                                                    porcentaje_retencion;

        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'importeRetencion', 
                                                    total_retencion;
    end if;

    if (coalesce(orden_compra, '') <> '') then
              insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                            numerodocumentoemisor, 
                                                            serienumero, 
                                                            tipodocumento, 
                                                            clave,
                                                            valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'ordenCompra', 
                                                    orden_compra;
    end if;

    if (coalesce(direccion_adquiriente, '') <> '') then
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'direccionAdquiriente', 
                                                    direccion_adquiriente;
    end if;

    if (coalesce(ubigeo_adquiriente, '') <> '') then
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'ubigeoAdquiriente', 
                                                    ubigeo_adquiriente;
    end if;

    if (coalesce(urbanizacion_adquiriente, '') <> '') then
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'urbanizacionAdquiriente', 
                                                    urbanizacion_adquiriente;
    end if;

    if (coalesce(provincia_adquiriente, '') <> '') then
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'provinciaAdquiriente',  
                                                    provincia_adquiriente;
    end if;
    
    if (coalesce(departamento_adquiriente, '') <> '') then
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'departamentoAdquiriente', 
                                                    departamento_adquiriente;
    end if;

    if (coalesce(distrito_adquiriente, '') <> '') then 
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'distritoAdquiriente', 
                                                    distrito_adquiriente;
    end if;

    if (coalesce(pais_adquiriente, '') <> '') then 
            insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'paisAdquiriente', 
                                                    pais_adquiriente;
    end if;

    if (coalesce(total_detraccion, '') not in ('', ' ', '0', '0.00')) then
			insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                          numerodocumentoemisor, 
                                                          serienumero, 
                                                          tipodocumento, 
                                                          clave,
                                                          valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'formaPago', 
                                                    '001';

            update public.spe_einvoiceheader 
                set codigoleyenda_2 = '2006', 
                    textoleyenda_2  = 'Operación sujeta a detracción'  
            where numerodocumentoemisor = numero_documento_emisor 
                and serienumero         = serie_numero 
                and tipodocumento       = tipo_documento;
    end if;

    if (coalesce(total_percepcion , '') in ('', ' ' , '0', '0.00', '0.000')) then
        update public.spe_einvoiceheader 
            set totalpercepcion         = null, 
                baseimponiblepercepcion = null, 
                porcentajepercepcion    = null, 
                totalventaconpercepcion = null, 
                regimenpercepcion       = null 
        where numerodocumentoemisor = numero_documento_emisor 
            and serienumero         = serie_numero 
            and tipodocumento       = tipo_documento;
    end if;

    if (coalesce(factura_pago_negociable, '') <> '') then
        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                          select  tipo_documento_emisor, 
                                                  numero_documento_emisor, 
                                                  serie_numero, 
                                                  tipo_documento, 
                                                  'formaPagoNegociable', 
                                                  factura_pago_negociable;
    end if;

    if (coalesce(monto_neto_pendiente, '') <> '') then
        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                          select  tipo_documento_emisor, 
                                                  numero_documento_emisor, 
                                                  serie_numero, 
                                                  tipo_documento, 
                                                  'montoNetoPendiente', 
                                                  monto_neto_pendiente;
    end if;

    if (coalesce(total_valor_venta_neto_op_gratuitas, '') not in ('', ' ', '0', '0.00')) then
        update public.spe_einvoiceheader 
            set codigoleyenda_2 = '1002', 
                textoleyenda_2  = 'TRANSFERENCIA GRATUITA DE UN BIEN O SERVICIO PRESTADO GRATUITAMENTE'
        where serienumero               = serie_numero 
            and tipodocumento           = tipo_documento 
            and numerodocumentoemisor   = numero_documento_emisor;
            
        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                            select  tipo_documento_emisor, 
                                                    numero_documento_emisor, 
                                                    serie_numero, 
                                                    tipo_documento, 
                                                    'totalTributosOpeGratuitas',
                                                    total_igv;
    end if;

-- Ya no se utilizará
    -- if (tipo_documento = '03' or tipo_documento_afectado = '03') then
    --     if not exists(
    --         select * 
    --             from public.spe_summary_item "I"
    --             inner join public.spe_summaryheader "H"
    --                     on "H".resumenid = "I"."resumenid" 
    --         where "I".numerocorrelativo = serie_numero 
    --             and "I".numerodocumentoemisor = numero_documento_emisor 
    --             and "I".tipodocumento = tipo_documento
    --             and "H".bl_estadoregistro = 'N'
    --     ) then

    --         _fecha := (select cast(now()::date as character(10)));
    --         fecha_resumen := replace(_fecha, '-', '');
    --         resumen_count := ((select coalesce(count(*), 0) from public.spe_summaryheader where fechageneracionresumen = _fecha) + 1);
    --         r_a := ('rc-' || fecha_resumen || '-' || right(('000' || cast(resumen_count as character(3))), 3));

    --         insert into public.spe_summaryheader 
    --         select numerodocumentoemisor, r_a, 6, '-', fechaemision, _fecha , '1', 
    --                 razonsocialemisor, 'RC', 'N', 0, 'W', null, null, null, null, null 
    --             from public.spe_einvoiceheader 
    --         where serienumero             = serie_numero 
    --             and numerodocumentoemisor = numero_documento_emisor 
    --             and tipodocumento         = tipo_documento;

    --         ---------- poner el totaligv o totalisc en 0 si es null
        
    --         insert into public.spe_summary_item
    --         select  numerodocumentoemisor, 
    --                 '6',
    --                 r_a, 
    --                 1,
    --                 tipodocumento,
    --                 serienumero,
    --                 tipodocumentoadquiriente,
    --                 numerodocumentoadquiriente,
    --                 numerodocumentoreferenciaprinc,
    --                 tipodocumentoreferenciaprincip, 
    --                 '1',
    --                 tipomoneda,
    --                 cast(round(cast(coalesce(totaligv, '0') as decimal),2) as character(15)),
    --                 cast(round(cast(coalesce(totalisc, '0') as decimal),2) as character(15)),

    --                 case 
    --                     when (cast(coalesce(totalotroscargos, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalotroscargos, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalotrostributos, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalotrostributos, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalvalorventanetoopexonerada, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalvalorventanetoopexonerada, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalvalorventanetoopgratuitas, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalvalorventanetoopgratuitas, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalvalorventanetoopgravadas, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalvalorventanetoopgravadas, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalvalorventanetoopnogravada, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalvalorventanetoopnogravada, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalventa, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalventa, '0') as decimal),2) as character(15))
    --                 end,
    --                 null, 
    --                 case
    --                     when ((coalesce(regimenpercepcion, '') = '') and (coalesce(porcentajepercepcion, '') <> '')) 
    --                         then '01' 
    --                     else regimenpercepcion 
    --                 end,
    --                 porcentajepercepcion,
    --                 case 
    --                     when (cast(coalesce(totalpercepcion, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalpercepcion, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalventaconpercepcion, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalventaconpercepcion, '0') as decimal),2) as character(15))
    --                 end, 
    --                 case 
    --                     when (cast(coalesce(totalpercepcion, '0') as decimal) = 0) 
    --                         then null 
    --                     else  cast(round(cast(coalesce(totalventa, '0') as decimal),2) as character(15))
    --                 end, 
    --                 null, 
    --                 null, 
    --                 null
    --             from public.spe_einvoiceheader 
    --                 where serienumero             = serie_numero 
    --                     and numerodocumentoemisor = numero_documento_emisor 
    --                     and tipodocumento         = tipo_documento;
    --     end if;
    -- end if;
end; $$
language 'plpgsql'
