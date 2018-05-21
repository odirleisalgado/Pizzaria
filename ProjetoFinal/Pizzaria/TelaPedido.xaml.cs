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
    /// Interaction logic for TelaPedido.xaml
    /// </summary>
    public partial class TelaPedido : Window
    {
        List<Pizza> ListaSabores = new List<Pizza>();
        public static List<Item> itemList = new List<Item>();
        
        static double  subTotal = 0.00;
        


        public TelaPedido()
        {
            InitializeComponent();
            rbP.IsChecked = true;// inicia o radio buttom de tamanho "Pequeno" como checked

            ListaSabores = Controller.PizzaController.retornaSabores(); // recebe a lista de sabores cadastrados
            txtTelefone.Focus();
            foreach (var x in ListaSabores)  // realiza a inserção dos sabores nas combobox
            {
                cmbSabores.Items.Add(x.SaborPizza);
                cmbSabores2.Items.Add(x.SaborPizza);
                cmbSabores3.Items.Add(x.SaborPizza);
            }           
        }


        // ************************************************ BOTÕES E EVENTOS ******************************************************************************


        // DISPARA UM EVENTO CASO O RADIO BUTTOM TAMANHO "P" ESTEJA CHECKED, ALTERNANDO A VISIBILIDADE DOS DEMAIS CAMPOS DE SABOR
        private void rbP_Checked(object sender, RoutedEventArgs e)
        {
            cmbSabores2.Visibility = Visibility.Hidden;
            cmbSabores3.Visibility = Visibility.Hidden;
            lblSabor2.Visibility = Visibility.Hidden;
            lblSabor3.Visibility = Visibility.Hidden;
        }

        // DISPARA UM EVENTO CASO O RADIO BUTTOM TAMANHO "M" ESTEJA CHECKED, ALTERNANDO A VISIBILIDADE DOS DEMAIS CAMPOS DE SABOR
        private void rbM_Checked(object sender, RoutedEventArgs e)
        {
            cmbSabores2.Visibility = Visibility.Visible;
            cmbSabores3.Visibility = Visibility.Hidden;
            lblSabor2.Visibility = Visibility.Visible;
            lblSabor3.Visibility = Visibility.Hidden;
        }

        // DISPARA UM EVENTO CASO O RADIO BUTTOM TAMANHO "G" ESTEJA CHECKED, ALTERNANDO A VISIBILIDADE DOS DEMAIS CAMPOS DE SABOR
        private void rbG_Checked(object sender, RoutedEventArgs e)
        {
            cmbSabores2.Visibility = Visibility.Visible;
            cmbSabores3.Visibility = Visibility.Visible;
            lblSabor2.Visibility = Visibility.Visible;
            lblSabor3.Visibility = Visibility.Visible;
        }


        // BOTÃO FINALIZAR PEDIDO
        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pedido novoPedido = new Pedido();
                
                novoPedido.clienteId = int.Parse(blockId.Text);
                
                novoPedido.DataPedido = DateTime.Now.ToString();

                novoPedido.Total = double.Parse(txtTotal.Text);
                
                if (itemList.Count==0)
                {
                    MessageBox.Show("Favor Inserir Um Item!!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {                    
                        novoPedido.ListaItens = itemList;                      
                        Controller.PedidoController.SalvarPedido(novoPedido);
                        TelaPedidoFinalizado novaTela = new TelaPedidoFinalizado();
                        novaTela.ShowDialog();
                        limparCampos();                 
                }                   
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Preencha Os Campos Corretamente", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        // BOTÃO ADICIONAR ITENS
        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item novoItem = new Item();

                novoItem = tamanhoSelecionado(novoItem);
                novoItem.Sabor = saborSelecionado();
                novoItem = adicionalSelecionado(novoItem);
               

                ListView1.Items.Add(novoItem);              
                itemList.Add(novoItem);

                cmbSabores.SelectedIndex = -1;
                cmbSabores2.SelectedIndex = -1;
                cmbSabores3.SelectedIndex = -1;
                cbAzeitona.IsChecked = false;
                cbCheddar.IsChecked = false;
                cbBacon.IsChecked = false;
                cbBorda.IsChecked = false;

                txtTotal.Text = (subTotal += novoItem.Valor).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione Um Sabor !!!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }         
        }

        // BOTÃO REMOVER ITEM
        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                int idSelecionado = int.Parse(ListView1.SelectedValue.ToString()); //Pega item selecionado e transforma o seu ID em int
                bool resp = removeItem(idSelecionado); //passa o ID do item selecionado para remoção
                if (resp.Equals(true))
                {
                    MessageBox.Show("Item Removido Com Sucesso!!!", "Sucesso", MessageBoxButton.OK);
                    txtTotal.Text = subTotal.ToString();
                    int selectedIndex = ListView1.SelectedIndex;
                    ListView1.Items.RemoveAt(selectedIndex);
                }
                else
                {
                    MessageBox.Show("Por Favor, Selecione Um Item ", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Selecione Um Item ", "Erro", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        // BOTÃO DE FECHAR A JANELA
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // BOTÃO DE PESQUISA ATRAVÉS DO TELEFONE DO CLIENTE
        private void btnPesquisaTel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // realiza a pesquisa do cliente passando o telefone como parâmetro e recebe o objeto 
                Cliente Recep = Controller.ClienteController.PesquisaCliPorTel(int.Parse(txtTelefone.Text));

                if (Recep == null)
                {
                    MessageBox.Show("Cliente Não Cadastrado", "Informação", MessageBoxButton.OK);
                    Controller.PedidoController.GuardaTelefone(int.Parse(txtTelefone.Text));
                    CadastroCliente tela = new CadastroCliente();
                    tela.ShowDialog();
                }
                else
                {
                    // Caso o cliente já esteja cadastrado, as informações aparecerão na tela
                    blockId.Text = Recep.ClienteID.ToString();
                    blockNome.Text = Recep.Nome;
                    blockFone.Text = Recep.Telefone.ToString();
                    blockEnd.Text = Recep.Endereco;
                    blockNr.Text = Recep.Numero.ToString();
                    blockBairro.Text = Recep.Bairro;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor, Insira O Telefone", "Informação", MessageBoxButton.OK);
            }

        }

        
       

        //**************************************  MÉTODOS ADICIONAIS ****************************************************

        //RETORNA O TAMAHO DA PIZZA SELECIONADA
        public Item tamanhoSelecionado(Item itemRecebido)
        {

            if (rbP.IsChecked == true)
            {
                itemRecebido.Tamanho = "Pequena";
                itemRecebido.Valor = 25.00;
                return itemRecebido;

            }
            else
            {
                if (rbM.IsChecked == true)
                {
                    itemRecebido.Tamanho = "Média";
                    itemRecebido.Valor = 35.00;
                    return itemRecebido;

                }
                else
                {
                    itemRecebido.Tamanho = "Grande";
                    itemRecebido.Valor = 45.00;
                    return itemRecebido;

                }
            }

        }

        //RETORNA O SABOR OU SABORES QUE FORAM SELECIONADOS
        public string saborSelecionado()
        {
            string sabores;

            if (rbP.IsChecked == true)
            {
                sabores = cmbSabores.SelectedItem.ToString();
                return sabores;
            }
            else
            {
                if (rbM.IsChecked == true)
                {
                    sabores = (cmbSabores.SelectedItem.ToString() + "-" + cmbSabores2.SelectedItem.ToString());
                    return sabores;
                }
                else
                {
                    sabores = (cmbSabores.SelectedItem.ToString() + "-" + cmbSabores2.SelectedItem.ToString() + "-" + cmbSabores3.SelectedItem.ToString());
                    return sabores;
                }
            }
        }

        //RETORNA UMA STRING COM OS ADICIONAIS SELECIONADOS RECEBENDO O OBJETO ITEM COMO PARÂMETRO
        public Item adicionalSelecionado(Item itemRecebido)
        {
            if (cbAzeitona.IsChecked == true)
            {
                itemRecebido.Adicional = "+Azeitona ";
                itemRecebido.Valor +=  5.30;

            }
            if (cbBorda.IsChecked == true)
            {
                itemRecebido.Adicional += "+Borda Recheada ";
                itemRecebido.Valor += 5.75;
            }
            if (cbCheddar.IsChecked == true)
            {
                itemRecebido.Adicional += "+Cheddar ";
                itemRecebido.Valor += 5.50;
            }
            if (cbBacon.IsChecked == true)
            {
                itemRecebido.Adicional += "+Bacon ";
                itemRecebido.Valor += 5.20;
            }

            return itemRecebido;


        }

        //REMOVE ITEM DA LISTA ATUAL
        public static bool removeItem(int id)
        {
            foreach (var item in itemList)
            {
                if (id == item.ItemID)
                {
                    itemList.Remove(item);
                    subTotal = (subTotal - item.Valor);                    
                    return true;
                }
            }
            return false;
        }

       //LIMPA OS CAMPOS DA TELA DE PEDIDO
        public void limparCampos()
        {
            subTotal = 0.00;
            cmbSabores.SelectedIndex = -1;
            cmbSabores2.SelectedIndex = -1;
            cmbSabores3.SelectedIndex = -1;
            cbAzeitona.IsChecked = false;
            cbCheddar.IsChecked = false;
            cbBacon.IsChecked = false;
            cbBorda.IsChecked = false;
            ListView1.Items.Clear();
            itemList = new List<Item>();
            blockId.Text = null;
            blockNome.Text = null;
            blockFone.Text = null;
            blockEnd.Text = null;
            blockNr.Text = null;
            blockBairro.Text = null;
            txtTelefone.Text = null;
            rbP.IsChecked = true;
            txtTotal.Text = null;
        }

        //EVENTO NO TEXTBOX DE PESQUISAR TELEFONE QUE RECONHECE A TECLA ENTER, DISPARA O EVENTO DO BOTÃO "PESQUISAR TELEFONE AO PRESSIONAR 'ENTER' "
        private void txtTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                btnPesquisaTel_Click(this, new RoutedEventArgs());
            }
        }

        // EVENTO QUE FECHA A JANELA AO PRESSIONAR A TECLA "ESC"
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                btnFechar_Click(this, new RoutedEventArgs());
            }
        }
    }

}


