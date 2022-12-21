using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.IDao.ExtraerDatos;

namespace peajeWebApp.Repositorio.Dao.ExtraerDatos
{
    public class ExtraerDetalle_Repositorio : IExtraerDetalles_Repositorio
    {
        private List<ListaDetalleGuardarDocumento> _detallesFE = new List<ListaDetalleGuardarDocumento>();
        string tipoDocumento = "";
        private async Task<string> AsignarDetalles(List<List<string>> listaDetalles, string NumeroDocumentoEmisor, string SerieNumero, string TipoDocumento, string TipoDocumentoEmisor)
        {
            try
            {
                foreach (List<string> detalle in listaDetalles)
                {
                    _detallesFE.Add(
                        new ListaDetalleGuardarDocumento()
                        {
                            NumeroOrdenItem = detalle[0],
                            Cantidad = detalle[1],
                            UnidadMedida = detalle[2],
                            ImporteTotalSinImpuesto = detalle[3],
                            ImporteTotalImpuestos = detalle[4],
                            ImporteUnitarioConImpuesto = detalle[5],
                            CodigoImporteUnitarioConImpuesto = detalle[6],
                            MontoBaseIgv = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 7 : 12],
                            ImporteIGV = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 8 : 13],
                            TasaIGV = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 10 : 15],
                            CodigoRazonExoneracion = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 11 : 16],
                            Descripcion = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 30 : 35],
                            CodigoProducto = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 32 : 37],
                            CodigoProductoSunat = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 33 : 38],
                            ImporteUnitarioSinImpuesto = detalle[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 35 : 40],
                            NumeroDocumentoEmisor = NumeroDocumentoEmisor,
                            SerieNumero = SerieNumero,
                            TipoDocumento = TipoDocumento,
                            TipoDocumentoEmisor = TipoDocumentoEmisor,
                            ImporteReferencial = "",
                            ImporteCargo = "",
                            TextoAuxiliar250_1 = "",
                            TextoAuxiliar250_2 = "",
                            TextoAuxiliar250_3 = "",
                            ImporteISC = "",
                            ImporteDescuento = "",
                            ImporteBaseDescuento = "",
                            FactorDescuento = "",
                            CodigoImporteReferencial = "",
                        }
                    );
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return $"error al asignar detalles -> {ex.Message}";
            }
        }

        #region Funcion de la interfaz 
        public async Task<List<ListaDetalleGuardarDocumento>> GetDetallesByDatosExtraidos(List<List<List<string>>> datosExtraidos, string NumeroDocumentoEmisor, string SerieNumero, string TipoDocumento, string TipoDocumentoEmisor)
        {
            this.tipoDocumento = datosExtraidos[0][0][0];

            string asignarDetalles = await this.AsignarDetalles(datosExtraidos[tipoDocumento.Equals("07") || tipoDocumento.Equals("08") ? 4 : 5], NumeroDocumentoEmisor, SerieNumero, TipoDocumento, TipoDocumentoEmisor);
            if (!asignarDetalles.Equals("ok"))
            {
                return null;
            }

            return _detallesFE;
        }
        #endregion
    }
}
