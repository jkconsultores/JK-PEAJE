using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao.ExtraerDatos
{
    public interface IExtraerBajaDocumentos_Repositorio
    {
        public DatosBajaDoc GetBajaDocumentosByDatosExtraidos(List<List<List<string>>> datosExtraidos);
    }
}
