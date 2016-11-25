namespace TesteBematech.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public DBModel(string connString)
            : base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<ItemPedido> ItemPedido { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
    }

    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string NumeroPedido { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ItemPedido> ItensPedido { get; set; }
    }

    public class ItemPedido
    {
        public int Id { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}