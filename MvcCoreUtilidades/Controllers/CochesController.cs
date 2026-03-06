using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private RepositoryCoches repo;

        public CochesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        //ESTA SERA LA VISTA PRINCIPAL
        public IActionResult Index()
        {
            return View();
        }
        //TENDREMOS UN IACTIONRESULT PARCIAL
        //PARA INTEGRAR DENTRO DE INDEX
        public IActionResult _CochesPartial()
        {
            //DEBEMOS DEVOLVER EL DIBUJO QUE DESEEMOS EN
            //AJAX. INDICAMOS EL NOMBRE DEL DICHERO
            //CSHTML Y SU MODEL (si tuviera)
            List<Coche> cars = this.repo.GetCoches();
            return PartialView("_CochesPartial", cars);
        }

        public IActionResult _CochesDetails(int idcoche) 
        {
            List<Coche> cars = this.repo.GetCoches();
            Coche car = cars.Find(x => x.IdCoche == idcoche);
            return PartialView("_CochesDetailsView", car);
        }

        public IActionResult Details(int idcoche)
        {
            Coche car = this.repo.FindCoche(idcoche);
            return View(car);
        }
    }
}
