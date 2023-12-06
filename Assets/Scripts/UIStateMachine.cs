using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStateMachine : MonoBehaviour
{
    private Dictionary<ScreenID, GameObject> screenObjects = new Dictionary<ScreenID, GameObject>();
    private GameObject roomNameObj;
    private ScreenID currentScreen;

    void Start()
    {
        InitializeScreenObjects();
        roomNameObj = GameObject.Find("Room Name");
        NetworkClientProcessing.SetStateChanger(this);
        ActivateScreen(ScreenID.WelcomeScreen);
    }

    private void InitializeScreenObjects()
    {
        screenObjects[ScreenID.WelcomeScreen] = transform.GetChild((int)ScreenID.WelcomeScreen).gameObject;
        screenObjects[ScreenID.GameRoomBrowserScreen] = transform.GetChild((int)ScreenID.GameRoomBrowserScreen).gameObject;
        screenObjects[ScreenID.GameWaitingRoomScreen] = transform.GetChild((int)ScreenID.GameWaitingRoomScreen).gameObject;
        screenObjects[ScreenID.GameRoomScreen] = transform.GetChild((int)ScreenID.GameRoomScreen).gameObject;
    }

    public void ActivateScreen(ScreenID screenID)
    {
        foreach (var screenObj in screenObjects.Values)
        {
            screenObj.SetActive(false);
        }
        screenObjects[screenID].SetActive(true);
        roomNameObj.SetActive(screenID == ScreenID.GameWaitingRoomScreen || screenID == ScreenID.GameRoomScreen);
        currentScreen = screenID;
    }

    public void SetRoomName(string name)
    {
        roomNameObj.SetActive(true);
        roomNameObj.GetComponentInChildren<TextMeshProUGUI>().text = name;
    }

    public ScreenID GetCurrentScreen()
    {
        return currentScreen;
    }
}

public enum ScreenID
{
    WelcomeScreen = 0,
    GameRoomBrowserScreen = 1,
    GameWaitingRoomScreen = 2,
    GameRoomScreen = 3
}
