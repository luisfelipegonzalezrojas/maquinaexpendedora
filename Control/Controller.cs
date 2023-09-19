using proyectoMaquina.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoMaquina.Control
{
    public sealed class Controller
    {


        private static Controller _instance;
        public List<IProduct> ListaProductos { get; set; }
        private Controller()
        {
            ListaProductos = new List<IProduct>();

            ListaProductos.Add(new Consumable("Cocacola", 3000, 15, 40));
            ListaProductos.Add(new Consumable("PapitasMayo", 2500, 7, 20));
            ListaProductos.Add(new Consumable("Snacks", 2200, 4, 10));

        }
        public static Controller GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }

        public string DisplayProductList()
        {
            string value = string.Empty;
            foreach (IProduct product in ListaProductos)
            {
                value += product.DisplayProduct() + '\n';

            }
            return value;
        }
        public bool ProductExists(string product_name)
        {
            bool product_exists = false;
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name && product.Quantitu > 0)
                {
                    product_exists = true;

                }
            }
            return product_exists;
        }
        public bool ProductHasInventory(string product_name)
        {
            return ListaProductos.Any(product => product.Name == product_name && product.Quantitu > 0);
        }

        public void AddProduct(string name, int price, int initialQuantity, int inventory)
        {
            ListaProductos.Add(new Consumable(name, price, initialQuantity, inventory));

        }
        public void RestockProduct(string name, int quantitu)
        {
            var product = ListaProductos.FirstOrDefault(p => p.Name == name);
            if (product != null)
            {
                product.Inventory += quantitu;
            }

        }
    }
}

