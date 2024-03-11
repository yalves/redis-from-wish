using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new(IPAddress.Any, 6379);
try
{
    server.Start();
    using var socket = await server.AcceptSocketAsync();

    var buffer = new byte[1024];
    var bytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
    Console.WriteLine(Encoding.UTF8.GetString(buffer[0..bytes]));

    await socket.SendAsync(Encoding.UTF8.GetBytes("+PONG\r\n"), SocketFlags.None);
}
finally
{
    server.Stop();
}


