using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao.ProcedimientosSQL;

using System.Data.SqlClient;

namespace peajeWebApp.Repositorio.Dao.ProcedimientosSQL
{
    public class StoresProcedures_SqlServer_Repositorio : IStoresProcedures_SqlServer_Repositorio
    {
        private readonly IConfiguration _config;
        public StoresProcedures_SqlServer_Repositorio(IConfiguration config)
        {
            _config = config;
        }

        #region Metodos publicos de la interfaz
        public string PostUspCabeceraFE(DatosGuardarDoc cabeceraFE)
        {
            var conexion = _config.GetSection("ConnectionStrings:DefaultConnectionSqlServer").Value;
            SqlConnection con = new SqlConnection(conexion);
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("[dbo].[USP_CabeceraFE]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    #region Campos de la funcion
                    cmd.Parameters.Add(new SqlParameter("@NUMERODOCUMENTOEMISOR", cabeceraFE.NumeroDocumentoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@SERIENUMERO", cabeceraFE.SerieNumero));
                    cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTO", cabeceraFE.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTOEMISOR", cabeceraFE.TipoDocumentoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@BL_ESTADOREGISTRO", cabeceraFE.BL_EstadoRegistro));
                    cmd.Parameters.Add(new SqlParameter("@BL_REINTENTO", cabeceraFE.BL_Reintento));
                    cmd.Parameters.Add(new SqlParameter("@BL_ORIGEN", cabeceraFE.BL_Origen));
                    cmd.Parameters.Add(new SqlParameter("@BL_HASFILERESPONSE", cabeceraFE.BL_HASFileResponse));
                    cmd.Parameters.Add(new SqlParameter("@CORREOADQUIRIENTE", cabeceraFE.CorreoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@CORREOEMISOR", cabeceraFE.CorreoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@DEPARTAMENTOEMISOR", cabeceraFE.DepartamentoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@DIRECCIONEMISOR", cabeceraFE.DireccionEmisor));
                    cmd.Parameters.Add(new SqlParameter("@DISTRITOEMISOR", cabeceraFE.DistritoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@FECHAEMISION", cabeceraFE.FechaEmision));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRECOMERCIALEMISOR", cabeceraFE.NombreComercialEmisor));
                    cmd.Parameters.Add(new SqlParameter("@NUMERODOCUMENTOADQUIRIENTE", cabeceraFE.NumeroDocumentoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@PAISEMISOR", cabeceraFE.PaisEmisor));
                    cmd.Parameters.Add(new SqlParameter("@PROVINCIAEMISOR", cabeceraFE.ProvinciaEmisor));
                    cmd.Parameters.Add(new SqlParameter("@RAZONSOCIALADQUIRIENTE", cabeceraFE.RazonSocialAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@RAZONSOCIALEMISOR", cabeceraFE.RazonSocialEmisor));
                    cmd.Parameters.Add(new SqlParameter("@serieNumeroAfectado", cabeceraFE.SerieNumeroAfectado));
                    cmd.Parameters.Add(new SqlParameter("@codigoLeyenda_1", cabeceraFE.CodigoLeyenda_1));
                    cmd.Parameters.Add(new SqlParameter("@textoLeyenda_1", cabeceraFE.TextoLeyenda_1));
                    cmd.Parameters.Add(new SqlParameter("@tipoDocumentoAdquiriente", cabeceraFE.TipoDocumentoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@tipoMoneda", cabeceraFE.TipoMoneda));
                    cmd.Parameters.Add(new SqlParameter("@totalIGV", cabeceraFE.TotalIGV));
                    cmd.Parameters.Add(new SqlParameter("@totalISC", cabeceraFE.TotalISC));
                    cmd.Parameters.Add(new SqlParameter("@totalOtrosCargos", cabeceraFE.TotalOtrosCargos));
                    cmd.Parameters.Add(new SqlParameter("@totalOtrosTributos", cabeceraFE.TotalOtrosTributos));
                    cmd.Parameters.Add(new SqlParameter("@totalValorVentaNetoOpExonerada", cabeceraFE.TotalValorVentaNetoOpExonerada));
                    cmd.Parameters.Add(new SqlParameter("@totalValorVentaNetoOpGratuitas", cabeceraFE.TotalValorVentaNetoOpGratuitas));
                    cmd.Parameters.Add(new SqlParameter("@totalValorVentaNetoOpGravadas", cabeceraFE.TotalValorVentaNetoOpGravadas));
                    cmd.Parameters.Add(new SqlParameter("@totalValorVentaNetoOpNoGravada", cabeceraFE.TotalValorVentaNetoOpNoGravada));
                    cmd.Parameters.Add(new SqlParameter("@totalvalorVentaNetoOpExporta", cabeceraFE.TotalvalorVentaNetoOpExporta));
                    cmd.Parameters.Add(new SqlParameter("@totalVenta", cabeceraFE.TotalVenta));
                    cmd.Parameters.Add(new SqlParameter("@ubigeoEmisor", cabeceraFE.UbigeoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@urbanizacion", cabeceraFE.Urbanizacion));
                    cmd.Parameters.Add(new SqlParameter("@tipoDocumentoAfectado", cabeceraFE.TipoDocumentoAfectado));
                    cmd.Parameters.Add(new SqlParameter("@MotivoNCND", cabeceraFE.MotivoNCND));
                    cmd.Parameters.Add(new SqlParameter("@TipoNCND", cabeceraFE.TipoNCND));
                    cmd.Parameters.Add(new SqlParameter("@tipocambio", cabeceraFE.Tipocambio));
                    cmd.Parameters.Add(new SqlParameter("@direccionAdquiriente", cabeceraFE.DireccionAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@totalImpuestos", cabeceraFE.TotalImpuestos));
                    cmd.Parameters.Add(new SqlParameter("@codigoAuxiliar40_1", cabeceraFE.CodigoAuxiliar40_1));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar40_1", cabeceraFE.TextoAuxiliar40_1));
                    cmd.Parameters.Add(new SqlParameter("@tipoOperacion", cabeceraFE.TipoOperacion));
                    cmd.Parameters.Add(new SqlParameter("@horaEmision", cabeceraFE.HoraEmision));
                    cmd.Parameters.Add(new SqlParameter("@codigoLocalAnexoEmisor", cabeceraFE.CodigoLocalAnexoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@GUIAREMISION", cabeceraFE.GuiaRemision));
                    cmd.Parameters.Add(new SqlParameter("@ORDENCOMPRA", cabeceraFE.OrdenCompra));
                    cmd.Parameters.Add(new SqlParameter("@TIPOGUIAREMISION", cabeceraFE.TipoGuiaRemision));
                    cmd.Parameters.Add(new SqlParameter("@formapago", cabeceraFE.Formapago));
                    cmd.Parameters.Add(new SqlParameter("@ubigeoAdquiriente", cabeceraFE.UbigeoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@urbanizacionAdquiriente", cabeceraFE.UrbanizacionAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@provinciaAdquiriente", cabeceraFE.ProvinciaAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@departamentoAdquiriente", cabeceraFE.DepartamentoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@distritoAdquiriente", cabeceraFE.DistritoAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@paisAdquiriente", cabeceraFE.PaisAdquiriente));
                    cmd.Parameters.Add(new SqlParameter("@codigoDescuento", cabeceraFE.CodigoDescuento));
                    cmd.Parameters.Add(new SqlParameter("@montoBaseDescuentoGlobal", cabeceraFE.MontoBaseDescuentoGlobal));
                    cmd.Parameters.Add(new SqlParameter("@porcentajeDsctoGlobal", cabeceraFE.PorcentajeDsctoGlobal));
                    cmd.Parameters.Add(new SqlParameter("@descuentosGlobales", cabeceraFE.DescuentosGlobales));
                    cmd.Parameters.Add(new SqlParameter("@TOTALDESCUENTOS", cabeceraFE.TotalDescuentos));
                    cmd.Parameters.Add(new SqlParameter("@CODIGODETRACCION", cabeceraFE.CodigoDetraccion));
                    cmd.Parameters.Add(new SqlParameter("@PORCENTAJEDETRACCION", cabeceraFE.PorcentajeDetraccion));
                    cmd.Parameters.Add(new SqlParameter("@TOTALDETRACCION", cabeceraFE.TotalDetraccion));
                    cmd.Parameters.Add(new SqlParameter("@BANCONACION", cabeceraFE.BancoNacion));
                    cmd.Parameters.Add(new SqlParameter("@CODIGOFORMAANTICIPO", cabeceraFE.CodigoFormaAnticipo));
                    cmd.Parameters.Add(new SqlParameter("@PORCENTAJEPERCEPCION", cabeceraFE.PorcentajePercepcion));
                    cmd.Parameters.Add(new SqlParameter("@TOTALVENTACONPERCEPCION", cabeceraFE.TotalVentaConPercepcion));
                    cmd.Parameters.Add(new SqlParameter("@BASEIMPONIBLEPERCEPCION", cabeceraFE.BaseImponiblePercepcion));
                    cmd.Parameters.Add(new SqlParameter("@REGIMENPERCEPCION", cabeceraFE.RegimenPercepcion));
                    cmd.Parameters.Add(new SqlParameter("@TOTALPERCEPCION", cabeceraFE.TotalPercepcion));
                    cmd.Parameters.Add(new SqlParameter("@TOTALRETENCION", cabeceraFE.TotalRetencion));
                    cmd.Parameters.Add(new SqlParameter("@PORCENTAJERETENCION", cabeceraFE.PorcentajeRetencion));
                    cmd.Parameters.Add(new SqlParameter("@totalDocumentoAnticipo", cabeceraFE.TotalDocumentoAnticipo));
                    cmd.Parameters.Add(new SqlParameter("@totaldsctoglobalesanticipo", cabeceraFE.TotalDsctoGlobalesAnticipo));
                    cmd.Parameters.Add(new SqlParameter("@porcentajeDsctoGlobalAnticipo", cabeceraFE.PorcentajeDsctoGlobalAnticipo));
                    cmd.Parameters.Add(new SqlParameter("@codigoSerieNumeroAfectado", cabeceraFE.CodigoSerieNumeroAfectado));
                    cmd.Parameters.Add(new SqlParameter("@textoleyenda_2", cabeceraFE.Textoleyenda_2));
                    cmd.Parameters.Add(new SqlParameter("@facturaPagoNegociable", cabeceraFE.FacturaPagoNegociable));
                    cmd.Parameters.Add(new SqlParameter("@montoNetoPendiente", cabeceraFE.MontoNetoPendiente));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota1", cabeceraFE.MontoPagoCuota1));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota2", cabeceraFE.MontoPagoCuota2));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota3", cabeceraFE.MontoPagoCuota3));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota4", cabeceraFE.MontoPagoCuota4));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota5", cabeceraFE.MontoPagoCuota5));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota6", cabeceraFE.MontoPagoCuota6));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota7", cabeceraFE.MontoPagoCuota7));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota8", cabeceraFE.MontoPagoCuota8));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota9", cabeceraFE.MontoPagoCuota9));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota10", cabeceraFE.MontoPagoCuota10));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota11", cabeceraFE.MontoPagoCuota11));
                    cmd.Parameters.Add(new SqlParameter("@montoPagoCuota12", cabeceraFE.MontoPagoCuota12));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota1", cabeceraFE.FechaPagoCuota1));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota2", cabeceraFE.FechaPagoCuota2));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota3", cabeceraFE.FechaPagoCuota3));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota4", cabeceraFE.FechaPagoCuota4));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota5", cabeceraFE.FechaPagoCuota5));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota6", cabeceraFE.FechaPagoCuota6));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota7", cabeceraFE.FechaPagoCuota7));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota8", cabeceraFE.FechaPagoCuota8));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota9", cabeceraFE.FechaPagoCuota9));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota10", cabeceraFE.FechaPagoCuota10));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota11", cabeceraFE.FechaPagoCuota11));
                    cmd.Parameters.Add(new SqlParameter("@fechaPagoCuota12", cabeceraFE.FechaPagoCuota12));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_1", cabeceraFE.TextoAuxiliar250_1));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_2", cabeceraFE.TextoAuxiliar250_2));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_3", cabeceraFE.TextoAuxiliar250_3));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_4", cabeceraFE.TextoAuxiliar250_4));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_5", cabeceraFE.TextoAuxiliar250_5));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_6", cabeceraFE.TextoAuxiliar250_6));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_7", cabeceraFE.TextoAuxiliar250_7));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_8", cabeceraFE.TextoAuxiliar250_8));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_9", cabeceraFE.TextoAuxiliar250_9));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_10", cabeceraFE.TextoAuxiliar250_10));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_11", cabeceraFE.TextoAuxiliar250_11));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_12", cabeceraFE.TextoAuxiliar250_12));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_13", cabeceraFE.TextoAuxiliar250_13));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_14", cabeceraFE.TextoAuxiliar250_14));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_15", cabeceraFE.TextoAuxiliar250_15));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_16", cabeceraFE.TextoAuxiliar250_16));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_17", cabeceraFE.TextoAuxiliar250_17));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_18", cabeceraFE.TextoAuxiliar250_18));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_19", cabeceraFE.TextoAuxiliar250_19));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_20", cabeceraFE.TextoAuxiliar250_20));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar500_1", cabeceraFE.TextoAuxiliar500_1));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar500_2", cabeceraFE.TextoAuxiliar500_2));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar500_3", cabeceraFE.TextoAuxiliar500_3));
                    cmd.Parameters.Add(new SqlParameter("@textoAuxiliar500_4", cabeceraFE.TextoAuxiliar500_4));
                    cmd.Parameters.Add(new SqlParameter("@montoBaseRetencion", cabeceraFE.MontoBaseRetencion));
                    cmd.Parameters.Add(new SqlParameter("@fechavencimiento", cabeceraFE.Fechavencimiento));
                    cmd.Parameters.Add(new SqlParameter("@totalvalorventa", cabeceraFE.Totalvalorventa));
                    cmd.Parameters.Add(new SqlParameter("@totalprecioventa", cabeceraFE.Totalprecioventa));
                    #endregion
                    cmd.ExecuteNonQuery();
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return $"cabecera sqlServer -> {ex.Message}";
            }
            finally
            {
                con.Close();
            }
        }
        public string PostUspDetalleFE(DatosGuardarDoc GuardarDocFe)
        {
            var conexion = _config.GetSection("ConnectionStrings:DefaultConnectionSqlServer").Value;
            SqlConnection con = new SqlConnection(conexion);
            try
            {
                foreach (ListaDetalleGuardarDocumento detalleFE in GuardarDocFe.listaDetalleGuardarDocumentos)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[USP_DetalleFE]", con))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        #region Campos de la funcion
                        cmd.Parameters.Add(new SqlParameter("@NUMERODOCUMENTOEMISOR", detalleFE.NumeroDocumentoEmisor));
                        cmd.Parameters.Add(new SqlParameter("@SERIENUMERO", detalleFE.SerieNumero));
                        cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTO", detalleFE.TipoDocumento));
                        cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTOEMISOR", detalleFE.TipoDocumentoEmisor));
                        cmd.Parameters.Add(new SqlParameter("@NUMEROORDENITEM", detalleFE.NumeroOrdenItem));
                        cmd.Parameters.Add(new SqlParameter("@CANTIDAD", detalleFE.Cantidad));
                        cmd.Parameters.Add(new SqlParameter("@CODIGOPRODUCTO", detalleFE.CodigoProducto));
                        cmd.Parameters.Add(new SqlParameter("@CODIGORAZONEXONERACION", detalleFE.CodigoRazonExoneracion));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", detalleFE.Descripcion));
                        cmd.Parameters.Add(new SqlParameter("@IMPORTEDESCUENTO", detalleFE.ImporteDescuento));
                        cmd.Parameters.Add(new SqlParameter("@importeTotalSinImpuesto", detalleFE.ImporteTotalSinImpuesto));
                        cmd.Parameters.Add(new SqlParameter("@importeUnitarioConImpuesto", detalleFE.ImporteUnitarioConImpuesto));
                        cmd.Parameters.Add(new SqlParameter("@importeUnitarioSinImpuesto", detalleFE.ImporteUnitarioSinImpuesto));
                        cmd.Parameters.Add(new SqlParameter("@CODIGOIMPORTEREFERENCIAL", detalleFE.CodigoImporteReferencial));
                        cmd.Parameters.Add(new SqlParameter("@IMPORTEREFERENCIAL", detalleFE.ImporteReferencial));
                        cmd.Parameters.Add(new SqlParameter("@UNIDADMEDIDA", detalleFE.UnidadMedida));
                        cmd.Parameters.Add(new SqlParameter("@codigoImporteUnitarioConImpuesto", detalleFE.CodigoImporteUnitarioConImpuesto));
                        cmd.Parameters.Add(new SqlParameter("@ImporteIGV", detalleFE.ImporteIGV));
                        cmd.Parameters.Add(new SqlParameter("@ImporteISC", detalleFE.ImporteISC));
                        cmd.Parameters.Add(new SqlParameter("@importeCargo", detalleFE.ImporteCargo));
                        cmd.Parameters.Add(new SqlParameter("@codigoProductoSUNAT", detalleFE.CodigoProductoSunat));
                        cmd.Parameters.Add(new SqlParameter("@montoBaseIgv", detalleFE.MontoBaseIgv));
                        cmd.Parameters.Add(new SqlParameter("@tasaIGV", detalleFE.TasaIGV));
                        cmd.Parameters.Add(new SqlParameter("@importeTotalImpuestos", detalleFE.ImporteTotalImpuestos));
                        cmd.Parameters.Add(new SqlParameter("@importeBaseDescuento", detalleFE.ImporteBaseDescuento));
                        cmd.Parameters.Add(new SqlParameter("@factorDescuento", detalleFE.FactorDescuento));
                        cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_1", detalleFE.TextoAuxiliar250_1));
                        cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_2", detalleFE.TextoAuxiliar250_2));
                        cmd.Parameters.Add(new SqlParameter("@textoAuxiliar250_3", detalleFE.TextoAuxiliar250_3));
                        #endregion
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "Detalle SQLServer --> " + ex.Message;
            }
            //finally
            //{
            //    con.Close();
            //}
        }
        public string PostUspEnviaDocumentosFE(UspEnvioDocumento envioDocumentoFE)
        {
            var conexion = _config.GetSection("ConnectionStrings:DefaultConnectionSqlServer").Value;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[USP_EnviaDocumentoFE]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@NUMERODOCUMENTOEMISOR", envioDocumentoFE.NumeroDocumentoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@SERIENUMERO", envioDocumentoFE.SerieNumero));
                    cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTO", envioDocumentoFE.TipoDocumento));
                    cmd.ExecuteNonQuery();
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "EnvioDocumento SQLServer --> " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        public string PostUspBajaDocumentoFE(DatosBajaDoc BajaDocumento)
        {
            var conexion = _config.GetSection("ConnectionStrings:DefaultConnectionSqlServer").Value;
            SqlConnection con = new SqlConnection(conexion);
            try
            {
                foreach (ListaBajaDocumento itemBajaDoc in BajaDocumento.listaBajaDocumentos)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[USP_BajaDocumentoFE]", con))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        #region Campos de la funcion
                        cmd.Parameters.Add(new SqlParameter("@NUMERODOCUMENTOEMISOR", BajaDocumento.NumeroDocumentoEmisor));
                        cmd.Parameters.Add(new SqlParameter("@SERIENUMERO", itemBajaDoc.SerieNumero));
                        cmd.Parameters.Add(new SqlParameter("@TIPODOCUMENTO", itemBajaDoc.TipoDocumento));
                        cmd.Parameters.Add(new SqlParameter("@FECHA", BajaDocumento.Fecha));
                        cmd.Parameters.Add(new SqlParameter("@FECHARA", BajaDocumento.FechaRa));
                        cmd.Parameters.Add(new SqlParameter("@motivo", itemBajaDoc.Motivo));
                        cmd.Parameters.Add(new SqlParameter("@resumenid", ""));
                        #endregion
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "BajaDocumento SQLServer --> " + ex.Message;

            }
            finally
            {
                var isClosed = con.State.ToString();
                if (!isClosed.Equals("Closed"))
                {
                    con.Close();
                }
            }
        }
        public string GetFirmaBajaDocumento(DatosBajaDoc BajaDocumento, string tipoDoc)
        {
            return "OK||RA|RA-20220502-00001|||||||fAnt+DpfUrxpQoKATUoXQIkSfI0=|dUEfxQ+zk8zfyG4aUf0v6p8njgGVXGlSgh7H9oIRDJik4xELYT9s7Z0RVZx0s51WLz5yGrbwe7pS9GmM2zxeO+KvBGglhv/nMf9kLSi99IfhJ+qRIfteo1cMPmeMdkwV7L6cEoPECl4QZGBSXHWmAMZlZlPKCZhGDZQAKTwK5nMtB6vYaMW9D5YmD9Q0D0SRF7HXdjUhL/W6QME/d8SaRb6rohTd/UaiWcVWC+DZOMDor1E8RH3XqDC7f8LPas7h27E/GWbudKWKcqwsmkKTXJrXGMDETb46tzWx8NKkbRRUogrJR6WNoz+yKVQ8aZVdlPifSJx/cqTOaiVXy4ZnMQ==|http://covisol2205.acepta.pe/v01/A633D76C80C96577286976D64728301CF6B98FFC?k=ab74b2cebb7c3c66a175aeb307c38c11|[URL_PDF]|}";
            return "ERROR|[DATOS_DOC]|NO ESTA ACTIVO EL SERVICIO BIZLINKS.";
            //OBTENER TRAMA CON FIRMA
            var conexion = _config.GetSection("ConnectionStrings:DefaultConnectionSqlServer").Value;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_tramaDevuelta]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@numero_documento_emisor", BajaDocumento.NumeroDocumentoEmisor));
                    cmd.Parameters.Add(new SqlParameter("@serie_numero", BajaDocumento.NumeroSerieBajaDocumento));
                    cmd.Parameters.Add(new SqlParameter("@tipo_doc", tipoDoc));

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        return "ERROR|[DATOS_DOC]|No se pudo extraer trama desde la base de datos sql server.}";
                    }
                    return (string)dr[0] + "}";
                    //if (dr.Read())
                    //{
                    //    tramaDevuelta = (string)dr[0] +
                    //                "|" + (string)dr[1] + "}";
                    //}
                    //return tramaDevuelta;
                }
            }
            catch (Exception ex)
            {
                return "GetFirma SQLServer --> " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}
