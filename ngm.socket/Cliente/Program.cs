using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClienteSocketUtils;

namespace ClienteImz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Conexion al servidor
            int puerto;
            string servidor;
            

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Ingrese la ip del servidor: ");
                Console.ResetColor();
                servidor = Console.ReadLine().Trim();
                

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Ingrese el puerto: ");
                Console.ResetColor();
                puerto = Convert.ToInt32(Console.ReadLine().Trim());
                

                // Creamos el socket para la conexion al servidor
                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

                // Validamos la conexion
                if (clienteSocket.Conectar())
                {
                    //OK, puede conectar
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("                     Enhorabuena!! conexion exitosa...");
                    Console.ResetColor();

                    //aqui esta el protocolo de comunicacion
                    bool seguir = true;
                    string respuesta;
                    string mensaje_server;
                    do
                    {
                        respuesta = clienteSocket.Leer();
                        Console.WriteLine("Servidor: " + respuesta);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("                             Envia una respuesta: ");
                        Console.ResetColor();
                        mensaje_server = Console.ReadLine().Trim();
                        clienteSocket.Escribir(mensaje_server);
                        if (respuesta == "chao" || mensaje_server == "chao")
                        {
                            seguir = false;
                            clienteSocket.Desconectar();

                            Console.ReadKey();

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("                        Te has desconectado del servidor...");
                            Console.ResetColor();
                        }
                    } while (seguir);
                }
                else
                {
                    Console.WriteLine("              Error de conexion, el puerto {0} esta en uso", puerto);
                }
            }
        }
    }
}