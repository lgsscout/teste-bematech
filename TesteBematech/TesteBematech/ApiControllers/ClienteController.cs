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
    public class ClienteController : ApiController
    {
        private DBModel db = new DBModel();
        private const int quantidadePorPaginaDefault = 10;

        // GET: api/Cliente/Listar
        // GET: api/Cliente/Listar?pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Cliente> Listar(int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault)
        {
            if(pagina.HasValue && quantidadePorPagina.HasValue)
            {
                return db.Cliente.OrderBy(c => c.Nome).Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }
            else return db.Cliente;
        }

        // GET: api/Cliente/Buscar?busca=<busca>
        // GET: api/Cliente/Buscar?busca=<busca>&pagina=<pagina>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public IQueryable<Cliente> Buscar(string busca = "", int? pagina = null, int? quantidadePorPagina = quantidadePorPaginaDefault)
        {
            if (pagina.HasValue && quantidadePorPagina.HasValue)
            {
                return db.Cliente.OrderBy(c => c.Nome).Where(c => c.Nome.Contains(busca) || c.Cpf.Contains(busca)).Skip(quantidadePorPagina.Value * (pagina.Value - 1)).Take(quantidadePorPagina.Value);
            }
            else return db.Cliente.Where(c => c.Nome.Contains(busca) || c.Cpf.Contains(busca));
        }

        // GET: api/Cliente/QuantidadePaginas?busca=<busca>
        // GET: api/Cliente/QuantidadePaginas?busca=<busca>&quantidadePorPagina=<quantidadePorPagina>
        [AcceptVerbs("GET")]
        public int QuantidadePaginas(string busca = "", int quantidadePorPagina = quantidadePorPaginaDefault)
        {
            var totalRegistros = db.Cliente.Where(c => c.Nome.Contains(busca) || c.Cpf.Contains(busca)).Count();
            var totalPaginas = (totalRegistros - 1) / quantidadePorPagina + 1;

            return totalPaginas;
        }

        // GET: api/Cliente/Buscar/<id>
        [AcceptVerbs("GET")]
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult Buscar(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // POST: api/Cliente/Salvar
        [ResponseType(typeof(void))]
        public IHttpActionResult Salvar(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cliente.Id > 0)
            {
                db.Entry(cliente).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
                db.Cliente.Add(cliente);
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cliente/Excluir
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult Excluir(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.Id == id) > 0;
        }
    }
}