using Modelos;
using Modelos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class PizzaController
    {
        //Métodos

        public static void SalvarNovoSabor(Pizza novo)
        {
            Contexto ctx = new Contexto();
            ctx.Pizzas.Add(novo);
            ctx.SaveChanges();
        }

        public static List<Pizza> retornaSabores()
        {
            Contexto ctx = new Contexto();
            return ctx.Pizzas.ToList();
        }

        public static bool ExcluirPizza(int idpizza)
        {
            Contexto ctx = new Contexto();
            Pizza p = ctx.Pizzas.Find(idpizza);

            ctx.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public static Pizza retornaDescricao(int id)
        {
            Contexto ctx = new Contexto();
           
            return ctx.Pizzas.Find(id);
        }

        public static void alterarDados(int id, string novaDesc)
        {
            Contexto ctx = new Contexto();
            
            Pizza p = ctx.Pizzas.Find(id);
            p.SaborPizza = novaDesc;
            ctx.SaveChanges();          
        }
    }
}
