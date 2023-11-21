using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Include the TextMeshPro namespace


public class TicTacToeGame : MonoBehaviour
{
    public Button[] buttons; // Assign your 3x3 grid buttons here in the inspector
    private bool playerTurn = true; // True if player's turn, false if opponent's turn

    void Start()
    {
        InitializeButtons();
        InitializeGame();
    }

    void InitializeGame()
    {
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.interactable = true;
        }
    }

    void InitializeButtons()
    {
        TicTacToeGrid[] gridObjects = FindObjectsOfType<TicTacToeGrid>();
        buttons = new Button[gridObjects.Length];

        for (int i = 0; i < gridObjects.Length; i++)
        {
            buttons[i] = gridObjects[i].GetComponent<Button>();
        }
    }

    public void PlayerMove(Button button)
    {
        if (playerTurn && button.GetComponentInChildren<TextMeshProUGUI>().text == "")
        {
            ProcessMove(button, "X");
            playerTurn = false;
            OpponentMove();
        }
    }

    void OpponentMove()
    {
        List<Button> availableButtons = new List<Button>();
        foreach (var button in buttons)
        {
            if (button.GetComponentInChildren<TextMeshProUGUI>().text == "")
            {
                availableButtons.Add(button);
            }
        }

        if (availableButtons.Count > 0)
        {
            int randomIndex = Random.Range(0, availableButtons.Count);
            ProcessMove(availableButtons[randomIndex], "O");
        }

        playerTurn = true;
        // Here you can add a method call to check for a win or a draw
    }

    void ProcessMove(Button button,string text)
    {
        if (text == "X")
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = text; // Player's mark
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
        else if(text == "O")
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = text; // Player's mark
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        }
        button.interactable = false;
    }

    // Additional methods to check for win conditions, draw, restart game, etc.
}
