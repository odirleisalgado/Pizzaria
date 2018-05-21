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
    /// Interaction logic for CadastroPizza.xaml
    /// </summary>
    public partial class CadastroPizza : Window
    {
        public static List<Pizza> novaLista = new List<Pizza>();
        public CadastroPizza()
        {
            InitializeComponent();   
            novaLista= Controller.PizzaController.retornaSabores();
            DtGrid.ItemsSource = null;
            DtGrid.ItemsSource = novaLista;
            txtNovoSabor.Focus();
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCadSabor_Click(object sender, RoutedEventArgs e)
        {
            if (txtNovoSabor.Text.Equals(""))
            {
                MessageBox.Show("Por favor, Insira a Descrição", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Pizza novoSabor = new Pizza();

                novoSabor.SaborPizza = txtNovoSabor.Text;
                Controller.PizzaController.SalvarNovoSabor(novoSabor);
                DtGrid.ItemsSource = null;
                DtGrid.ItemsSource= Controller.PizzaController.retornaSabores();
                // DtGrid.Items.Refresh();
                MessageBox.Show("Novo Sabor Cadastrado Com Sucesso!!!", "Sucesso!", MessageBoxButton.OK);
                txtNovoSabor.Clear();
                txtNovoSabor.Focus();
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            bool resp;
            try
            {
                resp = Controller.PizzaController.ExcluirPizza(int.Parse(DtGrid.SelectedValue.ToString()));

                if (resp.Equals(true))
                {
                    MessageBox.Show("Item Excluído com Sucesso!!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    DtGrid.ItemsSource = null;
                    DtGrid.ItemsSource = Controller.PizzaController.retornaSabores();                  
                }            
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione um Item !!!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                Pizza nova = new Pizza();
                nova = Controller.PizzaController.retornaDescricao(int.Parse(DtGrid.SelectedValue.ToString()));
                txtEditarItem.Text = nova.SaborPizza.ToString();
                txtNovoSabor.IsEnabled = false;
                btnCadSabor.IsEnabled = false;
                btnExcluir.IsEnabled = false;

                txtEditarItem.Visibility = Visibility.Visible;
                btnSalvarAlt.Visibility = Visibility.Visible;
               
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione Um Item!!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSalvarAlt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.PizzaController.alterarDados(int.Parse(DtGrid.SelectedValue.ToString()), txtEditarItem.Text);
                DtGrid.ItemsSource= Controller.PizzaController.retornaSabores();
                txtNovoSabor.IsEnabled = true;
                btnCadSabor.IsEnabled = true;
                btnExcluir.IsEnabled =true;
                txtEditarItem.Visibility = Visibility.Hidden;
                btnSalvarAlt.Visibility = Visibility.Hidden;
               
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione um Item!!");

            }
        }

        private void txtNovoSabor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                btnCadSabor_Click(this, new RoutedEventArgs());
            }
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
    