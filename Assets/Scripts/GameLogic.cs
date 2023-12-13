using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    NetworkClientProcessing.SendMessageToServer("3,Hello server's world, sincerely your network client", TransportPipeline.ReliableAndInOrder);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            //NetworkClientProcessing.ChangeScreen(5);
        }
    }

}
