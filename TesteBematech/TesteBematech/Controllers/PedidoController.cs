using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteBematech.Dal;

namespace TesteBematech.Controllers
{
    public class PedidoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pedido()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pedido(Pedido pedido)
        {
            return View(pedido);
        }
    }
}