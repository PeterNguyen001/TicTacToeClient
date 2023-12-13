using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatWheelButton : MonoBehaviour
{
    // Start is called before the first frame update
   public void SendChatMessage()
    {
        TextMeshProUGUI textComponent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        NetworkClientProcessing.SendChatMessage(textComponent.text);
    }
}
