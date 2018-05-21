using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pizzaria
{
    /// <summary>
    /// Interaction logic for DetalhesPedido.xaml
    /// </summary>
    public partial class DetalhesPedido : Window
    {
        public DetalhesPedido(int idPedido)
        {
            InitializeComponent();
            Pedido recebePedido= Controller.PedidoController.retornaPedidoPorId(idPedido);
           // List<Item> lista = Controller.ItemController.PesquisaItemsPorIdPedido(idPedido);
            Cliente c = Controller.ClienteController.retornaClientePorId(recebePedido.clienteId);
            
            txtNome.Text = recebePedido.cli.Nome;
            txtTelefone.Text = c.Telefone.ToString();
            txtEndereco.Text = c.Endereco;
            txtNumero.Text = c.Numero.ToString();
            txtBairro.Text = c.Bairro;
            txtTotal.Text = recebePedido.Total.ToString();
            txtCodigo.Text = recebePedido.PedidoID.ToString();
            txtData.Text = recebePedido.DataPedido;
            listFinal.ItemsSource = null;
            listFinal.ItemsSource = recebePedido.ListaItens; 

           



        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
