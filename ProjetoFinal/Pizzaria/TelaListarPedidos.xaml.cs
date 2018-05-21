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
    /// Interaction logic for TelaListarPedidos.xaml
    /// </summary>
    public partial class TelaListarPedidos : Window
    {
        public TelaListarPedidos()
        {
            InitializeComponent();

            DtGridPedido.ItemsSource = null;
            DtGridPedido.ItemsSource = Controller.PedidoController.RetornaPedidos();

        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DtGridPedido_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(DtGridPedido.SelectedValue.ToString());

           // Controller.PedidoController.retornaPedidoPorId(int.Parse(DtGridPedido.SelectedValue.ToString()));
            
            DetalhesPedido novaTela = new DetalhesPedido(int.Parse(DtGridPedido.SelectedValue.ToString()));
            novaTela.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                btnFechar_Click(this, new RoutedEventArgs());
            }
        }
    }
}
