using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao.ExtraerDatos
{
    public interface IExtraerDetalles_Repositorio
    {
        public Task<List<ListaDetalleGuardarDocumento>> GetDetallesByDatosExtraidos(List<List<List<string>>> datosExtraidos, string NumeroDocumentoEmisor, string SerieNumero, string TipoDocumento, string TipoDocumentoEmisor);
    }
}
