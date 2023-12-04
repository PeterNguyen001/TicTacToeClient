using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Device;

public class UIStateMachine : MonoBehaviour
{

    private List<GameObject> screenObjs = new List<GameObject>();


    private GameObject welcomeObj;
    private GameObject gameRoomBrowserObj;
    private GameObject gameWaitingRoomGameObj;
    private GameObject gameRoomGameObj;
    private int currentScreen;

    private GameObject roomNameObj;
    // Start is called before the first frame update
    void Start()
    {
        roomNameObj = GameObject.Find("Room Name");
        roomNameObj.SetActive(false);
        NetworkClientProcessing.SetStateChanger(this);
        welcomeObj = transform.GetChild(ScreenID.WelcomeScreen).gameObject;
        gameRoomBrowserObj = transform.GetChild(ScreenID.GameRoomBrowserScreen).gameObject;
        gameWaitingRoomGameObj = transform.GetChild(ScreenID.GameWaitingRoomScreen).gameObject;
        gameRoomGameObj = transform.GetChild(ScreenID.GameRoomScreen).gameObject;
        foreach (Transform child in transform)
        {
            screenObjs.Add(child.gameObject);
        }
        ActivateSpecificScreen(ScreenID.WelcomeScreen);
    }




    public void SetCurrentScreen(int screenID)
    {
        ActivateSpecificScreen(screenID);
    }



    public int GetIntCurrentScreen()
    {
        return currentScreen;
    }

    public void ActivateSpecificScreen(int screenID)
    {
        foreach(GameObject screenObj in screenObjs)
        {               
            screenObj.SetActive(false);
        }
        screenObjs[((int)screenID)].SetActive(true);
        if(screenID == ScreenID.GameWaitingRoomScreen || screenID == ScreenID.GameRoomScreen)
        {
            roomNameObj.SetActive(true);
        }
        else
        { roomNameObj.SetActive(false); }
    }
    public void SetRoomName(string name)
    { roomNameObj.GetComponentInChildren<TextMeshProUGUI>().text = name; }
}
public class ScreenID
{
    public const int WelcomeScreen = 0;
    public const int GameRoomBrowserScreen = 1;
    public const int GameWaitingRoomScreen = 2;
    public const int GameRoomScreen = 3;
}

