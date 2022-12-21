using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao.ExtraerDatos
{
    public interface IExtraerCabecera_Repositorio
    {
        public Task<DatosGuardarDoc> GetCabeceraByDatosExtraidos(List<List<List<string>>> datosExtraidos);
    }
}
