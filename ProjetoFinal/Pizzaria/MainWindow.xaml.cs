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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPedido_Click(object sender, RoutedEventArgs e)
        {
            TelaPedido pedido = new TelaPedido();
            pedido.ShowDialog();
        }

        private void btnPizzas_Click(object sender, RoutedEventArgs e)
        {
            CadastroPizza novoCad = new CadastroPizza();
            novoCad.ShowDialog();          
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            TelaCliente tela = new TelaCliente();
            tela.ShowDialog();
        }

        private void btnListarPedido_Click(object sender, RoutedEventArgs e)
        {
            TelaListarPedidos listar = new TelaListarPedidos();
            listar.ShowDialog();
        }
    }
}
