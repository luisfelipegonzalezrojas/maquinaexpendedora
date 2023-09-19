using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoMaquina.Model
{
   public interface IProduct
    {
        string Name { get; set; }
        int Quantitu {  get; set; }
        int Price { get; set; }

        int Inventory {  get; set; }
        string DisplayProduct();

    }
}
