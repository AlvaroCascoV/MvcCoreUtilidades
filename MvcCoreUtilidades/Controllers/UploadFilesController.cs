using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        //con esto podemos recuperar las rutas fisicas independientemente de donde estemos
        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            //VAMOS A SUBIR EL FICHERO A LOS ELEMENTOS TEMPORALES
            //DEL EQUIPO
            //string tempFolder = Path.GetTempPath();

            //AHORA NECESITAMOS LA RUTA HACIA LA CARPETA wwwroot
            string rootFolder = this.hostEnvironment.WebRootPath;

            string fileName = fichero.FileName;
            //CUANDO PENSAMOS EN FICHEROS Y SUS RUTAS
            //ESTAMOS PENSANDO EN ALGO PARECIDO A ESTO:
            //C:\misficheros\carpeta\1.txt
            //NET CORE NO ES WINDOWS Y ESTA RUTA ES DE WINDOWS.
            //LAS RUTAS DE LINUX Y MACOS PUEDEN SER DISTINTAS
            //DEBEMOS CREAR RUTAS CON HERRAMIENTAS DE NET CORE: Path
            string path = Path.Combine(/*tempFolder*/ rootFolder, "uploads", fileName);
            //PARA SUBIR FICHEROS UTILIZAMOS Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["FILENAME"] = fileName;
            return View();
        }
    }
}
