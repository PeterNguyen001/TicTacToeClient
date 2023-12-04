using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UserDataCollector : MonoBehaviour
{
    private GameObject  Username;
    private GameObject Password;
    private GameObject GameRoomName;


    // Start is called before the first frame update
    private void Start()
    {
        Username = GameObject.Find("Username Input Field");
        Password = GameObject.Find ("Password Input Field");
        GameRoomName = GameObject.Find("Room Name");
    }

    
    public string ProcessGameRoomName()
    {
        NetworkClientProcessing.ChangeGameRoomName(GameRoomName.GetComponent<TMP_InputField>().text);
        return GameRoomName.GetComponent<TMP_InputField>().text + ",";
    }
    public void SendGameRoomName()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.FindGameRoom + ProcessGameRoomName(), TransportPipeline.ReliableAndInOrder);
    }
    public void SendBackToBrownsertRequest()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.GoBack, TransportPipeline.ReliableAndInOrder);
    }

    public string GetUsernameAndPassword()
    { 
       return  $"{Username.GetComponent<TMP_InputField>().text},{Password.GetComponent<TMP_InputField>().text}";
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
