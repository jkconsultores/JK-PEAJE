using Npgsql;

using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao.ProcedimientosSQL;

namespace peajeWebApp.Repositorio.Dao.ProcedimientosSQL
{
    public class StoresProcedures_PostgreSQL_Repositorio : IStoresProcedures_PostgreSQL_Repositorio
    {
        private readonly IConfiguration _config;

        #region Metodos privados CABECERA
        private string ejecutarPostgreSQL_fase1(DatosGuardarDoc cabeceraFE)
        {
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                cn.Open();
                string sentencia = @"select usp_cabecerafe_fase1(";
                #region Campos de la sentencia
                string campos = ":numero_documento_emisor,"
                                 + ":serie_numero,"
                                 + ":tipo_documento,"
                                 + ":tipo_documento_emisor,"
                                 + ":bl_estado_registro,"
                                 + ":bl_reintento,"
                                 + ":bl_origen,"
                                 + ":bl_has_file_response,"
                                 + ":correo_adquiriente,"
                                 + ":correo_emisor,"
                                 + ":departamento_emisor,"
                                 + ":direccion_emisor,"
                                 + ":distrito_emisor,"
                                 + ":fecha_emisor,"
                                 + ":nombre_comercial_emisor,"
                                 + ":numero_documento_adquiriente,"
                                 + ":pais_emisor,"
                                 + ":provincia_emisor,"
                                 + ":razon_social_adquiriente,"
                                 + ":razon_social_emisor,"
                                 + ":serie_numero_afectado,"
                                 + ":codigo_leyenda_1,"
                                 + ":texto_leyenda_1,"
                                 + ":tipo_documento_adquiriente,"
                                 + ":tipo_moneda,"
                                 + ":total_igv,"
                                 + ":total_isc,"
                                 + ":total_otro_cargos,"
                                 + ":total_otros_tributos,"
                                 + ":total_valor_venta_neto_op_exonerada,"
                                 + ":total_valor_venta_neto_op_gratuitas,"
                                 + ":total_valor_venta_neto_op_gravadas,"
                                 + ":total_valor_venta_neto_op_no_gravada,"
                                 + ":total_valor_venta_neto_op_exporta,"
                                 + ":total_venta,"
                                 + ":ubigeo_emisor,"
                                 + ":_urbanizacion,"
                                 + ":tipo_documento_afectado,"
                                 + ":motivo_nc_nd,"
                                 + ":tipo_nc_nd,"
                                 + ":tipo_cambio,"
                                 + ":direccion_adquiriente,"
                                 + ":total_impuestos,"
                                 + ":codigo_auxiliar40_1,"
                                 + ":texto_auxiliar40_1,"
                                 + ":tipo_operacion,"
                                 + ":hora_emision,"
                                 + ":codigo_local_anexo_emisor,"
                                 + ":guia_remision,"
                                 + ":orden_compra,"
                                 + ":tipo_guia_remision,"
                                 + ":forma_pago,"
                                 + ":ubigeo_adquiriente,"
                                 + ":urbanizacion_adquiriente,"
                                 + ":provincia_adquiriente,"
                                 + ":departamento_adquiriente,"
                                 + ":distrito_adquiriente,"
                                 + ":pais_adquiriente,"
                                 + ":codigo_descuento,"
                                 + ":monto_base_descuento_global,"
                                 + ":porcentaje_dscto_global,"
                                 + ":descuentos_globales,"
                                 + ":total_descuentos,"
                                 + ":codigo_detraccion,"
                                 + ":porcentaje_detraccion,"
                                 + ":total_detraccion,"
                                 + ":banco_nacion,"
                                 + ":codigo_forma_anticipo,"
                                 + ":porcentaje_percepcion,"
                                 + ":total_venta_con_percepcion,"
                                 + ":base_imponible_percepcion,"
                                 + ":regimen_percepcion,"
                                 + ":total_percepcion,"
                                 + ":total_retencion,"
                                 + ":porcentaje_retencion,"
                                 + ":total_documento_anticipo,"
                                 + ":total_dscto_globales_anticipo,"
                                 + ":porcentaje_dscto_global_anticipo,"
                                 + ":codigo_serie_numero_afectado,"
                                 + ":texto_leyenda_2,"
                                 + ":factura_pago_negociable,"
                                 + ":monto_neto_pendiente,"
                                 + ":monto_base_retencion,"
                                 + ":fecha_vencimiento,"
                                 + ":total_valor_venta,"
                                 + ":total_precio_venta)";
                #endregion

                using (NpgsqlCommand cmd = new NpgsqlCommand(sentencia + campos, cn))
                {
                    cmd.Connection = cn;
                    #region Campos de la funcion
                    cmd.Parameters.AddWithValue(":numero_documento_emisor", cabeceraFE.NumeroDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":serie_numero", cabeceraFE.SerieNumero);
                    cmd.Parameters.AddWithValue(":tipo_documento", cabeceraFE.TipoDocumento);
                    cmd.Parameters.AddWithValue(":tipo_documento_emisor", cabeceraFE.TipoDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":bl_estado_registro", cabeceraFE.BL_EstadoRegistro);
                    cmd.Parameters.AddWithValue(":bl_reintento", cabeceraFE.BL_Reintento);
                    cmd.Parameters.AddWithValue(":bl_origen", cabeceraFE.BL_Origen);
                    cmd.Parameters.AddWithValue(":bl_has_file_response", cabeceraFE.BL_HASFileResponse);
                    cmd.Parameters.AddWithValue(":correo_adquiriente", cabeceraFE.CorreoAdquiriente);
                    cmd.Parameters.AddWithValue(":correo_emisor", cabeceraFE.CorreoEmisor);
                    cmd.Parameters.AddWithValue(":departamento_emisor", cabeceraFE.DepartamentoEmisor);
                    cmd.Parameters.AddWithValue(":direccion_emisor", cabeceraFE.DireccionEmisor);
                    cmd.Parameters.AddWithValue(":distrito_emisor", cabeceraFE.DistritoEmisor);
                    cmd.Parameters.AddWithValue(":fecha_emisor", cabeceraFE.FechaEmision);
                    cmd.Parameters.AddWithValue(":nombre_comercial_emisor", cabeceraFE.NombreComercialEmisor);
                    cmd.Parameters.AddWithValue(":numero_documento_adquiriente", cabeceraFE.NumeroDocumentoAdquiriente);
                    cmd.Parameters.AddWithValue(":pais_emisor", cabeceraFE.PaisEmisor);
                    cmd.Parameters.AddWithValue(":provincia_emisor", cabeceraFE.ProvinciaEmisor);
                    cmd.Parameters.AddWithValue(":razon_social_adquiriente", cabeceraFE.RazonSocialAdquiriente);
                    cmd.Parameters.AddWithValue(":razon_social_emisor", cabeceraFE.RazonSocialEmisor);
                    cmd.Parameters.AddWithValue(":serie_numero_afectado", cabeceraFE.SerieNumeroAfectado);
                    cmd.Parameters.AddWithValue(":codigo_leyenda_1", cabeceraFE.CodigoLeyenda_1);
                    cmd.Parameters.AddWithValue(":texto_leyenda_1", cabeceraFE.TextoLeyenda_1);
                    cmd.Parameters.AddWithValue(":tipo_documento_adquiriente", cabeceraFE.TipoDocumentoAdquiriente);
                    cmd.Parameters.AddWithValue(":tipo_moneda", cabeceraFE.TipoMoneda);
                    cmd.Parameters.AddWithValue(":total_igv", cabeceraFE.TotalIGV);
                    cmd.Parameters.AddWithValue(":total_isc", cabeceraFE.TotalISC);
                    cmd.Parameters.AddWithValue(":total_otro_cargos", cabeceraFE.TotalOtrosCargos);
                    cmd.Parameters.AddWithValue(":total_otros_tributos", cabeceraFE.TotalOtrosTributos);
                    cmd.Parameters.AddWithValue(":total_valor_venta_neto_op_exonerada", cabeceraFE.TotalValorVentaNetoOpExonerada);
                    cmd.Parameters.AddWithValue(":total_valor_venta_neto_op_gratuitas", cabeceraFE.TotalValorVentaNetoOpGratuitas);
                    cmd.Parameters.AddWithValue(":total_valor_venta_neto_op_gravadas", cabeceraFE.TotalValorVentaNetoOpGravadas);
                    cmd.Parameters.AddWithValue(":total_valor_venta_neto_op_no_gravada", cabeceraFE.TotalValorVentaNetoOpNoGravada);
                    cmd.Parameters.AddWithValue(":total_valor_venta_neto_op_exporta", cabeceraFE.TotalvalorVentaNetoOpExporta);
                    cmd.Parameters.AddWithValue(":total_venta", cabeceraFE.TotalVenta);
                    cmd.Parameters.AddWithValue(":ubigeo_emisor", cabeceraFE.UbigeoEmisor);
                    cmd.Parameters.AddWithValue(":_urbanizacion", cabeceraFE.Urbanizacion);
                    cmd.Parameters.AddWithValue(":tipo_documento_afectado", cabeceraFE.TipoDocumentoAfectado);
                    cmd.Parameters.AddWithValue(":motivo_nc_nd", cabeceraFE.MotivoNCND);
                    cmd.Parameters.AddWithValue(":tipo_nc_nd", cabeceraFE.TipoNCND);
                    cmd.Parameters.AddWithValue(":tipo_cambio", cabeceraFE.Tipocambio);
                    cmd.Parameters.AddWithValue(":direccion_adquiriente", cabeceraFE.DireccionAdquiriente);
                    cmd.Parameters.AddWithValue(":total_impuestos", cabeceraFE.TotalImpuestos);
                    cmd.Parameters.AddWithValue(":codigo_auxiliar40_1", cabeceraFE.CodigoAuxiliar40_1);
                    cmd.Parameters.AddWithValue(":texto_auxiliar40_1", cabeceraFE.TextoAuxiliar40_1);
                    cmd.Parameters.AddWithValue(":tipo_operacion", cabeceraFE.TipoOperacion);
                    cmd.Parameters.AddWithValue(":hora_emision", cabeceraFE.HoraEmision);
                    cmd.Parameters.AddWithValue(":codigo_local_anexo_emisor", cabeceraFE.CodigoLocalAnexoEmisor);
                    cmd.Parameters.AddWithValue(":guia_remision", cabeceraFE.GuiaRemision);
                    cmd.Parameters.AddWithValue(":orden_compra", cabeceraFE.OrdenCompra);
                    cmd.Parameters.AddWithValue(":tipo_guia_remision", cabeceraFE.TipoGuiaRemision);
                    cmd.Parameters.AddWithValue(":forma_pago", cabeceraFE.Formapago);
                    cmd.Parameters.AddWithValue(":ubigeo_adquiriente", cabeceraFE.UbigeoAdquiriente);
                    cmd.Parameters.AddWithValue(":urbanizacion_adquiriente", cabeceraFE.UrbanizacionAdquiriente);
                    cmd.Parameters.AddWithValue(":provincia_adquiriente", cabeceraFE.ProvinciaAdquiriente);
                    cmd.Parameters.AddWithValue(":departamento_adquiriente", cabeceraFE.DepartamentoAdquiriente);
                    cmd.Parameters.AddWithValue(":distrito_adquiriente", cabeceraFE.DistritoAdquiriente);
                    cmd.Parameters.AddWithValue(":pais_adquiriente", cabeceraFE.PaisAdquiriente);
                    cmd.Parameters.AddWithValue(":codigo_descuento", cabeceraFE.CodigoDescuento);
                    cmd.Parameters.AddWithValue(":monto_base_descuento_global", cabeceraFE.MontoBaseDescuentoGlobal);
                    cmd.Parameters.AddWithValue(":porcentaje_dscto_global", cabeceraFE.PorcentajeDsctoGlobal);
                    cmd.Parameters.AddWithValue(":descuentos_globales", cabeceraFE.DescuentosGlobales);
                    cmd.Parameters.AddWithValue(":total_descuentos", cabeceraFE.TotalDescuentos);
                    cmd.Parameters.AddWithValue(":codigo_detraccion", cabeceraFE.CodigoDetraccion);
                    cmd.Parameters.AddWithValue(":porcentaje_detraccion", cabeceraFE.PorcentajeDetraccion);
                    cmd.Parameters.AddWithValue(":total_detraccion", cabeceraFE.TotalDetraccion);
                    cmd.Parameters.AddWithValue(":banco_nacion", cabeceraFE.BancoNacion);
                    cmd.Parameters.AddWithValue(":codigo_forma_anticipo", cabeceraFE.CodigoFormaAnticipo);
                    cmd.Parameters.AddWithValue(":porcentaje_percepcion", cabeceraFE.PorcentajePercepcion);
                    cmd.Parameters.AddWithValue(":total_venta_con_percepcion", cabeceraFE.TotalVentaConPercepcion);
                    cmd.Parameters.AddWithValue(":base_imponible_percepcion", cabeceraFE.BaseImponiblePercepcion);
                    cmd.Parameters.AddWithValue(":regimen_percepcion", cabeceraFE.RegimenPercepcion);
                    cmd.Parameters.AddWithValue(":total_percepcion", cabeceraFE.TotalPercepcion);
                    cmd.Parameters.AddWithValue(":total_retencion", cabeceraFE.TotalRetencion);
                    cmd.Parameters.AddWithValue(":porcentaje_retencion", cabeceraFE.PorcentajeRetencion);
                    cmd.Parameters.AddWithValue(":total_documento_anticipo", cabeceraFE.TotalDocumentoAnticipo);
                    cmd.Parameters.AddWithValue(":total_dscto_globales_anticipo", cabeceraFE.TotalDsctoGlobalesAnticipo);
                    cmd.Parameters.AddWithValue(":porcentaje_dscto_global_anticipo", cabeceraFE.PorcentajeDsctoGlobalAnticipo);
                    cmd.Parameters.AddWithValue(":codigo_serie_numero_afectado", cabeceraFE.CodigoSerieNumeroAfectado);
                    cmd.Parameters.AddWithValue(":texto_leyenda_2", cabeceraFE.Textoleyenda_2);
                    cmd.Parameters.AddWithValue(":factura_pago_negociable", cabeceraFE.FacturaPagoNegociable);
                    cmd.Parameters.AddWithValue(":monto_neto_pendiente", cabeceraFE.MontoNetoPendiente);
                    cmd.Parameters.AddWithValue(":monto_base_retencion", cabeceraFE.MontoBaseRetencion);
                    cmd.Parameters.AddWithValue(":fecha_vencimiento", cabeceraFE.Fechavencimiento);
                    cmd.Parameters.AddWithValue(":total_valor_venta", cabeceraFE.Totalvalorventa);
                    cmd.Parameters.AddWithValue(":total_precio_venta", cabeceraFE.Totalprecioventa);
                    #endregion

                    var dr = cmd.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    return "ok";
                    //}
                    return "ok";
                }
            }
            catch (NpgsqlException ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
        }

        private string ejecutarPostgreSQL_fase2(DatosGuardarDoc cabeceraFE)
        {
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                cn.Open();
                string sentenia = @"select usp_cabecerafe_fase2(";
                #region Campos de la sentencia
                string campos = ":numero_documento_emisor,"
                               + ":serie_numero,"
                               + ":tipo_documento,"
                               + ":tipo_documento_emisor,"
                               + ":monto_pago_cuota_1,"
                               + ":monto_pago_cuota_2,"
                               + ":monto_pago_cuota_3,"
                               + ":monto_pago_cuota_4,"
                               + ":monto_pago_cuota_5,"
                               + ":monto_pago_cuota_6,"
                               + ":monto_pago_cuota_7,"
                               + ":monto_pago_cuota_8,"
                               + ":monto_pago_cuota_9,"
                               + ":monto_pago_cuota_10,"
                               + ":monto_pago_cuota_11,"
                               + ":monto_pago_cuota_12,"
                               + ":fecha_pago_cuota_1,"
                               + ":fecha_pago_cuota_2,"
                               + ":fecha_pago_cuota_3,"
                               + ":fecha_pago_cuota_4,"
                               + ":fecha_pago_cuota_5,"
                               + ":fecha_pago_cuota_6,"
                               + ":fecha_pago_cuota_7,"
                               + ":fecha_pago_cuota_8,"
                               + ":fecha_pago_cuota_9,"
                               + ":fecha_pago_cuota_10,"
                               + ":fecha_pago_cuota_11,"
                               + ":fecha_pago_cuota_12,"
                               + ":texto_auxiliar250_1,"
                               + ":texto_auxiliar250_2,"
                               + ":texto_auxiliar250_3,"
                               + ":texto_auxiliar250_4,"
                               + ":texto_auxiliar250_5,"
                               + ":texto_auxiliar250_6,"
                               + ":texto_auxiliar250_7,"
                               + ":texto_auxiliar250_8,"
                               + ":texto_auxiliar250_9,"
                               + ":texto_auxiliar250_10,"
                               + ":texto_auxiliar250_11,"
                               + ":texto_auxiliar250_12,"
                               + ":texto_auxiliar250_13,"
                               + ":texto_auxiliar250_14,"
                               + ":texto_auxiliar250_15,"
                               + ":texto_auxiliar250_16,"
                               + ":texto_auxiliar250_17,"
                               + ":texto_auxiliar250_18,"
                               + ":texto_auxiliar250_19,"
                               + ":texto_auxiliar250_20,"
                               + ":texto_auxiliar500_1,"
                               + ":texto_auxiliar500_2,"
                               + ":texto_auxiliar500_3,"
                               + ":texto_auxiliar500_4)";
                #endregion
                using (NpgsqlCommand cmd = new NpgsqlCommand(sentenia + campos, cn))
                {
                    cmd.Connection = cn;
                    #region Campos de la funcion
                    cmd.Parameters.AddWithValue(":numero_documento_emisor", cabeceraFE.NumeroDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":serie_numero", cabeceraFE.SerieNumero);
                    cmd.Parameters.AddWithValue(":tipo_documento", cabeceraFE.TipoDocumento);
                    cmd.Parameters.AddWithValue(":tipo_documento_emisor", cabeceraFE.TipoDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_1", cabeceraFE.MontoPagoCuota1);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_2", cabeceraFE.MontoPagoCuota2);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_3", cabeceraFE.MontoPagoCuota3);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_4", cabeceraFE.MontoPagoCuota4);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_5", cabeceraFE.MontoPagoCuota5);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_6", cabeceraFE.MontoPagoCuota6);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_7", cabeceraFE.MontoPagoCuota7);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_8", cabeceraFE.MontoPagoCuota8);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_9", cabeceraFE.MontoPagoCuota9);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_10", cabeceraFE.MontoPagoCuota10);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_11", cabeceraFE.MontoPagoCuota11);
                    cmd.Parameters.AddWithValue(":monto_pago_cuota_12", cabeceraFE.MontoPagoCuota12);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_1", cabeceraFE.FechaPagoCuota1);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_2", cabeceraFE.FechaPagoCuota2);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_3", cabeceraFE.FechaPagoCuota3);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_4", cabeceraFE.FechaPagoCuota4);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_5", cabeceraFE.FechaPagoCuota5);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_6", cabeceraFE.FechaPagoCuota6);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_7", cabeceraFE.FechaPagoCuota7);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_8", cabeceraFE.FechaPagoCuota8);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_9", cabeceraFE.FechaPagoCuota9);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_10", cabeceraFE.FechaPagoCuota10);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_11", cabeceraFE.FechaPagoCuota11);
                    cmd.Parameters.AddWithValue(":fecha_pago_cuota_12", cabeceraFE.FechaPagoCuota12);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_1", cabeceraFE.TextoAuxiliar250_1);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_2", cabeceraFE.TextoAuxiliar250_2);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_3", cabeceraFE.TextoAuxiliar250_3);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_4", cabeceraFE.TextoAuxiliar250_4);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_5", cabeceraFE.TextoAuxiliar250_5);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_6", cabeceraFE.TextoAuxiliar250_6);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_7", cabeceraFE.TextoAuxiliar250_7);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_8", cabeceraFE.TextoAuxiliar250_8);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_9", cabeceraFE.TextoAuxiliar250_9);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_10", cabeceraFE.TextoAuxiliar250_10);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_11", cabeceraFE.TextoAuxiliar250_11);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_12", cabeceraFE.TextoAuxiliar250_12);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_13", cabeceraFE.TextoAuxiliar250_13);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_14", cabeceraFE.TextoAuxiliar250_14);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_15", cabeceraFE.TextoAuxiliar250_15);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_16", cabeceraFE.TextoAuxiliar250_16);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_17", cabeceraFE.TextoAuxiliar250_17);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_18", cabeceraFE.TextoAuxiliar250_18);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_19", cabeceraFE.TextoAuxiliar250_19);
                    cmd.Parameters.AddWithValue(":texto_auxiliar250_20", cabeceraFE.TextoAuxiliar250_20);
                    cmd.Parameters.AddWithValue(":texto_auxiliar500_1", cabeceraFE.TextoAuxiliar500_1);
                    cmd.Parameters.AddWithValue(":texto_auxiliar500_2", cabeceraFE.TextoAuxiliar500_2);
                    cmd.Parameters.AddWithValue(":texto_auxiliar500_3", cabeceraFE.TextoAuxiliar500_3);
                    cmd.Parameters.AddWithValue(":texto_auxiliar500_4", cabeceraFE.TextoAuxiliar500_4);
                    #endregion

                    var dr = cmd.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    return "ok";
                    //}
                    return "ok";
                }
            }
            catch (NpgsqlException ex)
            {
                return "Cabecera PostgreSQL fase2 -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Cabecera PostgreSQL fase2 -> " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
        }
        #endregion
        public StoresProcedures_PostgreSQL_Repositorio(IConfiguration config)
        {
            _config = config;
        }

        #region Metodos publicos de la interfaz
        public string PostUspCabeceraFE(DatosGuardarDoc cabeceraFE)
        {
            string resultadoPostgreSql_fase1 = this.ejecutarPostgreSQL_fase1(cabeceraFE);
            if (!resultadoPostgreSql_fase1.Equals("ok"))
            {
                return resultadoPostgreSql_fase1;
            }
            string resultadoPostgreSql_fase2 = this.ejecutarPostgreSQL_fase2(cabeceraFE);
            if (!resultadoPostgreSql_fase2.Equals("ok"))
            {
                return resultadoPostgreSql_fase2;
            }
            return "ok";
        }
        public string PostUspDetalleFE(List<ListaDetalleGuardarDocumento> listaDetalleFE)
        {
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                string sentencia = @"select usp_detallefe(";
                #region Campos de la sentencia
                string campos = ":numero_documento_emisor,"
                              + ":serie_numero,"
                              + ":tipo_documento,"
                              + ":tipo_documento_emisor,"
                              + ":numero_orden_item,"
                              + ":_cantidad,"
                              + ":codigo_producto,"
                              + ":codigo_razon_exoneracion,"
                              + ":_descripcion,"
                              + ":importe_descuento,"
                              + ":importe_total_sin_impuesto,"
                              + ":importe_unitario_con_impuesto,"
                              + ":importe_unitario_sin_impuesto,"
                              + ":codigo_importe_referencial,"
                              + ":importe_referencial,"
                              + ":unidad_medida,"
                              + ":codigo_importe_unitario_con_impuesto,"
                              + ":importe_igv,"
                              + ":importe_isc,"
                              + ":importe_cargo,"
                              + ":codigo_producto_sunat,"
                              + ":monto_base_igv,"
                              + ":tasa_ig,"
                              + ":importe_total_impuestos,"
                              + ":importe_base_descuento,"
                              + ":factor_descuento,"
                              + ":texto_auxiliar250_1,"
                              + ":texto_auxiliar250_2,"
                              + ":texto_auxiliar250_3)";
                #endregion

                foreach (ListaDetalleGuardarDocumento detalleFE in listaDetalleFE)
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sentencia + campos, cn))
                    {
                        cmd.Connection = cn;
                        #region Campos de la funcion
                        cmd.Parameters.AddWithValue(":numero_documento_emisor", detalleFE.NumeroDocumentoEmisor);
                        cmd.Parameters.AddWithValue(":serie_numero", detalleFE.SerieNumero);
                        cmd.Parameters.AddWithValue(":tipo_documento", detalleFE.TipoDocumento);
                        cmd.Parameters.AddWithValue(":tipo_documento_emisor", detalleFE.TipoDocumentoEmisor);
                        cmd.Parameters.AddWithValue(":numero_orden_item", detalleFE.NumeroOrdenItem);
                        cmd.Parameters.AddWithValue(":_cantidad", detalleFE.Cantidad);
                        cmd.Parameters.AddWithValue(":codigo_producto", detalleFE.CodigoProducto);
                        cmd.Parameters.AddWithValue(":codigo_razon_exoneracion", detalleFE.CodigoRazonExoneracion);
                        cmd.Parameters.AddWithValue(":_descripcion", detalleFE.Descripcion);
                        cmd.Parameters.AddWithValue(":importe_descuento", detalleFE.ImporteDescuento);
                        cmd.Parameters.AddWithValue(":importe_total_sin_impuesto", detalleFE.ImporteTotalSinImpuesto);
                        cmd.Parameters.AddWithValue(":importe_unitario_con_impuesto", detalleFE.ImporteUnitarioConImpuesto);
                        cmd.Parameters.AddWithValue(":importe_unitario_sin_impuesto", detalleFE.ImporteUnitarioSinImpuesto);
                        cmd.Parameters.AddWithValue(":codigo_importe_referencial", detalleFE.CodigoImporteReferencial);
                        cmd.Parameters.AddWithValue(":importe_referencial", detalleFE.ImporteReferencial);
                        cmd.Parameters.AddWithValue(":unidad_medida", detalleFE.UnidadMedida);
                        cmd.Parameters.AddWithValue(":codigo_importe_unitario_con_impuesto", detalleFE.CodigoImporteUnitarioConImpuesto);
                        cmd.Parameters.AddWithValue(":importe_igv", detalleFE.ImporteIGV);
                        cmd.Parameters.AddWithValue(":importe_isc", detalleFE.ImporteISC);
                        cmd.Parameters.AddWithValue(":importe_cargo", detalleFE.ImporteCargo);
                        cmd.Parameters.AddWithValue(":codigo_producto_sunat", detalleFE.CodigoProductoSunat);
                        cmd.Parameters.AddWithValue(":monto_base_igv", detalleFE.MontoBaseIgv);
                        cmd.Parameters.AddWithValue(":tasa_ig", detalleFE.TasaIGV);
                        cmd.Parameters.AddWithValue(":importe_total_impuestos", detalleFE.ImporteTotalImpuestos);
                        cmd.Parameters.AddWithValue(":importe_base_descuento", detalleFE.ImporteBaseDescuento);
                        cmd.Parameters.AddWithValue(":factor_descuento", detalleFE.FactorDescuento);
                        cmd.Parameters.AddWithValue(":texto_auxiliar250_1", detalleFE.TextoAuxiliar250_1);
                        cmd.Parameters.AddWithValue(":texto_auxiliar250_2", detalleFE.TextoAuxiliar250_2);
                        cmd.Parameters.AddWithValue(":texto_auxiliar250_3", detalleFE.TextoAuxiliar250_3);
                        #endregion
                        cmd.ExecuteReader();
                    }
                    cn.Close();
                }
                return "ok";
            }
            catch (NpgsqlException ex)
            {
                return "Detalle PostgreSQL -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Detalle PostgreSQL -> " + ex.Message;
            }
            finally
            {
                var isClosed = cn.State.ToString();
                if (!isClosed.Equals("Closed"))
                {
                    cn.Close();
                }
            }
        }
        public string PostUspEnviaDocumentosFE(UspEnvioDocumento envioDocumentoFE)
        {
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                cn.Open();
                string sentencia = @"select usp_enviodocumentofe(";
                #region Campos de la sentencia
                string campos = ":numero_documento_emisor,"
                              + ":serie_numero,"
                              + ":tipo_documento)";
                #endregion

                using (NpgsqlCommand cmd = new NpgsqlCommand(sentencia + campos, cn))
                {
                    cmd.Connection = cn;
                    #region Campos de la funcion
                    cmd.Parameters.AddWithValue(":numero_documento_emisor", envioDocumentoFE.NumeroDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":serie_numero", envioDocumentoFE.SerieNumero);
                    cmd.Parameters.AddWithValue(":tipo_documento", envioDocumentoFE.TipoDocumento);
                    #endregion

                    cmd.ExecuteReader();
                    return "ok";
                }
            }
            catch (NpgsqlException ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
        }
        public string PostUspInsertarDocumento(DatosGuardarDoc cabeceraFE)
        {
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                cn.Open();
                string sentencia = @"select usp_insertar_tdocumentofe(";
                #region Campos de la sentencia
                string campos = ":numero_documento_emisor,"
                              + ":serie_numero,"
                              + ":tipo_documento)";
                #endregion
                using (NpgsqlCommand cmd = new NpgsqlCommand(sentencia + campos, cn))
                {
                    cmd.Connection = cn;
                    #region Campos de la sentencia
                    cmd.Parameters.AddWithValue(":numero_documento_emisor", cabeceraFE.NumeroDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":serie_numero", cabeceraFE.SerieNumero);
                    cmd.Parameters.AddWithValue(":tipo_documento", cabeceraFE.TipoDocumento);
                    #endregion
                    cmd.ExecuteReader();
                    return "ok";
                }
            }
            catch (NpgsqlException ex)
            {
                return "insertar tdocumntoFE -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "insertar tdocumntoFE -> " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
        }
        public string GetFirmaDocumento(DatosGuardarDoc cabeceraFE)
        {
            return "OK|20522547957|03|B511|00794609|1.48|9.70|2022-05-05|0|-|OFS5f5oeHo0QFXQaHSRBNNn49W4=|JI1XcU0++CFQJVXYoP+Hola4TGH6rLknx2kcQOq6Dbgk1Tdj6jl6B4GQ8Jd3waFSr81rRZR+qIVQb3jLuFx3bsV/4yFtVnTwtKafBID9//NjJd8z/6XbPuj/TxLPkzZFD54TLAOHmPd9nB8I185zJoTaldVLavJo7jSbs1vG/eftAcEK8iTaiPkeMMKOjkVDVGVUkdqUhudTqVBnkhaH+Uw/8lTZa5WnXMYwTRMML5OqDhfAGXyXBa5kVZlvySVt1kxh0lduq/y5GaT4d3z4Qwq5ieZmW6GQvp9EI+e/BlrITLt2eRIBE9K2WSCedujoaRS7IgOCqEOBYvpbkOp5uw==|http://covisol2205.acepta.pe/v01/7AC82432C3649A5117638F953759F4CBB8887678?k=f28cc78c716c8a461e0f1c8caff46e08|[URL_PDF]|}";
            return "ERROR|[DATOS_DOC]|NO ESTA ACTIVO EL SERVICIO BIZLINKS.";
            string conexion = _config.GetSection("ConnectionStrings:DefaultConnectionPostgreSql").Value;
            NpgsqlConnection cn = new NpgsqlConnection(conexion);
            try
            {
                cn.Open();
                string sentencia = @"select * from usp_tramadevuelta(:numero_documento_emisor,:serie_numero,:tipo_documento)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sentencia, cn))
                {
                    cmd.Connection = cn;

                    cmd.Parameters.AddWithValue(":numero_documento_emisor", cabeceraFE.NumeroDocumentoEmisor);
                    cmd.Parameters.AddWithValue(":tipo_documento", cabeceraFE.TipoDocumento);
                    cmd.Parameters.AddWithValue(":serie_numero", cabeceraFE.SerieNumero);

                    //cmd.CommandType = CommandType.StoredProckedure;
                    var dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        return "ERROR|[DATOS_DOC]|No se pudo extraer trama desde la base de datos postgesql.}";
                    }
                    return (string)dr[0] + "}";
                }
            }
            catch (NpgsqlException ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            catch (Exception ex)
            {
                return "EnvioDocumento PostgreSQL -> " + ex.Message;
            }
            finally
            {
                cn.Close();
            }
        }
        #endregion
    }
}
