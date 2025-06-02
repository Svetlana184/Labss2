using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
using Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
Random random = new Random();
try
{
    tcpListener.Bind(ipPoint);
    tcpListener.Listen();    
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {

        using var tcpClient = await tcpListener.AcceptAsync();


        byte[] data = Encoding.UTF8.GetBytes(random.Next(10, 100).ToString());

        await tcpClient.SendAsync(data);
        Console.WriteLine($"Клиенту {tcpClient.RemoteEndPoint} отправлены данные");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}