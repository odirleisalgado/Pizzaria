using Modelos;
using Modelos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ClienteController
    {           
        
        // Métodos

        public static void SalvarCliente(Cliente novoCli)
        {
            //int id = ultimoID + 1;
            //ultimoID = id;
            //novoCli.ClienteID = id;
            //Clientes.Add(novoCli);

            Contexto ctx = new Contexto();
            ctx.Clientes.Add(novoCli);
            ctx.SaveChanges();
        }
        
        public static Cliente PesquisaCliPorTel(int tel)
        {
            Contexto ctx = new Contexto();
            List<Cliente> lista = ctx.Clientes.ToList();
            foreach (var item in lista)
            {
                if (item.Telefone==tel)
                {
                    return item;

                }
            }
            return null;

        }

        public static void alterarDados(Cliente cli)
        {

            Contexto ctx = new Contexto();

            Cliente x = ctx.Clientes.Find(cli.ClienteID);
            x.Nome = cli.Nome;
            x.Endereco = cli.Endereco;
            x.Numero = cli.Numero;
            x.Telefone = cli.Telefone;
            x.Bairro = cli.Bairro;
            ctx.SaveChanges();

           
        }

        public static List<Cliente> retornaClientes()
        {
            Contexto ctx = new Contexto();
            return ctx.Clientes.ToList();
        }

        public static bool ExcluirCliente(int id)
        {

            Contexto ctx = new Contexto();
            Cliente c = ctx.Clientes.Find(id);

            ctx.Entry(c).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public static Cliente retornaClientePorId(int id)
        {
            Contexto ctx = new Contexto();
            Cliente c = ctx.Clientes.Find(id);
            return c;
        }

    }
}
