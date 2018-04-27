using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networkmanagement : NetworkManager
{
    public GameObject mainui;

    public GameObject gamemanager;

    public GameObject[] players;

    private void Start()
    {
        Network.maxConnections = 4;
        gamemanager = GameObject.FindGameObjectWithTag("gamemanager");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        if (this.numPlayers >= Network.maxConnections)
            conn.Disconnect();
        
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
       
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }
   


    public void Startuphost()
    {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartHost();
        mainui.SetActive(false);
    }

    public void joingame()
    {
        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
            mainui.SetActive(false);
      

    }
}
