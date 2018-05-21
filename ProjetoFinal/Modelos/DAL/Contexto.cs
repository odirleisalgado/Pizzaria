using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Modelos.DAL
{
    public class Contexto : DbContext 
    {
        public Contexto(): base("stringConn")
        {

        }

        public DbSet<Cliente> Clientes { get; set;  }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Item> Items { get; set; }



    }
}
