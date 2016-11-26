using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TesteBematech.Dal;
using TesteBematech.Models;

namespace TesteBematech.ApiControllers
{
    public class PedidoController : ApiController
    {
        private DBModel db = new DBModel();
        private const int quantidadePorPaginaDefault = 2;

        // GET: api/Pedido/Listar
        // GET: api/Pedido/Listar?pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Pedido> Listar(int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault, string order = "", string dir = "")
        {
            var list = Filtrar(order: order, dir: dir);

            if (pagina.HasValue && quantidadePorPagina.HasValue)
            {
                return list.Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }
            else return list;
        }

        // GET: api/Pedido/Buscar?busca=<busca>
        // GET: api/Pedido/Buscar?busca=<busca>&pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Pedido> Buscar(int? idCliente = null, string numeroPedido = "", DateTime? dataIni = null, DateTime? dataFim = null, int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault, string order = "", string dir = "")
        {
            var list = Filtrar(idCliente, numeroPedido, dataIni, dataFim, order, dir);

            if (pagina.HasValue && quantidadePorPagina.HasValue && !string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(dir))
            {
                return list.Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }

            else return list;
        }

        // GET: api/Pedido/QuantidadePaginas?busca=<busca>
        // GET: api/Pedido/QuantidadePaginas?busca=<busca>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public int QuantidadePaginas(int? idCliente = null, string numeroPedido = "", DateTime? dataIni = null, DateTime? dataFim = null, int quantidadePorPagina = quantidadePorPaginaDefault)
        {
            var totalRegistros = Filtrar(idCliente, numeroPedido, dataIni, dataFim, "", "").Count();
            var totalPaginas = (totalRegistros - 1) / quantidadePorPagina + 1;

            return totalPaginas;
        }

        // GET: api/Pedido/Buscar/<id>
        [AcceptVerbs("GET")]
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult Buscar(int id)
        {
            Pedido Pedido = db.Pedido.Find(id);
            if (Pedido == null)
            {
                return NotFound();
            }

            return Ok(Pedido);
        }

        // POST: api/Pedido/Salvar
        [ResponseType(typeof(void))]
        public IHttpActionResult Salvar(Pedido Pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Pedido.Id > 0)
            {
                db.Entry(Pedido).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(Pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                db.Pedido.Add(Pedido);
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pedido/Excluir
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult Excluir(int id)
        {
            Pedido Pedido = db.Pedido.Find(id);
            if (Pedido == null)
            {
                return NotFound();
            }

            db.Pedido.Remove(Pedido);
            db.SaveChanges();

            return Ok(Pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedido.Count(e => e.Id == id) > 0;
        }

        private IQueryable<Pedido> Filtrar(int? idCliente = null, string numeroPedido = "", DateTime? dataIni = null, DateTime? dataFim = null, string order = "", string dir = "")
        {
            if(dataFim.HasValue)
                dataFim = dataFim.Value.AddDays(1);

            var list = db.Pedido
                .Where(p =>
                    idCliente.HasValue ? idCliente == p.IdCliente : true &&
                    numeroPedido.Length > 0 ? p.NumeroPedido.Contains(numeroPedido) : true &&
                    dataIni.HasValue ? p.DataEntrega >= dataIni : true &&
                    dataFim.HasValue ? p.DataEntrega <= dataFim : true);
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(dir))
                return list;
            else
                return list.OrderBy(order, dir);
        }
    }
}