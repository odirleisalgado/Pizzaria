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
    /// Interaction logic for TelaPedidoFinalizado.xaml
    /// </summary>
    public partial class TelaPedidoFinalizado : Window
    {
        public TelaPedidoFinalizado()
        {
            InitializeComponent();

            Pedido recebePedido = new Pedido();
            
            recebePedido = Controller.PedidoController.ultimoPedido;
            
            Cliente c = Controller.ClienteController.retornaClientePorId(recebePedido.clienteId);

            txtNome.Text = c.Nome;
            txtTelefone.Text = c.Telefone.ToString();
            txtEndereco.Text = c.Endereco;
            txtNumero.Text = c.Numero.ToString();
            txtBairro.Text = c.Bairro;
            txtTotal.Text = recebePedido.Total.ToString();
            txtCodigo.Text = recebePedido.PedidoID.ToString();
            txtData.Text = recebePedido.DataPedido;
            listFinal.ItemsSource = recebePedido.ListaItens;

        }    

        private void btnFechar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
