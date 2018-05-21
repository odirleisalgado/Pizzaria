using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Item
    {
        public int ItemID { get; set; }

        public string Tamanho { get; set; }

        public string Sabor { get; set; }

        public string Adicional { get; set; }

        public double Valor { get; set; }

        public int PedidoID { get; set; }
        
    }
}
