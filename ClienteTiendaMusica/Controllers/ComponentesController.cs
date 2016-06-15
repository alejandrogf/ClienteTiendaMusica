using System;
using System.Collections;
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
    public class ComponentesController : Controller
    {
        [Dependency]
        public IServiciosRest<Componentes> Servicio { get; set; }
        [Dependency]
        public IServiciosRest<Categorias> ServicioCat { get; set; }
        // GET: Componentes
        public ActionResult Index()
        {
            var data = Servicio.Get();
            ViewBag.Categorias = ListaCat();
            return View(data);
        }

        public ActionResult VerComp(int idCat, string nombreCat)
        {
            var data = Servicio.Get().Where(o=>o.Categoria==idCat);
            ViewBag.NombreCat = nombreCat;
            return View(data);
        }


        public SelectList ListaCat()
        {
            var LCat = ServicioCat.Get();
            return new SelectList(LCat.ToArray(),
                                "ID",
                                "Nombre");
        }

        public ActionResult VerConfiguracion(Array datos)
        {
            var data = Servicio.Get();
            
            return View();
        }
    }
}