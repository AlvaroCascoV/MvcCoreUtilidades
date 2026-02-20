using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        //con esto podemos recuperar las rutas fisicas independientemente de donde estemos
        //se mueve a un helper
        //private IWebHostEnvironment hostEnvironment;
        private HelperPathProvider helper;

        public UploadFilesController(/*IWebHostEnvironment hostEnvironment*/ HelperPathProvider helper)
        {
            //this.hostEnvironment = hostEnvironment;
            this.helper = helper;
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
            //string rootFolder = this.hostEnvironment.WebRootPath;
            //modificamos el codigo para usar el helper

            string fileName = fichero.FileName;
            string path = this.helper.MapPath(fileName, Folders.Images);
            string urlPath = this.helper.MapUrlPath(fileName, Folders.Images);

            //CUANDO PENSAMOS EN FICHEROS Y SUS RUTAS
            //ESTAMOS PENSANDO EN ALGO PARECIDO A ESTO:
            //C:\misficheros\carpeta\1.txt
            //NET CORE NO ES WINDOWS Y ESTA RUTA ES DE WINDOWS.
            //LAS RUTAS DE LINUX Y MACOS PUEDEN SER DISTINTAS
            //DEBEMOS CREAR RUTAS CON HERRAMIENTAS DE NET CORE: Path
            //string path = Path.Combine(/*tempFolder*/ rootFolder, "uploads", fileName);
            //PARA SUBIR FICHEROS UTILIZAMOS Stream

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["URLPATH"] = urlPath;
            return View();
        }
    }
}
