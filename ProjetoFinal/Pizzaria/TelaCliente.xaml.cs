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
    /// Interaction logic for TelaCliente.xaml
    /// </summary>
    public partial class TelaCliente : Window
    {
        List<Cliente> ClienteLista = new List<Cliente>();
        private Cliente novo = new Cliente();

        public TelaCliente()
        {
            InitializeComponent();

            ClienteLista = Controller.ClienteController.retornaClientes();
            DtGrid.ItemsSource = null;
            DtGrid.ItemsSource = ClienteLista;
           
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       // private void btnExcluir_Click(object sender, RoutedEventArgs e)
       // {

           // bool resp;       
             //  try
                //{
                   //resp = Controller.ClienteController.ExcluirCliente(int.Parse(DtGrid.SelectedValue.ToString()));

                   // if (resp==true)
                  // {
                       // MessageBox.Show("Item Excluído com Sucesso!!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                                           
                   // }
                   // else
                  //  {
                       // Controller.PedidoController.excluirPedidoPorCliente(int.Parse(DtGrid.SelectedValue.ToString()));
                      //  MessageBox.Show("Item Excluído com Sucesso!!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                       // DtGrid.ItemsSource = null;
                      //  DtGrid.ItemsSource = Controller.ClienteController.retornaClientes();
                 //}
              // }
              //  catch (Exception)
              // {
                 //   MessageBox.Show("Por Favor, Selecione um Item !!!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                   // MessageBox.Show(DtGrid.SelectedValue.ToString());
              // }
           
       // }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                novo = Controller.ClienteController.retornaClientePorId(int.Parse(DtGrid.SelectedValue.ToString()));
                txtNome.Text = novo.Nome.ToString();              
                txtTelefone.Text = novo.Telefone.ToString();
                txtEnd.Text = novo.Endereco;
                txtNumero.Text = novo.Numero.ToString();
                txtBairro.Text = novo.Bairro;

                ViewBoxEdit.Visibility = Visibility.Visible;
                //btnExcluir.Visibility = Visibility.Hidden;
               
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
                if (txtNome.Text.Equals("") || txtBairro.Text.Equals("") || txtEnd.Text.Equals("") || txtTelefone.Text.Equals(value: "") || txtNumero.Text.Equals(value: ""))
                {
                    MessageBox.Show("Preencha Todos Os Campos!!!");
                }
                else
                {
                    //recebe os dados de todos os campos
                    novo.Nome = txtNome.Text.ToString();
                    novo.Telefone = int.Parse(txtTelefone.Text);
                    novo.Endereco = txtEnd.Text.ToString();

                    novo.Numero = int.Parse(txtNumero.Text);
                    novo.Bairro = txtBairro.Text.ToString();

                    // envia para controller as alteracos 
                    Controller.ClienteController.alterarDados(novo);


                    MessageBox.Show("Edição Realizada Com Sucesso!!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    DtGrid.ItemsSource = Controller.ClienteController.retornaClientes();
                    ViewBoxEdit.Visibility = Visibility.Hidden;
                    //btnExcluir.Visibility = Visibility.Visible;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Preencha Os Campos Corretamente", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ViewBoxEdit.Visibility = Visibility.Hidden;
           // btnExcluir.Visibility = Visibility.Visible;
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

