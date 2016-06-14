using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BaseServicio;
using ClienteTiendaMusica.Models;
using Microsoft.Practices.Unity;

namespace ClienteTiendaMusica.Controllers
{
    public class CategoriasController : Controller
    {
        [Dependency]
        public IServiciosRest<Categorias> Servicio { get; set; }
        // GET: Tipo
        public ActionResult Index()
        {
            var data = Servicio.Get();
            return View(data);
        }

        public ActionResult Alta()
        {
            return View(new Categorias());
        }

        [HttpPost]
        public async Task<ActionResult> Alta(Categorias model)
        {
            var data = await Servicio.Add(model);
            return RedirectToAction("Index");
        }
    }
}