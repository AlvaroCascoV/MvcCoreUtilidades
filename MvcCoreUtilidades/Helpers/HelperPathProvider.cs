using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers
{
    //Necesitamos una enumeracion con las carpetas que deseemos subir ficheros
    public enum Folders { Uploads, Images, Facturas, Temporal, Productos }
    public class HelperPathProvider
    {
        //el iwebhost irá aqui en vez de en el controller
        private IWebHostEnvironment hostEnvironment;
        //server para recuperar la url del server
        private IServer server;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server;
        }

        //TENDREMOS UN METODO QUE SE ENCARGARA DE RESOLVER LA RUTA
        //COMO STRING CUANDO RECIBAMOS EL FICHERO Y LA CARPETA
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "Images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "Uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "Facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "Temporal";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "Images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "Uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "Facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "Temporal";
            }
            else if (folder == Folders.Productos)
            {
                //ESTO CAMBIA PORQUE NECESITAMOS LA RUTA WEB
                carpeta = "images/productos";
            }
            //http:localhost:999/images/productos/1.png
            //Quiero buscar la forma de recuperar la URL de nuestro Server
            //en MVC Net Core
            //se puede hacer con web provider, ruta relativa o con IServer
            var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            //DEVOLVEMOS LA RUTA URL
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}
