using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UserDataCollector : MonoBehaviour
{
    private const int Username = 0;
    private const int Password = 1;
    private const int GameRoom = 0;


    
    private GameObject clientObj;
        private NetworkClient clientNW;
        public UserType userType;

        // Start is called before the first frame update
    void Start()
    {
        clientObj = GameObject.Find("Client");
        clientNW = GameObject.Find("Client").GetComponent<NetworkClient>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendUserDataToServer()
    {
        //clean up
        clientNW.SendMessageToServer( ProcessUserType() + ProcessUsername() + ProcessPassword(),TransportPipeline.ReliableAndInOrder);
    }

    public string ProcessUserType()
    {
        return ((int)userType).ToString() + ",";
    }
    public string ProcessUsername()
    {
        return transform.GetChild(Username).gameObject.GetComponent<TMP_InputField>().text + ",";
    }
    public string ProcessPassword()
    {
        return transform.GetChild(Password).gameObject.GetComponent<TMP_InputField>().text + ",";
    }
    public string ProcessGameRoomName()
    {
        return transform.GetChild(GameRoom).gameObject.GetComponent<TMP_InputField>().text + ",";
    }
    public void SendGameRoomName()
    {
        clientNW.SendMessageToServer(ProcessUserType() + ProcessGameRoomName(), TransportPipeline.ReliableAndInOrder);
    }
}
