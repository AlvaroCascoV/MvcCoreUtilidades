using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }
        //podemos usar el cache que viene incluido en net
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida() 
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }

        //tambien podemos usar un cache personalizado de nuget
        public IActionResult MemoriaPersonalizada(int? tiempo) 
        {
            if (tiempo == null)
            {
                tiempo = 60;
            }
            
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            //COMO ESTO ES MANUAL, DEBEMOS PREGUNTAR SI EXISTE ALGO EN CACHE O NO
            if(this.memoryCache.Get("FECHA") == null){
                //NO EXISTE CACHE TODAVIA
                //CREAMOS UN OBJETO ENTRY OPTIONS CON EL TIEMPO
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));

                this.memoryCache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "fecha almacenada";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                //EXISTE CACHE
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "fecha recuperada";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }
    }
}
