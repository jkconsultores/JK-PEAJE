using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao.ProcedimientosSQL
{
    public interface IStoresProcedures_SqlServer_Repositorio
    {
        public string PostUspCabeceraFE(DatosGuardarDoc cabeceraFE);
        public string PostUspDetalleFE(DatosGuardarDoc cabeceraFE);
        public string PostUspEnviaDocumentosFE(UspEnvioDocumento envioDocumentoFE);
        public string PostUspBajaDocumentoFE(DatosBajaDoc BajaDocumento);
        public string GetFirmaBajaDocumento(DatosBajaDoc BajaDocumento, string tipoDoc);
    }
}
