using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{
    static int userType = 0;
     static int loginScreenID = 2;
     static int gameRoomBrowserScreenID = 3;
    [SerializeField]
    static GameObject UI;

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {

        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');

        if (csv[userType] == ((int)UserType.LoggedInUser).ToString() && stateChanger.GetCurrentScreen() == loginScreenID)
        {
            stateChanger.SetCurrentScreen(gameRoomBrowserScreenID);
        }
        // if (signifier == ServerToClientSignifiers.asd)
        // {

        // }
        // else if (signifier == ServerToClientSignifiers.asd)
        // {

        // }

        //gameLogic.DoSomething();

    }

    static public void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        networkClient.SendMessageToServer(msg, pipeline);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkClient networkClient;
    static GameLogic gameLogic;
     static UIStateMachine stateChanger;
    static public void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    static public NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }
    static public void SetStateChanger(UIStateMachine StateChanger)
    {
        stateChanger = StateChanger;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int asd = 1;
}

static public class ServerToClientSignifiers
{
    public const int asd = 1;
}

#endregion

