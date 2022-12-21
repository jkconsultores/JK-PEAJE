using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao;
using peajeWebApp.Repositorio.IDao.ExtraerDatos;
using peajeWebApp.Repositorio.IDao.ProcedimientosSQL;

using System.IO;

namespace peajeWebApp.Repositorio.Dao
{
    public class ProcesarDatos_Repositorio : IProcesarDatos_Repositorio
    {
        #region Variables Privadas
        private readonly IStoresProcedures_SqlServer_Repositorio _sqlServer;
        private readonly IStoresProcedures_PostgreSQL_Repositorio _postgreSql;
        private readonly IExtraerCabecera_Repositorio _extraerCabecera;
        private readonly IExtraerDetalles_Repositorio _extraerDetalles;
        private readonly IExtraerBajaDocumentos_Repositorio _extraerBajaDocumentos;
        private readonly IGenerarPdf _generarPdf;
        private readonly IConfiguration _config;
        private static int tiempo_espera;
        #endregion

        public ProcesarDatos_Repositorio(IStoresProcedures_PostgreSQL_Repositorio StoreProcedures_PostgreSQL,
                                         IStoresProcedures_SqlServer_Repositorio StoreProcedures_SqlServer,
                                         IExtraerCabecera_Repositorio ExtraerCabecera,
                                         IExtraerDetalles_Repositorio ExtraerDetalles,
                                         IExtraerBajaDocumentos_Repositorio ExtraerBajaDocumentos,
                                         IConfiguration config,
                                         IGenerarPdf generarPdf)
        {
            _postgreSql = StoreProcedures_PostgreSQL;
            _sqlServer = StoreProcedures_SqlServer;
            _extraerCabecera = ExtraerCabecera;
            _extraerDetalles = ExtraerDetalles;
            _extraerBajaDocumentos = ExtraerBajaDocumentos;
            _config = config;
            _generarPdf = generarPdf;
            tiempo_espera = Int32.Parse(_config.GetSection("Constantes:TiempoEspera").Value!);
        }

        public List<List<List<string>>> GetDatosByTramo(string tramoTxt)
        {
            /*
                 Extrae las secciones luego los grupos y al final los campos,
                 Los campos se guarda en un array para ser guardado en su grupo correspondiente,
                 Los grupos son guardados en un array para se guardado en su seccion correspondiente.
            */

            List<List<List<string>>> secciones_grupos = new();

            List<string> secciones = this.extraerSecciones(tramoTxt);

            foreach (string seccionTxt in secciones)
            {
                List<List<string>> grupos_campos = new();

                List<string> Grupos = new List<string>(seccionTxt.Split("}").SkipLast(1).ToList());

                foreach (string grupoTxt in Grupos)
                {
                    List<string> campos_dato = new();

                    List<string> campos = new List<string>(grupoTxt.Split("|"));
                    foreach (string campo in campos)
                    {
                        campos_dato.Add(campo);
                    }
                    grupos_campos.Add(campos_dato);
                }
                secciones_grupos.Add(grupos_campos);
            }
            return secciones_grupos;
        }

        #region METODOS PRIVADOS
        private List<string> extraerSecciones(string datosTxt)
        {
            int numero = datosTxt.IndexOf("\\");
            string[] arraySecciones = datosTxt.Split('~');
            List<string> secciones = new List<string>(arraySecciones);
            if (numero > 0)
            {
                secciones = secciones.SkipLast(1).ToList();
            }
            return secciones;
        }
        private string extraerDatosErrorByTramo(List<List<List<string>>> datosTramo, string tipoDoc)
        {
            if (tipoDoc.Equals("RC") || tipoDoc.Equals("RA"))
            {
                string[] nroSerie = datosTramo[0][0][0].Split("-");
                return $"{datosTramo[1][0][0]}" + //NUMERO DOCUMENTO EMISOR(RUC)
                        $"|{tipoDoc}" +
                        //$"|{nroSerie[0]}" +
                        //$"|{nroSerie[1]}" +
                        //$"|{nroSerie[2]}" +
                        $"|{datosTramo[0][0][0]}" + //NroSerie
                        $"|{datosTramo[0][0][1]}" + //fecha
                        $"|{datosTramo[0][0][2]}";  //fechara
            }
            else
            {
                string[] nroSerie = datosTramo[0][0][1].Split("-");
                return $"{datosTramo[0][1][0]}" +//NUMERO DOCUMENTO EMISOR (RUC)
                        $"|{tipoDoc}" +
                        //$"|{nroSerie[0]}" +
                        //$"|{nroSerie[1]}" +
                        $"|{datosTramo[0][0][1]}" + //NroSerie
                        $"|{datosTramo[0][0][2]}" +//FECHA
                        $"|{datosTramo[0][2][0]}";//NUMERO DOCUMENTO ADQUIRIENTE (RUC)
            }
        }
        private string extraerFirmas(string tramaDevuelta)
        {
            /*DEVUELVE PRIMERO EL HASHFIRMA Y LUEGO LA FIRMA*/
            string[] campos = tramaDevuelta.Split('|');
            return $"{campos[10]}|{campos[11]}";
        }
        private string ObtenerUrlGeneraPdf(string nroDocEmisor, string fecha, string tipoDoc, string nroSerieDoc)
        {
            string urlGuardarPdf = _config.GetSection("Constantes:RutaGenerarPdf").Value!;

            string urlFecha = fecha.Replace("-", "/");
            string nombrePdf = $"{(tipoDoc.Equals("07") || tipoDoc.Equals("08") ? $"{nroDocEmisor}-" : "")}{tipoDoc}-{nroSerieDoc}";
            string URL_PDF = $"{urlGuardarPdf}/{nroDocEmisor}/{urlFecha}/{nombrePdf}.pdf~{urlGuardarPdf}/{nroDocEmisor}/{urlFecha}";
            return URL_PDF;
        }
        #endregion

        #region METODOS DE GUARDAR DOCUMENTO
        #region PROCEDIMIENTOS ALMACENADOS GUARDAR DOC
        private string ejecutarGuardarDocumento_SqlServer(DatosGuardarDoc guardarDoc, UspEnvioDocumento envioDocumento)
        {
            string postCabecera = _sqlServer.PostUspCabeceraFE(guardarDoc);
            if (!postCabecera.Equals("ok"))
            {
                return $"ERROR|{postCabecera}";
            }
            string postDetalle = _sqlServer.PostUspDetalleFE(guardarDoc);
            if (!postDetalle.Equals("ok"))
            {
                return $"ERROR|{postDetalle}";
            }
            string postEnvioDocumento = _sqlServer.PostUspEnviaDocumentosFE(envioDocumento);
            if (!postEnvioDocumento.Equals("ok"))
            {
                return $"ERROR|{postEnvioDocumento}";
            }
            return "ok";
        }
        private string ejecutarGuardarDocumento_PostgreSql(DatosGuardarDoc guardarDoc, UspEnvioDocumento envioDocumento)
        {

            string postCabecera = _postgreSql.PostUspCabeceraFE(guardarDoc);
            if (!postCabecera.Equals("ok"))
            {
                return postCabecera;
            }
            string postDetalle = _postgreSql.PostUspDetalleFE(guardarDoc.listaDetalleGuardarDocumentos);
            if (!postDetalle.Equals("ok"))
            {
                return postDetalle;
            }
            string postEnvioDocumento = _postgreSql.PostUspEnviaDocumentosFE(envioDocumento);
            if (!postEnvioDocumento.Equals("ok"))
            {
                return postEnvioDocumento;
            }
            return "ok";
        }
        #endregion
        private async Task<string> ObtenerTramaGuardarDocumento(DatosGuardarDoc guardarDoc, string comando)
        {
            string[] serieNum = guardarDoc.SerieNumero.Split("-");//{serieNum[0]}|{serieNum[1]}
            string datosError = $"{guardarDoc.NumeroDocumentoEmisor}|{guardarDoc.TipoDocumento}|{guardarDoc.SerieNumero}|{guardarDoc.FechaEmision}|{(guardarDoc.NumeroDocumentoAdquiriente)}";
            try
            {
                /*SE EJECUTA INSERTAR DOCUMENTO Y SE ESPERA UN TIEMPO PARA QUE BIZLINK PUEDA HACER SU CHAMBA DE GENERAR LA FIRMA*/
                string postInsertarDocumento = _postgreSql.PostUspInsertarDocumento(guardarDoc);
                if (!postInsertarDocumento.Equals("ok"))
                {
                    return await Task.FromResult($"ERROR|{datosError}|obtener trama guardar documento -> {postInsertarDocumento}");
                }
                await Task.Delay(tiempo_espera);
                
                /*SE EJECUTA EL PROCEDURE DE OBTENER FIRMA Y SE GUARDA EN UNA LISTA SEPARADA POR '|'*/
                string tramaDevuelta = _postgreSql.GetFirmaDocumento(guardarDoc);
                List<string> ListTramaDevuelta = new List<string>(tramaDevuelta.Split("|"));
                
                /*VALIDAMOS ALGUNOS POSIBLES ERRORES*/
                if(ListTramaDevuelta.IndexOf("ERROR") != -1)
                {
                    //string tramaError = tramaDevuelta.Replace("[DATOS_DOC]", datosError);
                    return await Task.FromResult(tramaDevuelta.Replace("[DATOS_DOC]", datosError));
                }
                if (tramaDevuelta.IndexOf("EnvioDocumento PostgreSQL") > 0)
                {
                    return await Task.FromResult($"ERROR|{datosError}|obtener trama guardar documento -> {tramaDevuelta}");
                }

                #region PEDAZO DE CODIGO PARA GENERAR TRAMA SIN FIRMA
                //if (getFirmaDocumento.Equals(""))
                //{
                //    //SE CREA TRAMO DE MANERA MANUAL SIN FIRMA
                //    string[] nroSerie = cabecera.SerieNumero.Split("-");
                //    string tramoRespaldo = $"OK" +
                //                            $"|{cabecera.NumeroDocumentoEmisor}" +
                //                            $"|{cabecera.TipoDocumento}" +
                //                            $"|{nroSerie[0]}" +
                //                            $"|{nroSerie[1]}" +
                //                            $"|{cabecera.TotalImpuestos}" +
                //                            $"|{cabecera.Totalprecioventa}" +
                //                            $"|{cabecera.FechaEmision}" +
                //                            $"|{cabecera.TipoDocumentoAdquiriente}" +
                //                            $"|{cabecera.NumeroDocumentoAdquiriente}" +
                //                            $"|" +//hashfirma
                //                            $"|" +//firma
                //                            $"|" +//url_bizlink
                //                            $"|[URL_PDF]" +
                //                            $"|";
                //    getFirmaDocumento = tramoRespaldo;
                //}
                #endregion 

                /*EXTRAER FIRMAS, CREAR URL_PDF Y GENERAR PDF SEGUN COMANDO Y TIPO DE DOC*/
                if (!string.IsNullOrEmpty(comando))
                {
                    string firmas = this.extraerFirmas(tramaDevuelta);
                    string urlPdf = this.ObtenerUrlGeneraPdf(guardarDoc.NumeroDocumentoEmisor, guardarDoc.FechaEmision, guardarDoc.TipoDocumento, guardarDoc.SerieNumero);
                    string[] URLs = urlPdf.Split("~");
                    #region TEMPORAL
                    await Task.Run(() =>
                    {
                        using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                        {
                            string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                            file.WriteLine($"INICIO PDF -> {hora}.");
                        }
                    });
                    #endregion
                    string resultPdf = _generarPdf.GeneratePdf(guardarDoc, null, firmas, URLs[0], URLs[1], comando);
                    #region TEMPORAL
                    await Task.Run(() =>
                    {
                        using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                        {
                            string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                            file.WriteLine($"FIN PDF -> {hora}.");
                        }
                    });
                    #endregion
                    if(resultPdf != ("ok"))
                    {
                        urlPdf = "";
                    }
                    tramaDevuelta = tramaDevuelta.Replace("[URL_PDF]", URLs[0]);
                }
                else
                {
                    tramaDevuelta = tramaDevuelta.Replace("[URL_PDF]", "");
                }
                return await Task.FromResult(tramaDevuelta);
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"ERROR|{datosError}|obtener trama guardar documento -> {ex.Message}");
            }
        }
        public async Task<string> EjecutarGuardarDocumento(List<List<List<string>>> datosTramo, string comando)
        {
            try
            {
                #region Extraer datos
                DatosGuardarDoc guardarDoc = new DatosGuardarDoc();
                UspEnvioDocumento envioDocumento = new UspEnvioDocumento();
                try
                {
                    guardarDoc = await _extraerCabecera.GetCabeceraByDatosExtraidos(datosTramo);
                    if (guardarDoc == null)
                    {
                        return "ERROR|No se pudo extraer cabecera del tramo, en guardar documento.";
                    }
                    //listaDetalles = await _extraerDetalles.GetDetallesByDatosExtraidos(datosTramo, guardarDoc.NumeroDocumentoEmisor, guardarDoc.SerieNumero, guardarDoc.TipoDocumento, guardarDoc.TipoDocumentoEmisor);
                    guardarDoc.listaDetalleGuardarDocumentos = await _extraerDetalles.GetDetallesByDatosExtraidos(datosTramo, guardarDoc.NumeroDocumentoEmisor, guardarDoc.SerieNumero, guardarDoc.TipoDocumento, guardarDoc.TipoDocumentoEmisor);
                    if (guardarDoc.listaDetalleGuardarDocumentos == null)
                    {
                        return "ERROR|No se pudo extraer detalle del tramo, en guardar documento.";
                    }
                    envioDocumento = new()
                    {
                        NumeroDocumentoEmisor = guardarDoc.NumeroDocumentoEmisor,
                        SerieNumero = guardarDoc.SerieNumero,
                        TipoDocumento = guardarDoc.TipoDocumento
                    };
                }
                catch (Exception)
                {
                    return "ERROR|Ocurrio algun error al asignar datos, en guardar documento.";
                }
                #endregion
                string errorSqlServer = "";
                string resultado_sqlServer = this.ejecutarGuardarDocumento_SqlServer(guardarDoc, envioDocumento);
                if (resultado_sqlServer != "ok")
                {
                    using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\Errores.txt"))
                    {
                        string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        await file.WriteLineAsync($"{hora} --- Sql server guardar Documento-> {resultado_sqlServer}.");
                    }
                }
                string resultado_postgreSql = this.ejecutarGuardarDocumento_PostgreSql(guardarDoc, envioDocumento);
                if (resultado_postgreSql != "ok")
                {
                    //return $"ERROR:\n{errorSqlServer}\nPostgresql guardar Documento-> {resultado_postgreSql}.";
                    return $"ERROR|{this.extraerDatosErrorByTramo(datosTramo, datosTramo[0][0][0])}|Postgresql -> {resultado_postgreSql}|SqlServer -> {errorSqlServer}";
                }
                return await this.ObtenerTramaGuardarDocumento(guardarDoc, comando);
            }
            catch (Exception ex)
            {
                return $"ERROR|{this.extraerDatosErrorByTramo(datosTramo, datosTramo[0][0][0])}|Se produjo una excepcion en guardar Documento -> {ex.Message}";
            }
        }
        #endregion

        #region METODOS DE BAJA DOCUMENTO
        #region PROCEDIMIENTO ALMACENADO BAJA DOC
        private string ejecutarBajaDocumento_SqlServer(DatosBajaDoc BajaDocumento)
        {

            string postBajaDocumento = _sqlServer.PostUspBajaDocumentoFE(BajaDocumento);
            if (postBajaDocumento != "ok")
            {
                return postBajaDocumento;
            }
            return "ok";
        }
        #endregion
        private async Task<string> ObtenerTramaBajarDocumento(DatosBajaDoc BajaDocumento, string comando)
        {
            //string[] serieNum = cabecera.SerieNumero.Split("-");
            string[] serieNum = BajaDocumento.NumeroSerieBajaDocumento.Split("-");//{serieNum[0]}|{serieNum[1]}|{serieNum[2]}
            string datosError = $"{BajaDocumento.NumeroDocumentoEmisor}|{BajaDocumento.TipoDocBaja}|{BajaDocumento.NumeroSerieBajaDocumento}|{BajaDocumento.Fecha}|{BajaDocumento.FechaRa}";
            try
            {
                /*ESPERAMOS UN TIEMPO PARA QUE BIZLINK GENERE LA FIRMA*/
                await Task.Delay(tiempo_espera);
                string tipoDoc = BajaDocumento.listaBajaDocumentos[0].TipoDocumento;
                string tramaDevuelta = _sqlServer.GetFirmaBajaDocumento(BajaDocumento, tipoDoc);
                List<string> ListTramaDevuelta = new List<string>(tramaDevuelta.Split("|"));

                /*VALIDAMOS ALGUNOS POSIBLES ERRORES*/
                if (ListTramaDevuelta.IndexOf("ERROR") != -1)
                {
                    //string tramaError = tramaDevuelta.Replace("[DATOS_DOC]", datosError);
                    return await Task.FromResult(tramaDevuelta.Replace("[DATOS_DOC]", datosError));
                }
                if (tramaDevuelta.IndexOf("GetFirma SQLServer") > 0)
                {
                    return await Task.FromResult($"ERROR|{datosError}|obtener trama baja documento -> {tramaDevuelta}");
                }

                /*EXTRAER FIRMAS, CREAR URL_PDF Y GENERAR PDF SEGUN COMANDO Y TIPO DE DOC*/
                if (!string.IsNullOrEmpty(comando))
                {
                    string firmas = this.extraerFirmas(tramaDevuelta);
                    string urlPdf = this.ObtenerUrlGeneraPdf(BajaDocumento.NumeroDocumentoEmisor, BajaDocumento.FechaRa, BajaDocumento.TipoDocBaja, BajaDocumento.NumeroSerieBajaDocumento);
                    string[] URLs = urlPdf.Split("~");
                    #region TEMPORAL
                    await Task.Run(() =>
                    {
                        using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                        {
                            string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                            file.WriteLine($"INICIO PDF -> {hora}.");
                        }
                    });
                    #endregion
                    //_generarPdf.GeneratePdf(null, BajaDocumento, firmas, urlPdf);
                    string resultPdf = _generarPdf.GeneratePdf(null, BajaDocumento, firmas, URLs[0], URLs[1], comando);
                    #region TEMPORAL
                    await Task.Run(() =>
                    {
                        using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                        {
                            string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                            file.WriteLine($"FIN PDF -> {hora}.");
                        }
                    });
                    #endregion
                    if (resultPdf != ("ok"))
                    {
                        urlPdf = "";
                    }
                    tramaDevuelta = tramaDevuelta.Replace("[URL_PDF]", URLs[0]);
                }
                else
                {
                    tramaDevuelta = tramaDevuelta.Replace("[URL_PDF]", "");
                }
                return await Task.FromResult(tramaDevuelta);
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"ERROR|{datosError}|obtener trama baja documento -> {ex.Message}");
            }
        }
        public async Task<string> EjecutarBajarDocumento(List<List<List<string>>> datosTramo, string comando)
        {
            try
            {
                #region Extraer datos
                DatosBajaDoc bajaDocs = new DatosBajaDoc();
                try
                {
                    bajaDocs = _extraerBajaDocumentos.GetBajaDocumentosByDatosExtraidos(datosTramo);
                    if (bajaDocs == null)
                    {
                        return "ERROR|No se pudo extraer los datos del tramo en baja documento.";
                    }
                }
                catch (Exception)
                {
                    return "ERROR|Ocurrio algun error al asignar datos en baja documento.";
                }
                #endregion

                string resultado_sqlServer = this.ejecutarBajaDocumento_SqlServer(bajaDocs);
                if (resultado_sqlServer != "ok")
                {
                    return $"ERROR|{extraerDatosErrorByTramo(datosTramo, datosTramo[0][0][3])}|SqlServer baja documento -> {resultado_sqlServer}.";
                }

                return await this.ObtenerTramaBajarDocumento(bajaDocs, comando);
            }
            catch (Exception ex)
            {
                return $"ERROR|{extraerDatosErrorByTramo(datosTramo, datosTramo[0][0][3])}|Se produjo una excepcion en baja documento -> {ex.Message}";
            }
        }
        #endregion
    }
}
