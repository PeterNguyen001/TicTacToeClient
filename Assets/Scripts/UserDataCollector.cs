using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserDataCollector : MonoBehaviour
{
    private TMP_InputField usernameInputField;
    private TMP_InputField passwordInputField;
    private TMP_InputField gameRoomNameInputField;

    private string GetGameRoomName()
    {
        return gameRoomNameInputField.text;
    }

    public void SendGameRoomName()
    {
        gameRoomNameInputField = GameObject.Find("Room Name Input Field").GetComponent<TMP_InputField>();
        string gameRoomName = GetGameRoomName();
        NetworkClientProcessing.ChangeGameRoomName(gameRoomName);
        NetworkClientProcessing.SendMessageToServer($"{ClientToServerSignifiers.FindGameRoom},{gameRoomName}", TransportPipeline.ReliableAndInOrder);
    }

    public void SendBackToBrowserRequest()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.GoBack, TransportPipeline.ReliableAndInOrder);
    }

    private string GetUsernameAndPassword()
    {
        usernameInputField = GameObject.Find("Username Input Field").GetComponent<TMP_InputField>();
        passwordInputField = GameObject.Find("Password Input Field").GetComponent<TMP_InputField>();
        return $"{usernameInputField.text},{passwordInputField.text}";
    }

    public void RegisterUser()
    {
        NetworkClientProcessing.SendMessageToServer($"{ClientToServerSignifiers.RegisterUser},{GetUsernameAndPassword()}", TransportPipeline.ReliableAndInOrder);
    }

    public void LoginUser()
    {
        NetworkClientProcessing.SendMessageToServer($"{ClientToServerSignifiers.LogInUser},{GetUsernameAndPassword()}", TransportPipeline.ReliableAndInOrder);
    }
}
