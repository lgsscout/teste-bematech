using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBematech.Dal;

namespace TesteBematech.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DBModel(ConnectionStrings.connString);

            db.Produto.Add(new Produto { Id = 1, Nome = "Produto 1", Valor = 10 });
            db.SaveChanges();
        }
    }
}
