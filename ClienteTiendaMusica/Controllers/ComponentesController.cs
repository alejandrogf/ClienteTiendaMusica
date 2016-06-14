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
    public class ComponentesController : Controller
    {
        [Dependency]
        public IServiciosRest<Componentes> Servicio { get; set; }
        // GET: Componentes
        public ActionResult Index()
        {
            var data = Servicio.Get();
            return View(data);
        }

        public ActionResult VerCarrito()
        {
            var data = Servicio.Get();
            return View(data);
        }

        public ActionResult AddCarrito()
        {
            return View(new Componentes());
        }

    }
}