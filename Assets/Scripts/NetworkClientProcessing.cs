using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{
    public const string changeUI = "1";
    public const string startGame = "7";
    public const string playing = "8";

    const int commandSign = 0;
    const int toChangeUISign = 1;
    const int symbolSign = 1;
    const int roomNameSign = 2;

    const int newGridPositionSign = 1;
    const int newGridSymbolSign = 2;

    const int loginScreenID = 2;
    const int gameRoomBrowserScreenID = 3;
    const int gameWaitingRoomScreenID = 4;
    const int gamegRoomScreenID = 5;
    [SerializeField]
    static GameObject UI;

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {

        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');

        ProcessMesagge(csv);

    }

    private static void ProcessMesagge(string[] csv)
    {
        if (csv[commandSign] == changeUI)
        {
            stateChanger.SetCurrentScreenFromInt(int.Parse(csv[toChangeUISign]));
        }
        else if (csv[commandSign] == startGame)
        {
            ticTacToeGame.SetSymbols(csv[symbolSign]);
        }
        else if (csv[commandSign] == playing)
        {
            ticTacToeGame.FindButton(int.Parse(csv[newGridPositionSign]), csv[newGridSymbolSign]);
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

    static public void ChangeScreen(int screenID)
    {
        stateChanger.SetCurrentScreenFromInt(screenID);
    }


    #endregion

    #region Setup
    static NetworkClient networkClient;
    static GameLogic gameLogic;
    static UIStateMachine stateChanger;
    static TicTacToeGame ticTacToeGame;
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
    static public void SetTicTacToeGame(TicTacToeGame Game)
    {
        ticTacToeGame = Game;
    }
    static public void ChangeGameRoomName(string name)
    { stateChanger.SetRoomName(name); }
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

