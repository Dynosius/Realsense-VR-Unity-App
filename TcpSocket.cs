using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TcpSocket
{
    private string ipAddress;
    private int port;
    private Socket socket;
    private byte[] buffer = new byte[1024];

    public delegate void OnMessageReceived(string message);
    public event OnMessageReceived MessageReceived;

    public TcpSocket(string ipAddress, int port)
    {
        this.ipAddress = ipAddress;
        this.port = port;
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }


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
        MessageReceived(message);

        // Handle packet
        Array.Clear(buffer, 0, buffer.Length);
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, null);
    }

    
}
