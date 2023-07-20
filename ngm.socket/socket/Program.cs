using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServidorSocketUtils;

namespace imz.sockt
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;


            Console.WriteLine("Inicializando el servidor en el puerto.... {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                //OK, puede conectar
                
                Console.WriteLine("       Servidor iniciado.....");
                
                
                while (true)
                {
                    //Console.ForegroundColor = ConsoleColor.DarkYellow;
                    //Console.WriteLine("                           Esperando Cliente...");
                    //Console.ResetColor();
                    Socket socketCliente = servidor.ObtenerCliente();

                    //construir el mecanismo para escribir y leeer cliente
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    //aqui esta el protocolo de comunicacion
                    bool salir = true;
                    string respuesta;
                    string mensaje_server;
                    do
                    //<! Establecer la conversacion
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("                               Escribe algo: ");
                        Console.ResetColor();
                        mensaje_server = Console.ReadLine().Trim();
                        cliente.Escribir(mensaje_server);
                        respuesta = cliente.Leer();
                        Console.WriteLine("Cliente: " + respuesta);
                        
                        
                        if (respuesta == "chao" || mensaje_server == "chao") 
                        {
                            salir = false;
                            cliente.Desconectar();

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("                         El cliente se desconecto del servidor... ");
                            Console.ResetColor();
                        }
                    }while (salir);
                }

            }
            else
            {
                Console.WriteLine("Error, intentalo nuevamente !!");
            }
        }
    }
}