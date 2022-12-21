create or replace function public.usp_cabecerafe_fase2(
 numero_documento_emisor              character(20),
 serie_numero                         character(13),
 tipo_documento                       character(2),
 tipo_documento_emisor                character(1),
 monto_pago_cuota_1                   character(15),
 monto_pago_cuota_2                   character(15),
 monto_pago_cuota_3                   character(15),
 monto_pago_cuota_4                   character(15),
 monto_pago_cuota_5                   character(15),
 monto_pago_cuota_6                   character(15),
 monto_pago_cuota_7                   character(15),
 monto_pago_cuota_8                   character(15),
 monto_pago_cuota_9                   character(15),
 monto_pago_cuota_10                  character(15),
 monto_pago_cuota_11                  character(15),
 monto_pago_cuota_12                  character(15),
 fecha_pago_cuota_1                   character(15),
 fecha_pago_cuota_2                   character(15),
 fecha_pago_cuota_3                   character(15),
 fecha_pago_cuota_4                   character(15),
 fecha_pago_cuota_5                   character(15),
 fecha_pago_cuota_6                   character(15),
 fecha_pago_cuota_7                   character(15),
 fecha_pago_cuota_8                   character(15),
 fecha_pago_cuota_9                   character(15),
 fecha_pago_cuota_10                  character(15),
 fecha_pago_cuota_11                  character(15),
 fecha_pago_cuota_12                  character(15),
 texto_auxiliar250_1                  character(250),
 texto_auxiliar250_2                  character(250),
 texto_auxiliar250_3                  character(250),
 texto_auxiliar250_4                  character(250),
 texto_auxiliar250_5                  character(250),
 texto_auxiliar250_6                  character(250),
 texto_auxiliar250_7                  character(250),
 texto_auxiliar250_8                  character(250),
 texto_auxiliar250_9                  character(250),
 texto_auxiliar250_10                 character(250),
 texto_auxiliar250_11                 character(250),
 texto_auxiliar250_12                 character(250),
 texto_auxiliar250_13                 character(250),
 texto_auxiliar250_14                 character(250),
 texto_auxiliar250_15                 character(250),
 texto_auxiliar250_16                 character(250),
 texto_auxiliar250_17                 character(250),
 texto_auxiliar250_18                 character(250),
 texto_auxiliar250_19                 character(250),
 texto_auxiliar250_20                 character(250),
 texto_auxiliar500_1                  character(250),
 texto_auxiliar500_2                  character(250),
 texto_auxiliar500_3                  character(250),
 texto_auxiliar500_4                  character(250)
)
returns void
as $$
begin
    update public.spe_einvoiceheader
        set codigoauxiliar250_1  = case when coalesce(texto_auxiliar250_1, '')                    = '' then null else '9114' end,--9157 -> 9114
            codigoauxiliar250_2  = case when coalesce(texto_auxiliar250_2, '')                    = '' then null else '8205' end,--9660 -> 8205
            codigoauxiliar250_3  = case when coalesce(texto_auxiliar250_3, '')                    = '' then null else '7260' end,--9935 -> 7260 
            codigoauxiliar250_4  = case when coalesce(texto_auxiliar250_4, '')                    = '' then null else '9032' end,
            codigoauxiliar250_5  = case when coalesce(texto_auxiliar250_5, '')                    = '' then null else '8607' end,--9218 -> 8607
            codigoauxiliar250_6  = case when coalesce(texto_auxiliar250_6, '')                    = '' then null else '9980' end,--9412 -> 9980
            codigoauxiliar250_7  = case when coalesce(texto_auxiliar250_7, '')                    = '' then null else '7259'  end,--8044 -> 7259
            codigoauxiliar250_8  = case when coalesce(texto_auxiliar250_8, '')                    = '' then null else '9484' end,
            codigoauxiliar250_9  = case when coalesce(texto_auxiliar250_9, '')                    = '' then null else '8319' end,
            codigoauxiliar250_10 = case when coalesce(texto_auxiliar250_10, '')                   = '' then null else '9092' end,
            codigoauxiliar250_11 = case when coalesce(texto_auxiliar250_11, '')                   = '' then null else '9143' end,
            codigoauxiliar250_12 = case when coalesce(texto_auxiliar250_12, '')                   = '' then null else '8570' end,
            codigoauxiliar250_13 = case when coalesce(texto_auxiliar250_13, '')                   = '' then null else '9597' end,
            codigoauxiliar250_14 = case when coalesce(texto_auxiliar250_14, '')                   = '' then null else '9994' end,
            codigoauxiliar250_15 = case when coalesce(texto_auxiliar250_15, '')                   = '' then null else '9568' end,
            codigoauxiliar250_16 = case when coalesce(texto_auxiliar250_16, '')                   = '' then null else '9841'  end,
            codigoauxiliar250_17 = case when coalesce(texto_auxiliar250_17, '')                   = '' then null else '9569' end,
            codigoauxiliar250_18 = case when coalesce(texto_auxiliar250_18, '')                   = '' then null else '9839' end,
            codigoauxiliar250_19 = case when coalesce(texto_auxiliar250_19, '')                   = '' then null else '9420' end,
            codigoauxiliar250_20 = case when coalesce(texto_auxiliar250_20, '')                   = '' then null else '8274' end,
            codigoauxiliar500_1  = case when coalesce(texto_auxiliar500_1, '')                    = '' then null else '8275' end,
            codigoauxiliar500_2  = case when coalesce(texto_auxiliar500_2, '')                    = '' then null else '8292' end,
            codigoauxiliar500_3  = case when coalesce(texto_auxiliar500_3, '')                    = '' then null else '9115' end,
            codigoauxiliar500_4  = case when coalesce(texto_auxiliar500_4, '')                    = '' then null else '8046' end,
            textoauxiliar250_1   = case when coalesce(texto_auxiliar250_1, '')                    = '' then null else texto_auxiliar250_1 end,
            textoauxiliar250_2   = case when coalesce(texto_auxiliar250_2, '')                    = '' then null else texto_auxiliar250_2 end,
            textoauxiliar250_3   = case when coalesce(texto_auxiliar250_3, '')                    = '' then null else texto_auxiliar250_3 end,
            textoauxiliar250_4   = case when coalesce(texto_auxiliar250_4, '')                    = '' then null else texto_auxiliar250_4 end,
            textoauxiliar250_5   = case when coalesce(texto_auxiliar250_5, '')                    = '' then null else texto_auxiliar250_5 end,
            textoauxiliar250_6   = case when coalesce(texto_auxiliar250_6, '')                    = '' then null else texto_auxiliar250_6 end,
            textoauxiliar250_7   = case when coalesce(texto_auxiliar250_7, '')                    = '' then null else texto_auxiliar250_7 end,
            textoauxiliar250_8   = case when coalesce(texto_auxiliar250_8, '')                    = '' then null else texto_auxiliar250_8 end,
            textoauxiliar250_9   = case when coalesce(texto_auxiliar250_9, '')                    = '' then null else texto_auxiliar250_9 end,
            textoauxiliar250_10  = case when coalesce(texto_auxiliar250_10, '')                   = '' then null else texto_auxiliar250_10 end,
            textoauxiliar250_11  = case when coalesce(texto_auxiliar250_11, '')                   = '' then null else texto_auxiliar250_11 end,
            textoauxiliar250_12  = case when coalesce(texto_auxiliar250_12, '')                   = '' then null else texto_auxiliar250_12 end,
            textoauxiliar250_13  = case when coalesce(texto_auxiliar250_13, '')                   = '' then null else texto_auxiliar250_13 end,
            textoauxiliar250_14  = case when coalesce(texto_auxiliar250_14, '')                   = '' then null else texto_auxiliar250_14 end,
            textoauxiliar250_15  = case when coalesce(texto_auxiliar250_15, '')                   = '' then null else texto_auxiliar250_15 end,
            textoauxiliar250_16  = case when coalesce(texto_auxiliar250_16, '')                   = '' then null else texto_auxiliar250_16 end,
            textoauxiliar250_17  = case when coalesce(texto_auxiliar250_17, '')                   = '' then null else texto_auxiliar250_17 end,
            textoauxiliar250_18  = case when coalesce(texto_auxiliar250_18, '')                   = '' then null else texto_auxiliar250_18 end,
            textoauxiliar250_19  = case when coalesce(texto_auxiliar250_19, '')                   = '' then null else texto_auxiliar250_19 end,
            textoauxiliar250_20  = case when coalesce(texto_auxiliar250_20, '')                   = '' then null else texto_auxiliar250_20 end,
            textoauxiliar500_1   = case when coalesce(texto_auxiliar500_1, '')                    = '' then null else texto_auxiliar500_1 end,
            textoauxiliar500_2   = case when coalesce(texto_auxiliar500_2, '')                    = '' then null else texto_auxiliar500_2 end,
            textoauxiliar500_3   = case when coalesce(texto_auxiliar500_3, '')                    = '' then null else texto_auxiliar500_3 end,
            textoauxiliar500_4   = case when coalesce(texto_auxiliar500_4, '')                    = '' then null else texto_auxiliar500_4 end
        where numerodocumentoemisor   = numero_documento_emisor 
            and serienumero           = serie_numero 
            and tipodocumento         = tipo_documento 
            and tipodocumentoemisor   = tipo_documento_emisor;
    -- if (coalesce(texto_auxiliar250_3, '') <> '') then
    --         insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
    --                                                       numerodocumentoemisor, 
    --                                                       serienumero, 
    --                                                       tipodocumento, 
    --                                                       clave,
    --                                                       valor)
    --                                         select  tipo_documento_emisor, 
    --                                                 numero_documento_emisor, 
    --                                                 serie_numero, 
    --                                                 tipo_documento, 
    --                                                 'fechaVencimiento', 
    --                                                 texto_auxiliar250_3;
    -- end if;
    if (coalesce(monto_pago_cuota_1, '') <> '') then
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
                                                  'montoPagoCuota1', 
                                                  monto_pago_cuota_1;
    end if;

    if (coalesce(monto_pago_cuota_2, '') <> '') then
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
                                                 'montoPagoCuota2', 
                                                 monto_pago_cuota_2;
    end if;
 
    if (coalesce(monto_pago_cuota_3, '') <> '') then
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
                                                  'montoPagoCuota3', 
                                                  monto_pago_cuota_3;
    end if;
 
    if (coalesce(monto_pago_cuota_4, '') <> '') then
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
                                                  'montoPagoCuota4', 
                                                  monto_pago_cuota_4;
    end if;
 
    if (coalesce(monto_pago_cuota_5, '') <> '') then
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
                                                  'montoPagoCuota5', 
                                                  monto_pago_cuota_5;
    end if;

    if (coalesce(monto_pago_cuota_6, '') <> '') then
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
                                                  'montoPagoCuota6', 
                                                  monto_pago_cuota_6;
    end if;

    if (coalesce(monto_pago_cuota_7, '') <> '') then
        insert into public.spe_einvoiceheader_add ( tipodocumentoemisor, 
                                                      numerodocumentoemisor, 
                                                      serienumero, 
                                                      tipodocumento, 
                                                      clave,
                                                      valor)
                                            select tipo_documento_emisor, 
                                                   numero_documento_emisor, 
                                                   serie_numero, 
                                                   tipo_documento, 
                                                   'montoPagoCuota7', 
                                                   monto_pago_cuota_7;
    end if;

    if (coalesce(monto_pago_cuota_8, '') <> '') then
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
                                                  'montoPagoCuota8', 
                                                  monto_pago_cuota_8;
    end if;

    if (coalesce(monto_pago_cuota_9, '') <> '') then
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
                                                  'montoPagoCuota9', 
                                                  monto_pago_cuota_9;
    end if;

    if (coalesce(monto_pago_cuota_10, '') <> '') then
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
                                                  'montoPagoCuota10', 
                                                  monto_pago_cuota_10;
    end if;
 
    if (coalesce(monto_pago_cuota_11, '') <> '') then
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
                                                  'montoPagoCuota11', 
                                                  monto_pago_cuota_11;
    end if;

    if (coalesce(monto_pago_cuota_12, '') <> '') then
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
                                                  'montoPagoCuota12', 
                                                  monto_pago_cuota_12;
    end if;

    if (coalesce(fecha_pago_cuota_1, '') <> '') then
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
                                                    'fechaPagoCuota1', 
                                                    fecha_pago_cuota_1;
    end if;

    if (coalesce(fecha_pago_cuota_2, '') <> '') then
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
                                                    'fechaPagoCuota2', 
                                                    fecha_pago_cuota_2;
    end if;

    if (coalesce(fecha_pago_cuota_3, '') <> '') then
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
                                                    'fechaPagoCuota3', 
                                                    fecha_pago_cuota_3;
    end if;

    if (coalesce(fecha_pago_cuota_4, '') <> '') then
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
                                                    'fechaPagoCuota4', 
                                                    fecha_pago_cuota_4;
    end if;
 
    if (coalesce(fecha_pago_cuota_5, '') <> '') then
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
                                                    'fechaPagoCuota5', 
                                                    fecha_pago_cuota_5;
    end if;

    if (coalesce(fecha_pago_cuota_6, '') <> '') then
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
                                                    'fechaPagoCuota6', 
                                                    fecha_pago_cuota_6;
    end if;

    if (coalesce(fecha_pago_cuota_7, '') <> '') then
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
                                                    'fechaPagoCuota7', 
                                                    fecha_pago_cuota_7;
    end if;

    if (coalesce(fecha_pago_cuota_8, '') <> '') then
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
                                                    'fechaPagoCuota8', 
                                                    fecha_pago_cuota_8;
    end if;

    if (coalesce(fecha_pago_cuota_9, '') <> '') then
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
                                                    'fechaPagoCuota9', 
                                                    fecha_pago_cuota_9;
    end if;

    if (coalesce(fecha_pago_cuota_10, '') <> '') then
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
                                                    'fechaPagoCuota10', 
                                                    fecha_pago_cuota_10;
    end if;

    if (coalesce(fecha_pago_cuota_11, '') <> '') then
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
                                                    'fechaPagoCuota11', 
                                                    fecha_pago_cuota_11;
    end if;

    if (coalesce(fecha_pago_cuota_12, '') <> '') then
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
                                                    'fechaPagoCuota12', 
                                                    fecha_pago_cuota_12;
    end if;
end; $$
language 'plpgsql'
