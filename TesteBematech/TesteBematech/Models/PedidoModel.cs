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
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataEntrega { get; set; }
        public string DataEntregaFormat { get { return DataEntrega.ToString("dd-MM-yyyy"); } }
        public decimal ValorTotal { get; set; }
    }

    public class PedidoEdicaoModel
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataEntrega { get; set; }
        public string DataEntregaFormat { get { return DataEntrega.ToString("yyyy-MM-dd"); } }
        public decimal ValorTotal { get; set; }
        public List<ItemPedidoEdicaoModel> ItemPedidoEdicao { get; set; }
    }

    public class ItemPedidoEdicaoModel
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
    }
}