using System.Net.Sockets;
using System.Text;

using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    await tcpClient.ConnectAsync("127.0.0.1", 8888);

    byte[] data = new byte[512];


    int bytes = await tcpClient.ReceiveAsync(data);
    // получаем отправленное время
    string speed_str = Encoding.UTF8.GetString(data, 0, bytes);

    int speed= int.Parse(speed_str);

    Console.WriteLine($"Скорость кручения мельницы: {speed}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}