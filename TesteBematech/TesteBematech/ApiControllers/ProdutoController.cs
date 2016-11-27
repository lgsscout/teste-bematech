using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TesteBematech.Dal;

namespace TesteBematech.ApiControllers
{
    public class ProdutoController : ApiController
    {
        private DBModel db = new DBModel();
        private const int quantidadePorPaginaDefault = 10;

        // GET: api/Produto/Listar
        // GET: api/Produto/Listar?pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Produto> Listar(int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault)
        {
            if(pagina.HasValue && quantidadePorPagina.HasValue)
            {
                return db.Produto.OrderBy(c => c.Nome).Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }
            else return db.Produto;
        }

        // GET: api/Produto/Buscar?busca=<busca>
        // GET: api/Produto/Buscar?busca=<busca>&pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Produto> Buscar(string busca = "", int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault)
        {
            if (pagina.HasValue && quantidadePorPagina.HasValue)
            {
                return db.Produto.OrderBy(c => c.Nome).Where(c => c.Nome.Contains(busca)).Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }
            else return db.Produto.Where(c => c.Nome.Contains(busca));
        }

        // GET: api/Produto/QuantidadePaginas?busca=<busca>
        // GET: api/Produto/QuantidadePaginas?busca=<busca>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public int QuantidadePaginas(string busca = "", int quantidadePorPagina = quantidadePorPaginaDefault)
        {
            var totalRegistros = db.Produto.Where(c => c.Nome.Contains(busca)).Count();
            var totalPaginas = (totalRegistros - 1) / quantidadePorPagina + 1;

            return totalPaginas;
        }

        // GET: api/Produto/Buscar/<id>
        [AcceptVerbs("GET")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult Buscar(int id)
        {
            Produto Produto = db.Produto.Find(id);
            if (Produto == null)
            {
                return NotFound();
            }

            return Ok(Produto);
        }

        // POST: api/Produto/Salvar
        [ResponseType(typeof(void))]
        public IHttpActionResult Salvar(Produto Produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Produto.Id > 0)
            {
                db.Entry(Produto).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(Produto.Id))
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
                db.Produto.Add(Produto);
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produto/Excluir
        [ResponseType(typeof(Produto))]
        public IHttpActionResult Excluir(int id)
        {
            Produto Produto = db.Produto.Find(id);
            if (Produto == null)
            {
                return NotFound();
            }

            db.Produto.Remove(Produto);
            db.SaveChanges();

            return Ok(Produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produto.Count(e => e.Id == id) > 0;
        }
    }
}