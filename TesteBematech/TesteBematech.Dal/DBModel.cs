namespace TesteBematech.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DataEntrega { get; set; }

        public string NumeroPedido { get; set; }

        public decimal ValorTotal { get; set; }

        [ForeignKey("Cliente")]
        public int? IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<ItemPedido> ItensPedido { get; set; }
    }

    public class ItemPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, 999.99, ErrorMessage = "Quantidade deve estar entre 0 e 999,99")]
        public decimal Quantidade { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorTotal { get; set; }

        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }

        public virtual Pedido Pedido { get; set; }

        public virtual Produto Produto { get; set; }
    }

    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(50, ErrorMessage = "Limite de 50 caracteres excedido")]
        public string Nome { get; set; }

        [MinLength(11, ErrorMessage = "CPF deve conter 11 digitos")]
        [MaxLength(11, ErrorMessage = "CPF deve conter 11 digitos")]
        public string Cpf { get; set; }
    }

    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(50, ErrorMessage = "Limite de 50 caracteres excedido")]
        public string Nome { get; set; }

        public decimal Valor { get; set; }
    }
}