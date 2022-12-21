namespace peajeWebApp.Models.DatosStoreProcedure
{
    public class DatosBajaDoc
    {
        public string NumeroDocumentoEmisor { get; set; }
        public List<ListaBajaDocumento> listaBajaDocumentos { get; set; }
        public string TipoDocBaja { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroSerieBajaDocumento { get; set; }
        public string Fecha { get; set; }
        public string FechaRa { get; set; }

    }
}
