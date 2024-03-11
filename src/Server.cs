using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Logs from your program will appear here!");

TcpListener server = new(IPAddress.Any, 6379);
try
{
    server.Start();
    

    while (true)
    {
        var socket = server.AcceptSocket();
        Task.Run(() => HandleRequest(socket));

        async Task HandleRequest(Socket socket)
        {
            var buffer = new byte[1024];
            while (true)
            {
                _ = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                await socket.SendAsync(Encoding.UTF8.GetBytes("+PONG\r\n"), SocketFlags.None);
            }            
        }        
    }
}
finally 
{ 
    server.Stop(); 
}



