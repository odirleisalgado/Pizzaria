using Modelos;
using Modelos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Controller
{
    public class PedidoController
    {
        public static int UltimoTelefone = 0;
        public static Pedido ultimoPedido = new Pedido();
      
       
        //Métodos

        public static void SalvarPedido(Pedido novoPedido)
        {
            ultimoPedido = novoPedido; //guarda as informações do último pedido realizado para ser usado na tela de Pedido Finalizado

            //int id = ultimoID + 1;
            //ultimoID = id;
            //novoPedido.PedidoID = ultimoID;
            //pedidos.Add(novoPedido);

           Contexto ctx = new Contexto();
           ctx.Pedidos.Add(novoPedido);
           ctx.SaveChanges();
            

        }

        //Armazena o telefone que foi previamente digitado na pesquisa
        public static void GuardaTelefone(int telefone)
        {
            UltimoTelefone = telefone;
        }

        //Retorna o telefone digitado no campo de pesquisa
        public static string RetornaTelefone()
        {
            return UltimoTelefone.ToString();

        }

        public static List<Pedido> RetornaPedidos()
        {
            Contexto ctx = new Contexto();
            var p = ctx.Pedidos
                .Include(a => a.cli)
                .ToList();
            return p;   


           // return ctx.Pedidos.ToList();
        }
       
        public static Pedido retornaPedidoPorId(int idPedido)
        {
            Contexto ctx = new Contexto();
            List<Item> listaR = Controller.ItemController.retornaListaItem();
            Pedido novoP= ctx.Pedidos.Include(p => p.cli).Where(b => b.PedidoID == idPedido).FirstOrDefault();

            foreach (var x in listaR)
            {
                if (x.PedidoID==novoP.PedidoID)
                {
                    novoP.ListaItens.Add(x);
                }
            }


            return novoP;
          
        }

        public static void excluirPedidoPorCliente(int idCliente)
        {
            List<Pedido> novo = RetornaPedidos();
            List<Item> itens = Controller.ItemController.retornaListaItem();
           


            foreach (var x in novo)
            {
                if (x.clienteId==idCliente)
                {
                    ItemController.excluirItem(x.PedidoID);

                    Contexto ctx = new Contexto();
                    Pedido p = ctx.Pedidos.Find(x.PedidoID);

                    ctx.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
           
            Controller.ClienteController.ExcluirCliente(idCliente);
            
        }

    }
}
