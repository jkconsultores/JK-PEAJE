using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao.ProcedimientosSQL
{
    public interface IStoresProcedures_PostgreSQL_Repositorio
    {
        public string PostUspCabeceraFE(DatosGuardarDoc cabeceraFE);
        public string PostUspDetalleFE(List<ListaDetalleGuardarDocumento> listaDetalleFE);
        public string PostUspEnviaDocumentosFE(UspEnvioDocumento envioDocumentoFE);
        public string PostUspInsertarDocumento(DatosGuardarDoc cabeceraFE);
        public string GetFirmaDocumento(DatosGuardarDoc cabeceraFE);
    }
}
