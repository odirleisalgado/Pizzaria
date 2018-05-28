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
  
    public partial class CadastroBebida : Window
    {
        public static List<Bebida> novaLista = new List<Bebida>();
        public CadastroBebida()
        {
            InitializeComponent();
            novaLista = Controller.BebidaController.retornaSabores();
            DtGrid.ItemsSource = null;
            DtGrid.ItemsSource = novaLista;
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
                Bebida novoSabor = new Bebida();

                novoSabor.SaborBebida = txtNovoSabor.Text;
                Controller.BebidaController.SalvarNovoSabor(novoSabor);
                DtGrid.ItemsSource = null;
                DtGrid.ItemsSource = Controller.BebidaController.retornaSabores();
                // DtGrid.Items.Refresh();
                MessageBox.Show("Nova Bebida Cadastrada com sucesso", "Sucesso!", MessageBoxButton.OK);
                txtNovoSabor.Clear();
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            bool resp;
            try
            {
                resp = Controller.BebidaController.ExcluirBebida(int.Parse(DtGrid.SelectedValue.ToString()));

                if (resp.Equals(true))
                {
                    MessageBox.Show("Item Excluído com Sucesso!!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    DtGrid.ItemsSource = null;
                    DtGrid.ItemsSource = Controller.BebidaController.retornaSabores();
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
                Bebida nova = new Bebida();
                nova = Controller.BebidaController.retornaDescricao(int.Parse(DtGrid.SelectedValue.ToString()));
                txtEditarItem.Text = nova.SaborBebida.ToString();
                txtNovoSabor.IsEnabled = false;
                btnCadSabor.IsEnabled = false;
                btnExcluir.IsEnabled = false;

                txtEditarItem.Visibility = Visibility.Visible;
                btnSalvarAlt.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione um item!!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSalvarAlt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.BebidaController.alterarDados(int.Parse(DtGrid.SelectedValue.ToString()), txtEditarItem.Text);
                DtGrid.ItemsSource = Controller.BebidaController.retornaSabores();
                txtNovoSabor.IsEnabled = true;
                btnCadSabor.IsEnabled = true;
                btnExcluir.IsEnabled = true;
                txtEditarItem.Visibility = Visibility.Hidden;
                btnSalvarAlt.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione um item!!");

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
