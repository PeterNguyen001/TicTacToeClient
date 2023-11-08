using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UIStateMachine : MonoBehaviour
{

    private List<GameObject> screenObjs = new List<GameObject>();

    private const int welcomeScreenID = 0; 
    private const int registerScreenID = 1;
    private const int loginScreenID = 2;
    private const int gameRoomBrowserScreenID = 3;
    private const int gameRoomScreenID = 4;

    private GameObject welcomeObj;
    private GameObject registerGameObj;
    private GameObject loginGameObj;
    private GameObject gameRoomBrowserObj;
    private GameObject gameRoomGameObj;

    private int currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        NetworkClientProcessing.SetStateChanger(this);
        welcomeObj = transform.GetChild(welcomeScreenID).gameObject;
        registerGameObj = transform.GetChild(registerScreenID).gameObject;
        loginGameObj = transform.GetChild(loginScreenID).gameObject;
        gameRoomBrowserObj = transform.GetChild(gameRoomBrowserScreenID).gameObject;
        gameRoomGameObj = transform.GetChild(gameRoomScreenID).gameObject;
        foreach (Transform child in transform)
        {
            screenObjs.Add(child.gameObject);
        }
        ActivateSpecificScreen(welcomeScreenID);
    }


    public void SetCurrentScreen(int screenID)
    {
        currentScreen = screenID;
        ActivateSpecificScreen(screenID);
    }

    public int GetCurrentScreen()
    {
        return currentScreen;
    }

    public void ActivateSpecificScreen(int screenID)
    {
        foreach(GameObject screenObj in screenObjs)
        {               
            screenObj.SetActive(false);
        }
        screenObjs[screenID].SetActive(true);
    }
}
