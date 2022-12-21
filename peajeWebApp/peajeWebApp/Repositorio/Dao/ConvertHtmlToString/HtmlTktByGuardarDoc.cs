using peajeWebApp.Models.DatosStoreProcedure;

using static Org.BouncyCastle.Math.EC.ECCurve;

namespace peajeWebApp.Repositorio.Dao.ConvertHtmlToString
{
    public class HtmlTktByGuardarDoc
    {
        private readonly IConfiguration _config;
        public HtmlTktByGuardarDoc(IConfiguration config)
        {
            _config = config;
        }

        private string obtenerDetalles(List<ListaDetalleGuardarDocumento> listaDetalle)
        {
            string UrlPlantilla = _config.GetSection("Constantes:UrlPlantilla").Value!;
            string stringHtmlDetalles = "";

            var readText = File.ReadAllText(UrlPlantilla + "/plantilla_tkt/htmlExtras/parteDetalle.html").ToString();
            foreach (ListaDetalleGuardarDocumento item in listaDetalle)
            {
                var nuevo = readText.Replace("[DESCRIPCION]", item.Descripcion);
                nuevo = nuevo.Replace("[IMPORTETOTALSINIMPUESTO]", item.ImporteTotalSinImpuesto);
                stringHtmlDetalles += nuevo;
            }

            return stringHtmlDetalles;
        }
        private string obtenerParteFinalesDetalle(DatosGuardarDoc guardarDoc)
        {
            string datoResult = "";

            //SUB TOTAL
            datoResult += "<tr>" +
                                "<td class='Texto bold'>SUB-TOTAL</td>" +
                                "<td class='Texto txtRigth'>S/.</td>" +
                                "<td class='Texto txtRigth'>[SUBTOTAL]</td>" +
                            "</tr>";
            datoResult = datoResult.Replace("[SUBTOTAL]", guardarDoc.TotalValorVentaNetoOpGravadas.Length > 0
                                                                ? guardarDoc.TotalValorVentaNetoOpGravadas
                                                                : (guardarDoc.TotalValorVentaNetoOpNoGravada.Length > 0
                                                                    ? guardarDoc.TotalValorVentaNetoOpNoGravada
                                                                    : guardarDoc.TotalValorVentaNetoOpExonerada
                                                                   )
                                                                );
            //IGV
            datoResult += "<tr>" +
                                "<td class='Texto bold'>IGV 18%</td>" +
                                "<td class='Texto txtRigth'>S/.</td>" +
                                "<td class='Texto txtRigth'>[TOTALIGV]</td>" +
                            "</tr>";
            datoResult = datoResult.Replace("[TOTALIGV]", guardarDoc.TotalIGV);

            // MONTO DETRACCION
            if (guardarDoc.TextoAuxiliar250_5.Length > 0 && guardarDoc.TextoAuxiliar250_6.Length > 0)
            {
                datoResult += "<tr>" +
                                    "<td class='Texto bold'>MONTO POR DETRACCION</td>" +
                                    "<td class='Texto txtRigth'>S/.</td>" +
                                    "<td class='Texto txtRigth'>[TEXTOAUXILIAR250_6]</td>" +
                                "</tr>";
                datoResult = datoResult.Replace("[TEXTOAUXILIAR250_6]", guardarDoc.TextoAuxiliar250_6);
            }

            //IMPORTE TOTAL

            datoResult += "<tr>" +
                                "<td class='Texto bold' style='padding-top: 5px;'>TOTAL</td>" +
                                "<td class='Texto txtRigth' style='padding-top: 5px;'>S/.</td>" +
                                "<td class='Texto txtRigth' style='padding-top: 5px;'>[TOTALVENTA]</td>" +
                            "</tr>";
            datoResult = datoResult.Replace("[TOTALVENTA]", guardarDoc.TotalVenta);

            // NRO DETRACCION
            if (guardarDoc.TextoAuxiliar250_5.Length > 0 && guardarDoc.TextoAuxiliar250_6.Length > 0)
            {
                datoResult += "<tr>" +
                                    "<td class='Texto bold'>Nro DETRACCION</td>" +
                                    "<td></td>" +
                                    "<td class='Texto txtRigth'>[TEXTOAUXILIAR250_5]</td>" +
                                "</tr>";
                datoResult = datoResult.Replace("[TEXTOAUXILIAR250_5]", guardarDoc.TextoAuxiliar250_5);
            }
            return datoResult;
        }
        private string obtenerParteFactura(DatosGuardarDoc guardarDoc)
        {

            string UrlPlantilla = _config.GetSection("Constantes:UrlPlantilla").Value!;
            string stringHtmlDetalles = "";

            var readText = File.ReadAllText(UrlPlantilla + "/plantilla_tkt/htmlExtras/parteFactura.html").ToString();
            
            var nuevo = readText.Replace("[NUMERODOCUMENTOADQUIRIENTE]", guardarDoc.NumeroDocumentoAdquiriente);
            nuevo = nuevo.Replace("[RAZONSOCIALADQUIRIENTE]", guardarDoc.RazonSocialAdquiriente);
            nuevo = nuevo.Replace("[FORMAPAGO]", (guardarDoc.Formapago.Equals("") || guardarDoc.Formapago.ToLower().Equals("contado")) ? "Contado" : "Credito");

            //nuevo = nuevo.Replace("[DIRECCIONADQUIRIENTE]", guardarDoc.DireccionAdquiriente);
            //nuevo = nuevo.Replace("[ANTICIPOS]", "");

            stringHtmlDetalles += nuevo;

            return stringHtmlDetalles;
        }


        public string ConvertHtmlToString(DatosGuardarDoc guardarDoc)
        {
            string UrlPlantilla = _config.GetSection("Constantes:UrlPlantilla").Value!;
            string stringHtml = "";

            var readText = File.ReadAllText(UrlPlantilla + "/plantilla_tkt/ticket.html").ToString();

            string tipoDoc = "";
            switch (guardarDoc.TipoDocumento)
            {
                case "01":
                    tipoDoc = "FACTURA ELECTRÓNICA";
                    break;
                case "03":
                    tipoDoc = "BOLETA DE VENTA ELECTRÓNICA";
                    break;
                case "07":
                    tipoDoc = "NOTA DE CREDITO";
                    break;
                case "08":
                    tipoDoc = "NOTA DE DEBITO";
                    break;
                default:
                    tipoDoc = "no identificado";
                    break;
            }

            var nuevo = readText.Replace("[RAZONSOCIALEMISOR]", guardarDoc.RazonSocialEmisor);
            nuevo = nuevo.Replace("[TEXTOAUXILIAR250_7]", guardarDoc.TextoAuxiliar250_7);
            nuevo = nuevo.Replace("[TEXTOAUXILIAR250_3]", guardarDoc.TextoAuxiliar250_3);
            nuevo = nuevo.Replace("[DIRECCIONEMISOR]", guardarDoc.DireccionEmisor);
            nuevo = nuevo.Replace("[DISTRITOEMISOR]", guardarDoc.DistritoEmisor);
            nuevo = nuevo.Replace("[PROVINCIAEMISOR]", guardarDoc.ProvinciaEmisor);
            nuevo = nuevo.Replace("[DEPARTAMENTOEMISOR]", guardarDoc.DepartamentoEmisor);
            nuevo = nuevo.Replace("[FECHAEMISION]", guardarDoc.FechaEmision);
            nuevo = nuevo.Replace("[HORAEMISOR]", guardarDoc.HoraEmision);
            nuevo = nuevo.Replace("[NUMERODOCUMENTOEMISOR]", guardarDoc.NumeroDocumentoEmisor);

            string[] numeroSerie = guardarDoc.SerieNumero.Split("-");
            nuevo = nuevo.Replace("[SERIENUMERO1]", numeroSerie[0]);
            nuevo = nuevo.Replace("[SERIENUMERO2]", numeroSerie[1]);
            nuevo = nuevo.Replace("[TIPODOCUMENTO]", tipoDoc);

            nuevo = nuevo.Replace("[TEXTOLEYENDA_1]", guardarDoc.TextoLeyenda_1.Replace("Son: ", ""));
            nuevo = nuevo.Replace("[TEXTOAUXILIAR250_2]", guardarDoc.TextoAuxiliar250_2);
            nuevo = nuevo.Replace("[TEXTOAUXILIAR250_1]", guardarDoc.TextoAuxiliar250_1);

            if (guardarDoc.TipoDocumento.Equals("01"))
            {
                nuevo = nuevo.Replace("[PARTE_FACTURA]", this.obtenerParteFactura(guardarDoc));
            }

            nuevo = nuevo.Replace("[PARTE_DETALLE]", this.obtenerDetalles(guardarDoc.listaDetalleGuardarDocumentos));
            nuevo = nuevo.Replace("[PARTE_DATOSFINALESDETALLE]", this.obtenerParteFinalesDetalle(guardarDoc));
            stringHtml = nuevo;

            return stringHtml;
        }
    }
}
