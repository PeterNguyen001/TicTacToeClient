using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Device;

public class UIStateMachine : MonoBehaviour
{

    private List<GameObject> screenObjs = new List<GameObject>();

    private const int welcomeScreenID = 0; 
    private const int registerScreenID = 1;
    private const int loginScreenID = 2;
    private const int gameRoomBrowserScreenID = 3;
    private const int gameWaitingRoomScreenID = 4;
    private const int gamegRoomScreenID = 5;

    private GameObject welcomeObj;
    private GameObject registerGameObj;
    private GameObject loginGameObj;
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
        welcomeObj = transform.GetChild(welcomeScreenID).gameObject;
        registerGameObj = transform.GetChild(registerScreenID).gameObject;
        loginGameObj = transform.GetChild(loginScreenID).gameObject;
        gameRoomBrowserObj = transform.GetChild(gameRoomBrowserScreenID).gameObject;
        gameWaitingRoomGameObj = transform.GetChild(gameWaitingRoomScreenID).gameObject;
        gameRoomGameObj = transform.GetChild(gamegRoomScreenID).gameObject;
        foreach (Transform child in transform)
        {
            screenObjs.Add(child.gameObject);
        }
        ActivateSpecificScreen(welcomeScreenID);
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
    public const int RegisterScreen = 1;
    public const int LoginScreen = 2;
    public const int GameRoomBrowserScreen = 3;
    public const int GameWaitingRoomScreen = 4;
    public const int GameRoomScreen = 5;
}

