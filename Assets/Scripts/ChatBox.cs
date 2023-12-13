using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public List<TextMeshProUGUI> chatTexts; 
    private FixedSizeQueue chatMessages = new FixedSizeQueue();

    private void Start()
    {
        NetworkClientProcessing.SetChatBox(this);
    }
    public void AddMessage(string message)
    {
        chatMessages.Enqueue(message);
        UpdateChatDisplay();
    }

    private void UpdateChatDisplay()
    {
        var messages = chatMessages.GetQueue();
        int i = 0;

        foreach (var message in messages)
        {
            if (i < chatTexts.Count)
            {
                chatTexts[i].text = message;
                i++;
            }
        }
    }
}


public class FixedSizeQueue
{
    private Queue<string> queue = new Queue<string>();
    private const int MaxSize = 4;

    public void Enqueue(string item)
    {
        if (queue.Count >= MaxSize)
        {
            queue.Dequeue(); // Remove the oldest item
        }
        queue.Enqueue(item);
    }

    public IEnumerable<string> GetQueue()
    {
        return queue;
    }
}