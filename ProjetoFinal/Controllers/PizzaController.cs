using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
    
{
     
    public class PizzaController
    {

        public static List<Pizza> PizzaList = new List<Pizza>();

        public static void SalvarNovoSabor(Pizza novo)
        {

            PizzaList.Add(novo);

        }

    }
}
