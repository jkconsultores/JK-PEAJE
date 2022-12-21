using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;

using QRCoder;

using Microsoft.AspNetCore.Html;

using peajeWebApp.Repositorio.IDao;

using System.Text;
using System.Diagnostics;
using peajeWebApp.Models.DatosStoreProcedure;
using peajeWebApp.Repositorio.Dao.ConvertHtmlToString;

namespace peajeWebApp.Repositorio.Dao
{
    public class GenerarPdf : IGenerarPdf
    {
        private readonly IConfiguration _config;
        private string UrlPlantilla = "";

        public GenerarPdf(IConfiguration configuration)
        {
            _config = configuration;
            UrlPlantilla = _config.GetSection("Constantes:UrlPlantilla").Value;
        }
        
        #region Metodos Privada
        #region Metodos para obtener html segun el tipo
        private string htmlTktByGuardarDoc(DatosGuardarDoc bajaDoc)
        {
            return new ConvertHtmlToString.HtmlTktByGuardarDoc(_config).ConvertHtmlToString(bajaDoc);
        }
        private string htmlA4ByGuardarDoc(DatosGuardarDoc bajaDoc)
        {
            string stringHtml = "";
            return stringHtml;
        }
        private string htmlA4ByBajaDoc(DatosBajaDoc bajaDoc)
        {
            string stringHtml = "";
            return stringHtml;
        }
        #endregion
        #region Metodo QR
        private void SaveImagenQR(string ImgStr)
        {
            String path = UrlPlantilla + "\\QR\\";
            string imageName = "QR.png";
            if (System.IO.File.Exists(path + "QR.png"))
            {
                System.IO.File.Delete(path);
            }
            string imgPath = Path.Combine(path, imageName);
            byte[] imageBytes = Convert.FromBase64String(ImgStr);
            File.WriteAllBytes(imgPath, imageBytes);
        }
        public void DeleteImagenQR()
        {
            String path = UrlPlantilla + "\\QR\\QR.png";
            bool existeImagen = File.Exists(path);
            if (existeImagen)
            {
                System.IO.File.Delete(path);
            }
        }
        private void generarImgQR(string cadenaQR)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(cadenaQR, QRCodeGenerator.ECCLevel.L);
            BitmapByteQRCode bitmapByteQRCode = new BitmapByteQRCode(qrCodeData);
            var bitMap = bitmapByteQRCode.GetGraphic(10);
            using var ms = new MemoryStream();
            ms.Write(bitMap);
            byte[] byteImage = ms.ToArray();
            string img = Convert.ToBase64String(byteImage);
            this.SaveImagenQR(img);
        }
        #endregion
        #endregion

        public string GeneratePdf(DatosGuardarDoc? guardarDoc, DatosBajaDoc? bajaDoc, string Firmas, string UrlGeneratePdf, string UrlCarpetaPdf, string comandoPdf)
        {
            float widthPdf;
            float heightPdf;
            string htmlString = "";
            string cadenaQR = "";
            if (guardarDoc != null)
            {
                cadenaQR = $"{guardarDoc.NumeroDocumentoEmisor}" +
                            $"|{guardarDoc.TipoDocumento}" +
                            $"|{guardarDoc.SerieNumero.Replace("-", "|")}" +
                            $"|{guardarDoc.TotalIGV}" +
                            $"|{guardarDoc.TotalVenta}" +
                            $"|{guardarDoc.FechaEmision}" +
                            $"|{guardarDoc.TipoDocumentoAdquiriente}" +
                            $"|{guardarDoc.NumeroDocumentoAdquiriente}" +
                            $"|{Firmas}";
            }
            else if (bajaDoc != null)
            {
                cadenaQR = $"{bajaDoc.TipoDocBaja}";
            }

            //IR GENERANDO EL QR

            /*DEPENDE DE LO QUE VENGA EN TIPOPDF(emitir o emitir_tkt) SE VA A EXTRAER UN HTML DIFERENTE, SI NO PERTENECE A NINGUNO NO GENERARA NINGUN PDF*/
            if (comandoPdf.Equals("emitir_tkt") && (guardarDoc is not null))
            {
                widthPdf = 260;
                heightPdf = 540;
                heightPdf += (20 * (guardarDoc.listaDetalleGuardarDocumentos.Count - 1));
                heightPdf += (guardarDoc.TipoDocumento.Equals("01") ? 55 : 0); //FACTURA
                heightPdf += (guardarDoc.CodigoDetraccion.Length > 0 ? 26 : 0); //DETRACCION

                htmlString = this.htmlTktByGuardarDoc(guardarDoc);
                
            }
            else if (comandoPdf.Equals("emitir") && ((guardarDoc is not null) || (bajaDoc is not null)))
            {
                widthPdf = PageSize.A4.Width;
                heightPdf = PageSize.A4.Height;
                if (guardarDoc != null)
                {
                    htmlString = this.htmlA4ByGuardarDoc(guardarDoc);
                }
                else if (bajaDoc != null)
                {
                    htmlString = this.htmlA4ByBajaDoc(bajaDoc);
                }
            }
            else
            {
                return "";
            }

            /*SE GENERA EL QR DESDE LOS DATOS DE DOC Y LA FIRMAS PARA GENERAR UNA IMAGEN EN LA CARPETA Y USARLO EN EL PDF*/
            try
            {
                this.generarImgQR(cadenaQR);
            }
            catch (Exception e)
            {
                return "";
            }
            //htmlString = File.ReadAllText(UrlPlantilla + "plantilla_tkt\\htmlterminado\\ticketTerminado.html").ToString();
            htmlString = htmlString.Replace("[URL_QR]", UrlPlantilla + "QR\\QR.png");

            if (File.Exists(UrlGeneratePdf))
            {
                try
                {
                    File.Delete(UrlGeneratePdf);
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            if (!Directory.Exists(UrlCarpetaPdf))
            {
                Directory.CreateDirectory(UrlCarpetaPdf);
            }
            try
            {
                Document document = new Document(new Rectangle(widthPdf, heightPdf), 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(UrlGeneratePdf, FileMode.Create));

                document.Open();
                XMLWorkerHelper xmlWorkerHelper = XMLWorkerHelper.GetInstance();

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                xmlWorkerHelper.ParseXHtml(writer, document, new StringReader(htmlString));
                document.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Task.Run(() =>
                {
                    this.DeleteImagenQR();
                });
            }
            return "ok";
        }
    }
}

