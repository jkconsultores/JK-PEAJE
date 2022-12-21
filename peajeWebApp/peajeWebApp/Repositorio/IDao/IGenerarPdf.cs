using peajeWebApp.Models.DatosStoreProcedure;

namespace peajeWebApp.Repositorio.IDao
{
    public interface IGenerarPdf
    {
        public string GeneratePdf(DatosGuardarDoc? guardarDoc, DatosBajaDoc? bajaDoc, string Firmas, string UrlGeneratePdf, string UrlCarpetaPdf, string comandoPdf);
    }
}
