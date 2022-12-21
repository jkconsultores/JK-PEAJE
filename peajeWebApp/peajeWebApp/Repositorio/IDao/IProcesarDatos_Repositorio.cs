namespace peajeWebApp.Repositorio.IDao
{
    public interface IProcesarDatos_Repositorio
    {
        public List<List<List<string>>> GetDatosByTramo(string tramoTxt);
        public Task<string> EjecutarGuardarDocumento(List<List<List<string>>> datosTramo, string comando);
        public Task<string> EjecutarBajarDocumento(List<List<List<string>>> datosTramo, string comando);
    }
}
