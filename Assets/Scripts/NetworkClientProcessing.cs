using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{
    static int userType = 0;
     static int loginScreenID = 2;
     static int gameRoomBrowserScreenID = 3;
    private const int gameWaitingRoomScreenID = 4;
    private const int gamegRoomScreenID = 5;
    [SerializeField]
    static GameObject UI;

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {

        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');

        CheckPermission(csv);

    }

    private static void CheckPermission(string[] csv)
    {
        if (csv[userType] == ((int)UserType.LoggedInUser).ToString() && ((int)stateChanger.GetIntCurrentScreen()) == loginScreenID)
        {
            stateChanger.SetCurrentScreenFromInt(gameRoomBrowserScreenID);
        }
        else if(csv[userType] == ((int)UserType.GamerUser).ToString() && ((int)stateChanger.GetIntCurrentScreen()) == gameWaitingRoomScreenID)
        {
            stateChanger.SetCurrentScreenFromInt(gamegRoomScreenID);
        }
    }

    static public void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        if(IsValidMessage(msg))
        { networkClient.SendMessageToServer(msg, pipeline); }
        else
        { Debug.Log("Invalid Character!"); }
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

    static public bool IsValidMessage(string message)
    {
        char[] invalidChars = System.IO.Path.GetInvalidPathChars();

        foreach (char c in message)
        {
            if (Array.IndexOf(invalidChars, c) >= 0)
            {
                return false;
            }
        }

        // Additional checks for other invalid characters can be added here if needed

        return true;
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

