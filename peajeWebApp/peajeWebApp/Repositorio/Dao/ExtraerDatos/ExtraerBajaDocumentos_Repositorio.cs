using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao.ExtraerDatos;

namespace peajeWebApp.Repositorio.Dao.ExtraerDatos
{
    public class ExtraerBajaDocumentos_Repositorio : IExtraerBajaDocumentos_Repositorio
    {
        private DatosBajaDoc _DatosBajaDoc = new DatosBajaDoc();

        #region Extraer Datos de Documentos a Anular
        private string AsignarBajaDocumentos(List<List<string>> listDocumentosAnular)
        {
            /*SE EXTRAE LOS DOCUMENTOS A DAR DE BAJA DE LA TRAMA SEGUN LA GUIA Y SE GUARDA EN LA LISTA DE *DatosBajaDoc**/
            try
            {
                foreach (List<string> item in listDocumentosAnular)
                {
                    string numeroLinea = listDocumentosAnular.IndexOf(item).ToString();
                    _DatosBajaDoc.listaBajaDocumentos.Add(
                        new ListaBajaDocumento()
                        {
                            NumeroLinea = listDocumentosAnular.IndexOf(item).ToString(),
                            TipoDocumento = item[_DatosBajaDoc.TipoDocBaja.Equals("RA") ? 0 : 1],
                            SerieNumero = _DatosBajaDoc.TipoDocBaja.Equals("RA") ? $"{item[1]}-{item[2]}" : item[2],
                            Motivo = _DatosBajaDoc.TipoDocBaja.Equals("RA") ? item[3] : "",
                            MontoTotal = _DatosBajaDoc.TipoDocBaja.Equals("RA") ? "" : item[8],
                            MontoGrabado = _DatosBajaDoc.TipoDocBaja.Equals("RA") ? "" : item[9],
                            IGV = _DatosBajaDoc.TipoDocBaja.Equals("RA") ? "" : item[17],
                        }
                    );
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return $"error en asignar baja documento-> {ex.Message}";
            }
        }
        #endregion

        #region Funcion de la interfaz 
        public DatosBajaDoc GetBajaDocumentosByDatosExtraidos(List<List<List<string>>> datosExtraidos)
        {
            /*SE EXTRAE DATOS PRINCIPALES Y LUEGO SE GUARDA EN UNA LISTA TODOS LOS DOCUMENTOS A DAR DE BAJA*/
            #region Extraer Descripcion
            this._DatosBajaDoc.TipoDocBaja = datosExtraidos[0][0][3].ToUpper();
            this._DatosBajaDoc.NumeroSerieBajaDocumento = datosExtraidos[0][0][0];
            this._DatosBajaDoc.Fecha = datosExtraidos[0][0][1];
            this._DatosBajaDoc.FechaRa = datosExtraidos[0][0][2];
            #endregion

            #region Extraer Datos del Emisor
            this._DatosBajaDoc.NumeroDocumentoEmisor = datosExtraidos[1][0][0];
            #endregion
            /*AQUÍ SE OBTIENE TODOS LO DOCUMENTOS A DAR DE BAJA*/
            string asignarDetalles = this.AsignarBajaDocumentos(datosExtraidos[2]);
            if (!asignarDetalles.Equals("ok"))
            {
                return null;
            }
            return _DatosBajaDoc;
        }
        #endregion
    }
}
