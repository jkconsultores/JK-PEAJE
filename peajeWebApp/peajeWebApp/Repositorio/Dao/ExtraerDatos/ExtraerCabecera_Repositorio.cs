using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao.ExtraerDatos;

namespace peajeWebApp.Repositorio.Dao.ExtraerDatos
{
    public class ExtraerCabecera_Repositorio : IExtraerCabecera_Repositorio
    {
        //private UspCabecera _cabeceraFE = new UspCabecera();
        private DatosGuardarDoc _cabeceraFE = new DatosGuardarDoc();
        string tipoDocumento = "";
        public ExtraerCabecera_Repositorio()
        {
            this.InicializarCamposVacios_Cabecera();
        }

        #region Metodos de asignacion
        private void InicializarCamposVacios_Cabecera()
        {
            /*CABECERA*/
            _cabeceraFE.NumeroDocumentoEmisor = ""; _cabeceraFE.PorcentajePercepcion = "";
            _cabeceraFE.SerieNumero = ""; _cabeceraFE.TotalVentaConPercepcion = "";
            _cabeceraFE.TipoDocumento = ""; _cabeceraFE.BaseImponiblePercepcion = "";
            _cabeceraFE.TipoDocumentoEmisor = ""; _cabeceraFE.RegimenPercepcion = "";
            _cabeceraFE.BL_EstadoRegistro = "N"; _cabeceraFE.TotalPercepcion = "";
            _cabeceraFE.BL_Reintento = 0; _cabeceraFE.TotalRetencion = "";
            _cabeceraFE.BL_Origen = "W"; _cabeceraFE.PorcentajeRetencion = "";
            _cabeceraFE.BL_HASFileResponse = 0; _cabeceraFE.TotalDocumentoAnticipo = "";
            _cabeceraFE.CorreoAdquiriente = ""; _cabeceraFE.TotalDsctoGlobalesAnticipo = "";
            _cabeceraFE.CorreoEmisor = "-"; _cabeceraFE.PorcentajeDsctoGlobalAnticipo = "";
            _cabeceraFE.DepartamentoEmisor = ""; _cabeceraFE.CodigoSerieNumeroAfectado = "";
            _cabeceraFE.DireccionEmisor = ""; _cabeceraFE.Textoleyenda_2 = "";
            _cabeceraFE.DistritoEmisor = ""; _cabeceraFE.FacturaPagoNegociable = "";
            _cabeceraFE.FechaEmision = ""; _cabeceraFE.MontoNetoPendiente = "";
            _cabeceraFE.NombreComercialEmisor = ""; _cabeceraFE.MontoPagoCuota1 = "";
            _cabeceraFE.NumeroDocumentoAdquiriente = ""; _cabeceraFE.MontoPagoCuota2 = "";
            _cabeceraFE.PaisEmisor = ""; _cabeceraFE.MontoPagoCuota3 = "";
            _cabeceraFE.ProvinciaEmisor = ""; _cabeceraFE.MontoPagoCuota4 = "";
            _cabeceraFE.RazonSocialAdquiriente = ""; _cabeceraFE.MontoPagoCuota5 = "";
            _cabeceraFE.RazonSocialEmisor = ""; _cabeceraFE.MontoPagoCuota6 = "";
            _cabeceraFE.SerieNumeroAfectado = ""; _cabeceraFE.MontoPagoCuota7 = "";
            _cabeceraFE.CodigoLeyenda_1 = ""; _cabeceraFE.MontoPagoCuota8 = "";
            _cabeceraFE.TextoLeyenda_1 = ""; _cabeceraFE.MontoPagoCuota9 = "";
            _cabeceraFE.TipoDocumentoAdquiriente = ""; _cabeceraFE.MontoPagoCuota10 = "";
            _cabeceraFE.TipoMoneda = ""; _cabeceraFE.MontoPagoCuota11 = "";
            _cabeceraFE.TotalIGV = ""; _cabeceraFE.MontoPagoCuota12 = "";
            _cabeceraFE.TotalISC = ""; _cabeceraFE.FechaPagoCuota1 = "";
            _cabeceraFE.TotalOtrosCargos = ""; _cabeceraFE.FechaPagoCuota2 = "";
            _cabeceraFE.TotalOtrosTributos = ""; _cabeceraFE.FechaPagoCuota3 = "";
            _cabeceraFE.TotalValorVentaNetoOpExonerada = ""; _cabeceraFE.FechaPagoCuota4 = "";
            _cabeceraFE.TotalValorVentaNetoOpGratuitas = ""; _cabeceraFE.FechaPagoCuota5 = "";
            _cabeceraFE.TotalValorVentaNetoOpGravadas = ""; _cabeceraFE.FechaPagoCuota6 = "";
            _cabeceraFE.TotalValorVentaNetoOpNoGravada = ""; _cabeceraFE.FechaPagoCuota7 = "";
            _cabeceraFE.TotalvalorVentaNetoOpExporta = ""; _cabeceraFE.FechaPagoCuota8 = "";
            _cabeceraFE.TotalVenta = ""; _cabeceraFE.FechaPagoCuota9 = "";
            _cabeceraFE.UbigeoEmisor = ""; _cabeceraFE.FechaPagoCuota10 = "";
            _cabeceraFE.Urbanizacion = "-"; _cabeceraFE.FechaPagoCuota11 = "";
            _cabeceraFE.TipoDocumentoAfectado = ""; _cabeceraFE.FechaPagoCuota12 = "";
            _cabeceraFE.MotivoNCND = ""; _cabeceraFE.TextoAuxiliar250_1 = "";
            _cabeceraFE.TipoNCND = ""; _cabeceraFE.TextoAuxiliar250_2 = "";
            _cabeceraFE.Tipocambio = ""; _cabeceraFE.TextoAuxiliar250_3 = "";
            _cabeceraFE.DireccionAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_4 = "";
            _cabeceraFE.TotalImpuestos = ""; _cabeceraFE.TextoAuxiliar250_5 = "";
            _cabeceraFE.CodigoAuxiliar40_1 = "9011"; _cabeceraFE.TextoAuxiliar250_6 = "";
            _cabeceraFE.TextoAuxiliar40_1 = "18%"; _cabeceraFE.TextoAuxiliar250_7 = "";
            _cabeceraFE.TipoOperacion = "0101"; _cabeceraFE.TextoAuxiliar250_8 = "";
            _cabeceraFE.HoraEmision = ""; _cabeceraFE.TextoAuxiliar250_9 = "";
            _cabeceraFE.CodigoLocalAnexoEmisor = "0000"; _cabeceraFE.TextoAuxiliar250_10 = "";
            _cabeceraFE.GuiaRemision = ""; _cabeceraFE.TextoAuxiliar250_11 = "";
            _cabeceraFE.OrdenCompra = ""; _cabeceraFE.TextoAuxiliar250_12 = "";
            _cabeceraFE.TipoGuiaRemision = ""; _cabeceraFE.TextoAuxiliar250_13 = "";
            _cabeceraFE.Formapago = ""; _cabeceraFE.TextoAuxiliar250_14 = "";
            _cabeceraFE.UbigeoAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_15 = "";
            _cabeceraFE.UrbanizacionAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_16 = "";
            _cabeceraFE.ProvinciaAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_17 = "";
            _cabeceraFE.DepartamentoAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_18 = "";
            _cabeceraFE.DistritoAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_19 = "";
            _cabeceraFE.PaisAdquiriente = ""; _cabeceraFE.TextoAuxiliar250_20 = "";
            _cabeceraFE.CodigoDescuento = ""; _cabeceraFE.TextoAuxiliar500_1 = "";
            _cabeceraFE.MontoBaseDescuentoGlobal = ""; _cabeceraFE.TextoAuxiliar500_2 = "";
            _cabeceraFE.PorcentajeDsctoGlobal = ""; _cabeceraFE.TextoAuxiliar500_3 = "";
            _cabeceraFE.DescuentosGlobales = ""; _cabeceraFE.TextoAuxiliar500_4 = "";
            _cabeceraFE.TotalDescuentos = ""; _cabeceraFE.MontoBaseRetencion = "";
            _cabeceraFE.CodigoDetraccion = ""; _cabeceraFE.Fechavencimiento = "";
            _cabeceraFE.PorcentajeDetraccion = ""; _cabeceraFE.Totalvalorventa = "";
            _cabeceraFE.TotalDetraccion = ""; _cabeceraFE.Totalprecioventa = "";
            _cabeceraFE.BancoNacion = ""; _cabeceraFE.MontoBaseIGB = "";
            _cabeceraFE.CodigoFormaAnticipo = "";
        }

        private async Task<string> AsignarDatosIniciales(List<List<string>> datosIniciales)
        {
            try
            {
                #region DESCRIPCION DEL DOCUMENTO
                _cabeceraFE.TipoDocumento = datosIniciales[0][0];
                _cabeceraFE.SerieNumero = datosIniciales[0][1];
                _cabeceraFE.FechaEmision = datosIniciales[0][2];
                _cabeceraFE.TipoMoneda = datosIniciales[0][3];
                #region Validacion de fecha y tipo moneda
                //try
                //{
                //    DateTime FechaValida = DateTime.Parse(datosIniciales[0][2]);
                //    if (FechaValida.ToString("yyyy-MM-dd") == datosIniciales[0][2])
                //    {
                //        _cabeceraFE.FechaEmision = datosIniciales[0][2];
                //    }
                //    else
                //    {
                //        return "formato incorrecto en fecha emision";
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return "fecha emision incorrecta";
                //}
                //if (datosIniciales[0][3] == "PEN" || datosIniciales[0][3] == "USD")
                //{
                //    _cabeceraFE.TipoMoneda = datosIniciales[0][3];
                //}
                //else
                //{
                //    return "Tipo de Moneda incorrecta";
                //}
                #endregion
                _cabeceraFE.GuiaRemision = datosIniciales[0][4];
                _cabeceraFE.TipoGuiaRemision = datosIniciales[0][5];
                //Numer de cualquier otro documento(no hay campo)[6]
                //Codigo tipo de cualquier otro documento(no hay campo)[7]
                _cabeceraFE.OrdenCompra = datosIniciales[0][8];
                if (tipoDocumento.Equals("01") || tipoDocumento.Equals("03"))
                {
                    _cabeceraFE.Fechavencimiento = datosIniciales[0][8];
                }
                _cabeceraFE.HoraEmision = datosIniciales[0]
                    [
                        tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 9 : 10
                    ];
                #region Validar hora de emision
                //try
                //{
                //    DateTime FechaValida = DateTime.Parse(datosIniciales[0][tipoDocumento.Equals("07") ? 9 : 10]);
                //    if (FechaValida.ToString("HH:mm:ss") == datosIniciales[0][tipoDocumento.Equals("07") ? 9 : 10])
                //    {
                //        _cabeceraFE.HoraEmision = datosIniciales[0][tipoDocumento.Equals("07") ? 9 : 10];
                //    }
                //    else
                //    {
                //        return "formato incorrecto en Hora emision";
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return "Hora emision incorrecta";
                //}
                #endregion

                _cabeceraFE.TipoOperacion = datosIniciales[0]
                    [
                        tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 10 : 11
                    ];
                //Cantidad de items[12]
                //fecha de inicio del ciclo de facturacion[13]
                //fecha de fin del ciclo de facturacion[14]
                #endregion

                #region DATOS DEL EMISOR
                _cabeceraFE.NumeroDocumentoEmisor = datosIniciales[1][0];
                _cabeceraFE.TipoDocumentoEmisor = datosIniciales[1][1];
                _cabeceraFE.RazonSocialEmisor = datosIniciales[1][2];
                _cabeceraFE.NombreComercialEmisor = datosIniciales[1][3];
                _cabeceraFE.UbigeoEmisor = datosIniciales[1][4];
                _cabeceraFE.DireccionEmisor = datosIniciales[1][5];
                _cabeceraFE.Urbanizacion = datosIniciales[1][6];
                _cabeceraFE.ProvinciaEmisor = datosIniciales[1][7];
                _cabeceraFE.DepartamentoEmisor = datosIniciales[1][8];
                _cabeceraFE.DistritoEmisor = datosIniciales[1][9];
                _cabeceraFE.PaisEmisor = datosIniciales[1][10];
                //Pagina Web[11]
                //Telefono[12]
                _cabeceraFE.CorreoEmisor = datosIniciales[1][13];
                _cabeceraFE.CodigoLocalAnexoEmisor = datosIniciales[1][14];
                #endregion

                #region DATOS DEL RECEPTOR
                _cabeceraFE.NumeroDocumentoAdquiriente = datosIniciales[2][0];
                _cabeceraFE.TipoDocumentoAdquiriente = datosIniciales[2][1];
                _cabeceraFE.RazonSocialAdquiriente = datosIniciales[2][2];
                _cabeceraFE.UbigeoAdquiriente = datosIniciales[2][4];
                _cabeceraFE.DireccionAdquiriente = datosIniciales[2][5];
                _cabeceraFE.UrbanizacionAdquiriente = datosIniciales[2][6];
                _cabeceraFE.ProvinciaAdquiriente = datosIniciales[2][7];
                _cabeceraFE.DepartamentoAdquiriente = datosIniciales[2][8];
                _cabeceraFE.DistritoAdquiriente = datosIniciales[2][9];
                _cabeceraFE.PaisAdquiriente = datosIniciales[2][10];
                _cabeceraFE.CorreoAdquiriente = datosIniciales[2][11];
                #endregion
            }
            catch (Exception ex)
            {

                return "error al asignar valores en datos iniciales -> " + ex.Message;
            }

            return "ok";
        }

        private async Task<string> AsignarDatosDelComprobanteDeReferencia(List<List<string>> datosComprobanteReferencia)
        {
            try
            {
                _cabeceraFE.TipoNCND = datosComprobanteReferencia[0][0];
                _cabeceraFE.CodigoSerieNumeroAfectado = datosComprobanteReferencia[0][0];
                _cabeceraFE.MotivoNCND = datosComprobanteReferencia[0][1];
                _cabeceraFE.SerieNumeroAfectado = datosComprobanteReferencia[0][2];
                //Fecha emision del documento[3]
                _cabeceraFE.TipoDocumentoAfectado = datosComprobanteReferencia[0][4];
            }
            catch (Exception ex)
            {
                return $"error en asignar datos del comprobante de referencia -> {ex.Message}";
            }
            return "ok";
        }

        private async Task<string> AsignarImpuestosTotalesPorOperacion(List<List<string>> impuestosTotalesOperacion)
        {
            try
            {
                #region EXPORTACION/EXONERADAS/INAFECTAS
                switch (impuestosTotalesOperacion[0][3])
                {
                    case "9995":
                        _cabeceraFE.TotalvalorVentaNetoOpExporta = impuestosTotalesOperacion[0][0];
                        break;
                    case "9997":
                        _cabeceraFE.TotalValorVentaNetoOpExonerada = impuestosTotalesOperacion[0][0];
                        break;
                    case "9998":
                        _cabeceraFE.TotalValorVentaNetoOpNoGravada = impuestosTotalesOperacion[0][0];//INAFECTO
                        break;
                    default:
                        break;
                }
                //Importe del tributo[1]
                //Categoria de impuestos[2]
                //Codigo de tributo[3]
                //Nombre de tributo[4]
                //Codigo internacional tributo[5]
                #endregion

                #region GRATUITAS
                _cabeceraFE.TotalValorVentaNetoOpGratuitas = impuestosTotalesOperacion[1][0];
                //Sum de impuestos de operaciones gratuitas[1]
                //Categoria de impuestos[2]
                //Codigo de tributo[3]
                //Nombre de tributo[4]
                //Codigo internacional tributo[5]
                #endregion

                #region GRAVADAS
                _cabeceraFE.TotalValorVentaNetoOpGravadas = impuestosTotalesOperacion[2][0];
                _cabeceraFE.TotalIGV = impuestosTotalesOperacion[2][1];
                //Categoria de impuestos[2]
                //Codigo de tributo[3]
                //Nombre de tributo[4]
                //Codigo internacional tributo[5]
                #endregion

                #region ISC/SUMATORIA OTROS TRIBUTOS
                //Monto base[0]
                _cabeceraFE.TotalISC = impuestosTotalesOperacion[3][1];
                //Categoria de impuestos[2]
                //Codigo de tributo[3]
                //Nombre de tributo[4]
                //Codigo internacional tributo[5]
                #endregion
            }
            catch (Exception ex)
            {
                return "error al signar impuesto totales por operacion -> " + ex.Message;
            }
            return "ok";
        }

        private async Task<string> AsignarMontosTotales(List<List<string>> montosTotales)
        {
            try
            {
                _cabeceraFE.Totalvalorventa = montosTotales[0][0];
                _cabeceraFE.Totalprecioventa = montosTotales[0][1];
                if (tipoDocumento == "01" || tipoDocumento == "03")
                {
                    _cabeceraFE.TotalDescuentos = montosTotales[0][2];
                }
                _cabeceraFE.TotalOtrosCargos = montosTotales[0]
                    [
                        tipoDocumento.Equals("01") || tipoDocumento.Equals("03") ? 3 : 2
                    ];
                if (tipoDocumento.Equals("01") || tipoDocumento.Equals("03"))
                {
                    _cabeceraFE.TotalDocumentoAnticipo = montosTotales[0][4];
                }
                _cabeceraFE.TotalVenta = montosTotales[0]
                    [
                        tipoDocumento.Equals("01") || tipoDocumento.Equals("03") ? 5 : 3
                    ];
                //redondeo delimporte total[6o4]
                _cabeceraFE.TotalImpuestos = montosTotales[0]
                    [
                        tipoDocumento.Equals("01") || tipoDocumento.Equals("03") ? 7 : 5
                    ];
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Montos Totales -> " + ex.Message;
            }
            return "ok";
        }

        private async Task<string> AsignarDescuentosGlobales(List<List<string>> descuentosGlobales)
        {
            try
            {
                //Indicador cargo/descuentoGlobal[0]
                _cabeceraFE.CodigoDescuento = descuentosGlobales[0][1];
                //Factor del cargo/descuentoGlobal[2]
                _cabeceraFE.DescuentosGlobales = descuentosGlobales[0][3];
                _cabeceraFE.MontoBaseDescuentoGlobal = descuentosGlobales[0][4];
                return "ok";
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Descuuentos Globales -> " + ex.Message;
            }
        }

        private async Task<string> AsignarDatosDelAnticipo(List<List<string>> datosAnticipo)
        {
            //NO SE LOGRA ENCONTRAR CAMPO SEGUN EXCEL
            /*_cabeceraFE.CodigoFormaAnticipo;
            _cabeceraFE.TotalDocumentoAnticipo;
            _cabeceraFE.TotalDsctoGlobalesAnticipo;
            _cabeceraFE.PorcentajeDsctoGlobalAnticipo;*/
            return "ok";
        }

        private async Task<string> AsignarDetraccion(List<List<string>> datosDetraccion)
        {
            try
            {
                //Numero de Linea
                _cabeceraFE.Formapago = datosDetraccion[0][1];
                _cabeceraFE.BancoNacion = datosDetraccion[0][2];
                _cabeceraFE.CodigoDetraccion = datosDetraccion[0][3];
                _cabeceraFE.PorcentajeDetraccion = datosDetraccion[0][4];
                _cabeceraFE.TotalDetraccion = datosDetraccion[0][5];
                if (_cabeceraFE.TotalDetraccion == "0.00" || _cabeceraFE.TotalDetraccion == "")
                {
                    _cabeceraFE.Formapago = "";
                    _cabeceraFE.BancoNacion = "";
                    _cabeceraFE.CodigoDetraccion = "";
                    _cabeceraFE.PorcentajeDetraccion = "";
                }
                //Tipo de moneda
                return "ok";
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Detraccion -> " + ex.Message;
            }
        }

        private async Task<string> AsignarFacturaBoletaGuia(List<List<string>> facturaBoletaGuia, List<List<string>> facturaBoletaGuiaConductores, List<List<string>> facturaBoletaGuiaVehiculos)
        {
            //NO SE ENCONTRÓ CAMPOS PARA GUARDAR VALORES
            #region FACTURA GUIA
            /*
            try
            {
                Número de documento de identidad del destinatario
                Código de tipo  de documento de identidad del destinatario
                Apellidos y nombres, denominación o razón social del destinatario
                Código de motivo del traslado
                Peso bruto total
                Código de unidad de medida
                Modalidad de Transporte
                Fecha de inicio del traslado o fecha de entrega de bienes al transportista
                Numero de RUC del transportista
                Código de tipo de documento de identidad del transportista
                Apellidos y Nombres o denominación o razón social del transportista
                Numero de registro MTC
                Número de constancia de inscripción del vehículo o certificado de habilitación vehicular
                Numero de placa del vehículo del vehículo que realiza el traslado
                Dirección del punto de llegada(Código de ubigeo)
                Dirección del punto de llegada(Dirección completa y detallada)
                Indicador de subcontratación(en caso de Factura Guía Transportista)
                Dirección del punto de partida(Código de ubigeo)
                Dirección del punto de partida(Dirección completa y detallada)
                Código de ubigeo
                Dirección del lugar en el que se entrega el bien(Dirección completa y detallada)
                Urbanización
                Provincia
                Departamento
                Distrito
                Código de país
            }
            catch (Exception ex)
            {
                return $"ERROR -> {ex.Message}";
            }
            */
            #endregion

            #region FACTURA GUIA CONDUCTORES
            /*
            try
            {
                Número de Documento de los conductores
                Tipo de Documento de los conductores
            }
            catch (Exception ex)
            {
                return $"ERROR -> {ex.Message}";
            }
            */
            #endregion

            #region FACTURA GUIA VEHICULOS
            /*
            try
            { 
                Numero de placa del vehículo del vehículo que realiza el traslado 

            }
            catch (Exception ex)
            {
                return $"ERROR -> {ex.Message}";
            }
            */
            #endregion
            return "ok";
        }

        private async Task<string> AsignarLeyendas(List<List<string>> leyendas)
        {
            try
            {
                if (leyendas.Count <= 2)
                {
                    _cabeceraFE.TextoLeyenda_1 = "Son: " + leyendas[0][0];
                    _cabeceraFE.CodigoLeyenda_1 = leyendas[0][1];
                }
                if (leyendas.Count == 2)
                {
                    _cabeceraFE.Textoleyenda_2 = leyendas[1][0];
                    //CODIGO LEYENDA 2 [1][1]
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Leyendas -> " + ex.Message;
            }
        }

        private async Task<string> AsignarAdjuntos(List<List<string>> adjuntos)
        {
            try
            {
                _cabeceraFE.TextoAuxiliar250_1 = adjuntos[0][0];//PLACA
                _cabeceraFE.TextoAuxiliar250_2 = adjuntos[0][1];//Cajero
                _cabeceraFE.TextoAuxiliar250_3 = adjuntos[0][2];//Direccion Caseta
                _cabeceraFE.TextoAuxiliar250_4 = adjuntos[0][3];//Hora Emision
                _cabeceraFE.TextoAuxiliar250_5 = adjuntos[0][4];//Nro detraccion
                _cabeceraFE.TextoAuxiliar250_6 = adjuntos[0][5];//Monto detraccion
                _cabeceraFE.TextoAuxiliar250_7 = adjuntos[0][6];//Nombre de caseta
                if (adjuntos[0].Count > 7)
                {
                    _cabeceraFE.TextoAuxiliar250_8 = adjuntos[0][7];//DIRECCION DE VENTA
                }
                if (adjuntos[0].Count > 8)
                {
                    _cabeceraFE.TextoAuxiliar250_9 = adjuntos[0][8];//CODIGO AUTORIZACIÓN DE CONTINGENCIA
                }
                /*
                 Observación[1][0]
                 Correo electronico al cual se enviara el comprobante[1][1]
                 Se debe encontrar conectada en red y visible desde el servidor de facturacion.[1][2]
                 Numero de páginas a imprimir[1][3]
                 "emitir" (utilizar para implementaciones SPOOL)[1][4]
                 */
                return "ok";
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Adjunto -> " + ex.Message;
            }
        }

        private async Task<string> AsignarFormaDePago(List<List<string>> formasPago)
        {
            try
            {
                if (tipoDocumento.Equals("07") || tipoDocumento.Equals("08"))
                {
                    return "ok";
                }
                string tipoPago = formasPago[0][0];
                _cabeceraFE.Formapago = tipoPago;
                if (tipoPago.ToLower().Equals("contado"))
                {
                    _cabeceraFE.FacturaPagoNegociable = "0";
                    _cabeceraFE.MontoNetoPendiente = formasPago[0][1];
                }
                else if (tipoPago.ToLower().Equals("credito"))
                {
                    _cabeceraFE.FacturaPagoNegociable = "1";
                    _cabeceraFE.MontoNetoPendiente = formasPago[0][1];
                    if (formasPago.Count >= 2)
                    {
                        _cabeceraFE.MontoPagoCuota1 = formasPago[1][1];
                        _cabeceraFE.FechaPagoCuota1 = formasPago[1][2];
                    }
                    if (formasPago.Count >= 3)
                    {
                        _cabeceraFE.MontoPagoCuota2 = formasPago[2][1];
                        _cabeceraFE.FechaPagoCuota2 = formasPago[2][2];
                    }
                    if (formasPago.Count >= 4)
                    {
                        _cabeceraFE.MontoPagoCuota3 = formasPago[3][1];
                        _cabeceraFE.FechaPagoCuota3 = formasPago[3][2];
                    }
                    if (formasPago.Count >= 5)
                    {
                        _cabeceraFE.MontoPagoCuota4 = formasPago[4][1];
                        _cabeceraFE.FechaPagoCuota4 = formasPago[4][2];
                    }
                    if (formasPago.Count >= 6)
                    {
                        _cabeceraFE.MontoPagoCuota5 = formasPago[5][1];
                        _cabeceraFE.FechaPagoCuota5 = formasPago[5][2];
                    }
                    if (formasPago.Count >= 7)
                    {
                        _cabeceraFE.MontoPagoCuota6 = formasPago[6][1];
                        _cabeceraFE.FechaPagoCuota6 = formasPago[6][2];
                    }
                    if (formasPago.Count >= 8)
                    {
                        _cabeceraFE.MontoPagoCuota7 = formasPago[7][1];
                        _cabeceraFE.FechaPagoCuota7 = formasPago[7][2];
                    }
                    if (formasPago.Count >= 9)
                    {
                        _cabeceraFE.MontoPagoCuota8 = formasPago[8][1];
                        _cabeceraFE.FechaPagoCuota8 = formasPago[8][2];
                    }
                    if (formasPago.Count >= 10)
                    {
                        _cabeceraFE.MontoPagoCuota9 = formasPago[9][1];
                        _cabeceraFE.FechaPagoCuota9 = formasPago[9][2];
                    }
                    if (formasPago.Count >= 11)
                    {
                        _cabeceraFE.MontoPagoCuota10 = formasPago[10][1];
                        _cabeceraFE.FechaPagoCuota10 = formasPago[10][2];
                    }
                    if (formasPago.Count >= 12)
                    {
                        _cabeceraFE.MontoPagoCuota11 = formasPago[11][1];
                        _cabeceraFE.FechaPagoCuota11 = formasPago[11][2];
                    }
                    if (formasPago.Count >= 13)
                    {
                        _cabeceraFE.MontoPagoCuota12 = formasPago[12][1];
                        _cabeceraFE.FechaPagoCuota12 = formasPago[12][2];
                    }
                }

                return "ok";
            }
            catch (Exception ex)
            {
                return "error al asignar valores en Adjunto -> " + ex.Message;
            }
        }

        #endregion

        #region Funcion de la interfaz 
        public async Task<DatosGuardarDoc> GetCabeceraByDatosExtraidos(List<List<List<string>>> datosExtraidos)
        {
            //OBTENER TIPO DE DOCUMENTO
            this.tipoDocumento = datosExtraidos[0][0][0];

            #region ASIGNACIONES QUE SE USAN EN TODOS LOS TIPOS DE DOCUMENTO
            string asignarDatosInicials = await this.AsignarDatosIniciales(datosExtraidos[0]);
            if (!asignarDatosInicials.Equals("ok"))
            {
                return null;
            }
            string asignarImpuestosTotalesPorOperacion = await this.AsignarImpuestosTotalesPorOperacion(datosExtraidos[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 2 : 1]);
            if (!asignarImpuestosTotalesPorOperacion.Equals("ok"))
            {
                return null;
            }
            string asignarMontosTotales = await this.AsignarMontosTotales(datosExtraidos[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 3 : 2]);
            if (!asignarMontosTotales.Equals("ok"))
            {
                return null;
            }
            string asignarLeyendas = await this.AsignarLeyendas(
                                            datosExtraidos[tipoDocumento.Equals("07") || tipoDocumento.Equals("08")
                                                ? 5
                                                : (tipoDocumento.Equals("03")
                                                    ? 6
                                                    : 10)]);
            if (!asignarLeyendas.Equals("ok"))
            {
                return null;
            }
            string asignarAdjuntos = await this.AsignarAdjuntos(
                                            datosExtraidos[tipoDocumento.Equals("07") || tipoDocumento.Equals("08")
                                                ? 6
                                                : (tipoDocumento.Equals("03")
                                                    ? 7
                                                    : 11)]);
            if (!asignarAdjuntos.Equals("ok"))
            {
                return null;
            }
            #endregion

            #region ASIGNACION DE SOLO FACTURA Y BOLETA
            if (tipoDocumento.Equals("01") || tipoDocumento.Equals("03"))
            {
                string asignarDescuentosGlobales = await this.AsignarDescuentosGlobales(datosExtraidos[3]);
                if (!asignarDescuentosGlobales.Equals("ok"))
                {
                    return null;
                }

                string asignarDatosDelAnticipo = await this.AsignarDatosDelAnticipo(datosExtraidos[4]);
                if (!asignarDatosDelAnticipo.Equals("ok"))
                {
                    return null;
                }
                if (datosExtraidos[6][0].Count == 7)
                {
                    string asignarDetraccion = await this.AsignarDetraccion(datosExtraidos[6]);
                    if (!asignarDetraccion.Equals("ok"))
                    {
                        return null;
                    }
                }
            }
            #endregion

            #region ASIGNACION DE SOLO FACTURA
            if (tipoDocumento.Equals("01"))
            {
                string asignarFacturaBoletaGuia = await this.AsignarFacturaBoletaGuia(datosExtraidos[7], datosExtraidos[8], datosExtraidos[9]);
                if (!asignarFacturaBoletaGuia.Equals("ok"))
                {
                    return null;
                }
                string asignarFormaDePago = await this.AsignarFormaDePago(datosExtraidos[12]);
                if (!asignarFormaDePago.Equals("ok"))
                {
                    return null;
                }
            }
            #endregion

            #region ASIGNACION DE SOLO BOLETA
            //if (tipoDocumento.Equals("03"))
            //{

            //}
            #endregion

            #region ASIGNACION DE SOLO NC O ND
            if (tipoDocumento.Equals("07") || tipoDocumento.Equals("08"))
            {
                string asignarDatosDelComprobanteDeReferencia = await this.AsignarDatosDelComprobanteDeReferencia(datosExtraidos[1]);
                if (!asignarDatosDelComprobanteDeReferencia.Equals("ok"))
                {
                    return null;
                }
                string asignarFormaDePago = await this.AsignarFormaDePago(datosExtraidos[7]);
                if (!asignarFormaDePago.Equals("ok"))
                {
                    return null;
                }
            }
            #endregion

            return _cabeceraFE;
        }
        #endregion
    }
}
