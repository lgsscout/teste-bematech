using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesteBematech.Dal;

namespace TesteBematech.Models
{
    public class PedidoModel
    {
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Data de Entrega obrigatória")]
        //[CustomValidation((typeof(DateTime), DateTime.Now.D, DateTime.MaxValue)]
        public DateTime DataEntrega { get; set; }
    }
}