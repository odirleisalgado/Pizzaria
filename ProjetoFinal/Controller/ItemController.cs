using Modelos;
using Modelos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ItemController
    {
       public static void salvarItem(Item novoItem)
       {
            Contexto ctx = new Contexto();
            ctx.Items.Add(novoItem);
            ctx.SaveChanges();
        }
      
       public static Item retornaItem(int id)
        {
            Contexto ctx = new Contexto();

            return ctx.Items.Find(id);
        }

        public static List<Item> retornaListaItem()
        {
            Contexto ctx = new Contexto();
           
            return ctx.Items.ToList();
        }

        public static void excluirItem(int id)
        {
            Contexto ctx = new Contexto();
            Item c = ctx.Items.Find(id);

            ctx.Entry(c).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
           
        }

        //public static List<Item> PesquisaItemsPorIdPedido(int idPe)
        //{
        //    Contexto ctx = new Contexto();
        //    List<Item> l = ctx.Items.ToList();
        //    List<Item> lista = new List<Item>();
        //    foreach (var x in l)
        //    {
        //        if (x.idPedido==idPe)
        //        {
        //            lista.Add(x);
        //        }
        //    }
        //    return lista;

        //}
    }
}
