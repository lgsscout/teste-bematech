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

            var pedido = db.Pedido.FirstOrDefault();
            pedido.Cliente.ToString();
        }
    }
}
