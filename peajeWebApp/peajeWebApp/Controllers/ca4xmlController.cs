using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

using peajeWebApp.Models;
using peajeWebApp.Repositorio.IDao;

using System.Reflection.Metadata;

namespace peajeWebApp.Controllers
{
    public class ca4xmlController : Controller
    {
        private readonly IProcesarDatos_Repositorio _procesarDatos;
        private readonly IGenerarPdf _generate;
        private DateTime tiempoIni = DateTime.Now;
        public ca4xmlController(IProcesarDatos_Repositorio ProcesarDatos, IGenerarPdf generate)
        {
            _procesarDatos = ProcesarDatos;
            _generate = generate;
        }

        [HttpPost]
        public async Task<IActionResult> Index(DatosEntrada datosEntrada)
        {
            #region TEMPORAL
            await Task.Run(() =>
            {
                using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt"))
                {
                    string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                    this.tiempoIni = DateTime.Now;
                    file.WriteLine($"Inicio -> {hora}.");
                }
            });
            #endregion

            string NrotipoDocumento = datosEntrada.Datos.Substring(0, 2);
            if (!(NrotipoDocumento == "01" || NrotipoDocumento == "03" || NrotipoDocumento == "07" || NrotipoDocumento == "08" || NrotipoDocumento == "RA" || NrotipoDocumento == "RC"))
            {
                string error = "ERROR|EL TRAMO NO ES VALIDO O NO EXISTE UN EL TIPO DE DOCUMENTO";
                #region TEMPORAL
                await Task.Run(() =>
                {
                    using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                    {
                        string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                        file.WriteLine($"\nFIN-error -> {hora}.");
                    }
                });
                #endregion
                return Ok(error);
            }
            else if (NrotipoDocumento == "01" || NrotipoDocumento == "03" || NrotipoDocumento == "07" || NrotipoDocumento == "08")
            {
                string tipoDoc = datosEntrada.Datos.Substring(3, 1);
                if (!(tipoDoc == "F" || tipoDoc == "B"))
                {
                    string error = "ERROR|NO SE PUDO IDENTIFICAR SI ES BOLETA O FACTURA";
                    #region TEMPORAL
                    await Task.Run(() =>
                    {
                        using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt"))
                        {
                            string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                            file.WriteLineAsync($"FIN-error -> {hora}.");
                        }
                    });
                    #endregion
                    return Ok(error);
                }
            }
            /*EXTRAEMOS LOS DATOS DE LA TRAMA*/
            List<List<List<string>>> datosTramo = new();
            datosTramo = _procesarDatos.GetDatosByTramo(datosEntrada.Datos);

            //GUARDAR DOCUMENTO
            if (NrotipoDocumento == "01" || NrotipoDocumento == "03" || NrotipoDocumento == "07" || NrotipoDocumento == "08")
            {
                string resultado = await _procesarDatos.EjecutarGuardarDocumento(datosTramo, datosEntrada.Comando!);

                #region TEMPORAL
                await Task.Run(() =>
                {
                    using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                    {
                        string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                        //int tiempoFin = Int32.Parse(DateTime.Now.ToString("ss"));
                        DateTime tiempoFin = DateTime.Now;
                        double tiempoTranscurrido = tiempoFin.Subtract(this.tiempoIni).TotalSeconds;
                        //file.WriteLineAsync($"FIN GUARDAR DOC -> {hora}.\n\ntiempo transcurrido -> {tiempoFin - this.tiempoIni}");
                        file.WriteLineAsync($"FIN GUARDAR DOC -> {hora}.\n\ntiempo transcurrido -> {tiempoTranscurrido}");
                    }
                });
                #endregion

                return Ok(resultado);
            }
            //BAJA DOCUMENTO
            else
            {
                string resultado = await _procesarDatos.EjecutarBajarDocumento(datosTramo, datosEntrada.Comando!);

                #region TEMPORAL
                await Task.Run(() =>
                {
                    using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\Nueva carpeta\\TiempoEjec.txt", append: true))
                    {
                        string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                        file.WriteLineAsync($"FIN BAJA DOC -> {hora}.");
                    }
                });
                #endregion

                return Ok(resultado);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Prueba()
        {
            #region TEMPORAL
            await Task.Run(() =>
            {
                using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\logs\\TiempoEjec.txt"))
                {
                    string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                    this.tiempoIni = DateTime.Now;
                    file.WriteLine($"Inicio -> {hora}.");
                }
            });
            #endregion
            #region TEMPORAL
            await Task.Run(() =>
            {
                using (StreamWriter file = new StreamWriter("C:\\Users\\carlo\\Desktop\\logs\\TiempoEjec.txt", append: true))
                {
                    string hora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
                    file.WriteLineAsync($"FIN -> {hora}.");
                }
            });
            #endregion
            return Ok("ook");
        }
    }
}
