using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program

{
    static string address = "127.0.0.1"; // поточний адрес
    static int port = 8080; // порт для отримання вхідних запитів
    private static void Main(string[] args)
    {
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);

        //Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient listener = new UdpClient(ipPoint);
        try
        {
            //listenSocket.Bind(ipPoint);
            Console.WriteLine("Server started! Waiting for connection .... ");

            while (true)
            {
                int bytes = 0;
                /*byte[] data = new byte[1024];

                bytes = listenSocket.ReceiveFrom(data, ref remotePoint);*/
                byte[] data = listener.Receive(ref remotePoint);
                string msg = Encoding.Unicode.GetString(data);
                Console.WriteLine($"{DateTime.Now.ToShortDateString()} :: {msg}");


                string message = "Message was send";
                data = Encoding.Unicode.GetBytes(message);
                listener.Send(data,data.Length, remotePoint);
                //listenSocket.SendTo(data, remotePoint);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        listener.Close();
    }
}