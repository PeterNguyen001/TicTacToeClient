using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UserDataCollector : MonoBehaviour
{
    private const int Username = 0;
    private const int Password = 1;
    private const int GameRoom = 0;

    private const string goBack = "b";
    private string username;
    
     [SerializeField]
    private UserType userType;

        // Start is called before the first frame update


    public void SendUsernameAndPasswordToServer()
    {
        NetworkClientProcessing.SendMessageToServer( ProcessUserType() + ProcessUsername() + ProcessPassword(),TransportPipeline.ReliableAndInOrder);
    }

    public string ProcessUserType()
    {
        return ((int)userType).ToString() + ",";
    }
    public string ProcessUsername()
    {
        username = transform.GetChild(Username).gameObject.GetComponent<TMP_InputField>().text + ",";
        return username ;
    }
    public string ProcessPassword()
    {
        return transform.GetChild(Password).gameObject.GetComponent<TMP_InputField>().text + ",";
    }
    public string ProcessGameRoomName()
    {
        NetworkClientProcessing.ChangeGameRoomName(transform.GetChild(GameRoom).gameObject.GetComponent<TMP_InputField>().text);
        return transform.GetChild(GameRoom).gameObject.GetComponent<TMP_InputField>().text + ",";
    }
    public void SendGameRoomName()
    {
        NetworkClientProcessing.SendMessageToServer(ProcessUserType() + username + ProcessGameRoomName(), TransportPipeline.ReliableAndInOrder);
    }
    public void SendBackToBrownsertRequest()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.goBack, TransportPipeline.ReliableAndInOrder);
    }
}
