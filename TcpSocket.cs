using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TcpSocket
{
    private string ipAddress;
    private int port;
    private Socket socket, clientSocket;
    private byte[] buffer = new byte[1024];

    private int backlog = 10;

    public delegate void OnMessageReceived(string message);
    public event OnMessageReceived MessageReceived;

    public TcpSocket(string ipAddress, int port)
    {
        this.ipAddress = ipAddress;
        this.port = port;
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        #region added for server emulation
        socket.Bind(new IPEndPoint(IPAddress.Any, port));
        Debug.Log("Bound socket to port " + port);
        socket.Listen(backlog);
        Debug.Log("Started listening...");
        Accept();
        #endregion
    }

    #region MethodsForServerEmulation
    private void AcceptedCallback(IAsyncResult result)
    {
        Console.WriteLine("Accept callback called... ");
        clientSocket = socket.EndAccept(result);
        if (socket.Connected)
            Console.WriteLine("A client has connected... ");
        clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, clientSocket);

        Accept();
    }
    public void Accept()
    {
        socket.BeginAccept(AcceptedCallback, null);
        Debug.Log("Beginning accept... ");
    }
    #endregion

    public IAsyncResult Connect()
    {
        return socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectCallback, null);
    }
    
    private void ConnectCallback(IAsyncResult result)
    {
        if (socket.Connected)
        {
            Debug.Log("Connected to server!");
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, null);
        }
    }

    private void ReceivedCallback(IAsyncResult result)
    {
        Debug.Log("Entered Receive callback...");
        int bufferLength = socket.EndReceive(result);
        string message = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
        Debug.Log(message);
        if (MessageReceived != null) { MessageReceived(message); }

        // Handle packet
        Array.Clear(buffer, 0, buffer.Length);
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, null);
    }

    
}
