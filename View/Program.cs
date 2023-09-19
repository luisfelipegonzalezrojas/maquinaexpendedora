using proyectoMaquina.Control;
using proyectoMaquina.Model;
using System.Security.Cryptography.X509Certificates;

namespace proyectoMaquina.View // Note: actual namespace depends on the project name.
{
    internal class view
    {
        static void Main(string[] args)
        {
            //Aqui empieza nuestro programa

            Controller controller = Controller.GetInstance();


            string texto_bienvenida = "bienvenido a la maquina expendedora ";
            Console.WriteLine(texto_bienvenida);
            string input_cliente = "";

            while (true)
            {

                do
                {
                    Console.WriteLine(" Escoja el tipo de cliente: [C] o [P]");
                    input_cliente = Console.ReadLine();
                } while (input_cliente != "C" && input_cliente != "P");

                Console.WriteLine("La lista de productos es: ");
                Console.WriteLine(controller.DisplayProductList());

                if (input_cliente == "C")
                {
                    {
                        Console.WriteLine("Escoja un producto de la lista...");
                        bool valid_product = false;
                        IProduct selectedProduct = null;

                        do
                        {
                            string input_producto = Console.ReadLine();
                            valid_product = controller.ProductExists(input_producto) && controller.ProductHasInventory(input_producto);

                            if (valid_product)
                            {
                                selectedProduct = controller.ListaProductos.FirstOrDefault(p => p.Name == input_producto);
                            }
                            else
                            {
                                Console.WriteLine("Escoja un producto válido");
                            }
                        } while (!valid_product);

                        int totalPrice = selectedProduct.Price;
                        int totalCoins = 0;

                        Console.WriteLine($"El precio del producto es: {totalPrice}");

                        while (totalCoins < totalPrice)
                        {
                            try
                            {
                                Console.WriteLine("Ingrese una moneda (500, 200, 100, 50):");
                                int coinValue = int.Parse(Console.ReadLine());

                                if (coinValue != 500 && coinValue != 200 && coinValue != 100 && coinValue != 50)
                                {
                                    throw new ArgumentException("Moneda no válida.");
                                }

                                totalCoins += coinValue;
                                Console.WriteLine($"Total ingresado: {totalCoins}");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Ingrese un valor numérico válido.");
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Ingrese una moneda válida (500, 200, 100, 50).");
                            }
                        }

                        if (totalCoins > totalPrice)
                        {
                            // Calcula el cambio y muestra el cambio al cliente.
                            int change = totalCoins - totalPrice;
                            Console.WriteLine($"Compra exitosa. Aquí tiene su producto y su cambio: {change}");
                            // Actualiza la cantidad de productos disponibles.
                            selectedProduct.Quantitu--;
                        }
                        else if (totalCoins == totalPrice)
                        {
                            // El cliente ha pagado exactamente el precio del producto.
                            Console.WriteLine("Compra exitosa. Aquí tiene su producto.");
                            // Actualiza la cantidad de productos disponibles.
                            selectedProduct.Quantitu--;
                        }
                        else
                        {
                            // El dinero ingresado no es suficiente. La compra ha sido cancelada.
                            Console.WriteLine("El dinero ingresado no es suficiente. La compra ha sido cancelada.");
                        }
                    }
                }






                // comparar precio producto vs Suma_billetes hacer el calculo

                else if (input_cliente == "P")
                {
                    Console.WriteLine("Bienvenido, proveedor.");
                    Console.WriteLine("¿Desea ingresar un nuevo producto (N) o rellenar inventario de un producto existente (R)?");
                    string input_proveedor = Console.ReadLine();

                    if (input_proveedor == "N")
                    {
                        Console.WriteLine("Ingrese el nombre del nuevo producto:");
                        string newProductName = Console.ReadLine();

                        Console.WriteLine("Ingrese el precio del nuevo producto:");
                        int newProductPrice = int.Parse(Console.ReadLine());

                        Console.WriteLine("Ingrese la cantidad inicial del nuevo producto:");
                        int newProductInitialQuantity = int.Parse(Console.ReadLine());

                        Console.WriteLine("Ingrese la cantidad en inventario del nuevo producto:");
                        int newProductInventory = int.Parse(Console.ReadLine());

                        controller.AddProduct(newProductName, newProductPrice, newProductInitialQuantity, newProductInventory);
                        Console.WriteLine($"Producto '{newProductName}' agregado con éxito.");
                    }
                    else if (input_proveedor == "R")
                    {
                        Console.WriteLine("Ingrese el nombre del producto para rellenar inventario:");
                        string productNameToRestock = Console.ReadLine();

                        Console.WriteLine("Ingrese la cantidad para rellenar inventario:");
                        int restockQuantity = int.Parse(Console.ReadLine());

                        controller.RestockProduct(productNameToRestock, restockQuantity);
                        Console.WriteLine($"Inventario de '{productNameToRestock}' rellenado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida para proveedores.");
                    }
                }

            }
        }
    }
}