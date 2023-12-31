using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {

        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');

        ProcessMesagge(csv);

    }

    private static void ProcessMesagge(string[] csv)
    {

        int signifier;
        int.TryParse(csv[0], out signifier);

        switch (signifier)
        {
            case ServerToClientSignifiers.changeUI:
                ScreenID screenID = (ScreenID)int.Parse(csv[1]);
                stateChanger.ActivateScreen(screenID); break;

            case ServerToClientSignifiers.startGame:
                ticTacToeGame.SetSymbols(csv[1]); break;

            case ServerToClientSignifiers.playing:
                ticTacToeGame.FindButton(int.Parse(csv[1]), csv[2]); break;

            case ServerToClientSignifiers.ChatMessage:
                chatBox.AddMessage(csv[1]); break;
        }
    }

    static public void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        if(IsValidMessage(msg))
        { networkClient.SendMessageToServer(msg, pipeline); }
        else
        { Debug.Log("Invalid Character!"); }
    }

    static public void SendChatMessage(string msg)
    {
        SendMessageToServer($"{ClientToServerSignifiers.ChatMessage},{msg}", TransportPipeline.ReliableAndInOrder);
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
    static UIStateMachine stateChanger;
    static TicTacToeGame ticTacToeGame;
    static ChatBox chatBox;
    static public void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    static public NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    static public void SetStateChanger(UIStateMachine StateChanger)
    {
        stateChanger = StateChanger;
    }
    static public void SetTicTacToeGame(TicTacToeGame Game)
    {
        ticTacToeGame = Game;
    }
    static public void SetChatBox(ChatBox ChatBox)
    {
        chatBox = ChatBox;
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
    public const int RegisterUser = 2;
    public const int LogInUser = 3;
    public const int FindGameRoom = 4;
    public const int updateHeartbeat = 5;
    public const int Playing = 8;
    public const int GoBack = 10;
    public const int ChatMessage = 11;
}

static public class ServerToClientSignifiers
{
    public const int changeUI = 1;
    public const int startGame = 7;
    public const int playing = 8;
    public const int Spectate = 9;
    public const int ChatMessage = 11;
}

#endregion

