using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Pedido(int id)
        {
            return View();
        }
    }
}