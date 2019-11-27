using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WebSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11111);

            string message = "#END#";
            try
            {
               

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Соединяем сокет с удаленной точкой
                sender.Connect(ipEndPoint);
                do
                {
                    
                    Console.Write("Введите сообщение: ");
                    message = Console.ReadLine();

                    Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
                    byte[] msg = Encoding.UTF8.GetBytes(message);

                    // Отправляем данные через сокет
                    int bytesSent = sender.Send(msg);

                    // Получаем ответ от сервера
                    int bytesRec = sender.Receive(bytes);

                    Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

                 
                }
                while (message.IndexOf("#END#") == -1);

                // Освобождаем сокет
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }

            
        }


        
    }


}
