using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoMaquina.Model
{
    public class Consumable : IProduct
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantitu { get; set;}

        public int Inventory {  get; set; }

        

        public Consumable(string name, int price, int quantitu, int inventory)
        {
            Name = name;
            Price = price;
                Quantitu = quantitu;
            Inventory = inventory;
            
        }
        public string DisplayProduct()
        {
            return $"Nombre: {Name} - Price: {Price} ({Quantitu})";
        }
    }
}
