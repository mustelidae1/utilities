using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.SocketIO;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ServerConnect : MonoBehaviour
{
    // Singleton 
    public static ServerConnect s;

    public string socketURI;

    public SocketOptions options;
    public SocketManager socketManager;

    private void Awake()
    {
        s = this;
        CreateSocketRef();
    }

    void Start()
    {
        // Socket messages that we are listening for 
        socketManager.Socket.On("connect", OnConnect);
        socketManager.Socket.On("hello", OnHello);

        socketManager.Socket.Emit("hi");
    }

    //////////////////////////// Outgoing Socket Messages ////////////////////////////////////////

    public void sendInfo(string json)
    {
        socketManager.Socket.Emit("Info", json);
    }

    ///////////////////// Handlers for recieved socket messages //////////////////////////////////

    void OnConnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Connected to Socket.IO server");
    }
    void OnHello(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Got nice hello message from server");
    }

    /////////////////////////////// Socket.io connection utilities /////////////////////////////////
    void OnApplicationQuit()
    {
        LeaveRoomFromServer();
        DisconnectMySocket();
    }

    public void CreateSocketRef()
    {
        TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(1000);

        options = new SocketOptions();
        options.ReconnectionAttempts = 3;
        options.AutoConnect = true;
        options.ReconnectionDelay = miliSecForReconnect;

        socketManager = new SocketManager(new Uri(socketURI), options);
    }

    public void DisconnectMySocket()
    {
        socketManager.GetSocket().Disconnect();
    }

    public void LeaveRoomFromServer()
    {
        socketManager.GetSocket().Emit("leave", OnSendEmitDataToServerCallBack);
    }

    private void OnSendEmitDataToServerCallBack(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Send Packet Data : " + packet.ToString());
    }

    public void SetNamespaceForSocket()
    {
        //namespaceForCurrentPlayer = socketNamespace;
        //mySocket = socketManagerRef.GetSocket(“/ Room - 1);
    }

}
