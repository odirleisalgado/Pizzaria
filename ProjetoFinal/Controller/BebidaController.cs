using Modelos;
using Modelos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class BebidaController
    {
        //Métodos

        public static void SalvarNovoSabor(Bebida novo)
        {
            Contexto ctx = new Contexto();
            ctx.Bebidas.Add(novo);
            ctx.SaveChanges();
        }

        public static List<Bebida> retornaSabores()
        {
            Contexto ctx = new Contexto();
            return ctx.Bebidas.ToList();
        }

        public static bool ExcluirBebida(int idBebida)
        {
            Contexto ctx = new Contexto();
            Bebida p = ctx.Bebidas.Find(idBebida);

            ctx.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public static Bebida retornaDescricao(int id)
        {
            Contexto ctx = new Contexto();
           
            return ctx.Bebidas.Find(id);
        }

        public static void alterarDados(int id, string novaDesc)
        {
            Contexto ctx = new Contexto();
            
            Bebida p = ctx.Bebidas.Find(id);
            p.SaborBebida = novaDesc;
            ctx.SaveChanges();          
        }
    }
}
