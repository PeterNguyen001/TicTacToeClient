using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Include the TextMeshPro namespace


public class TicTacToeGame : MonoBehaviour
{

    public Button[] buttons; 
    private bool playerTurn = false; 
    private string yourSymbol;
    private string opponentSymbol;

    void Start()
    {
        NetworkClientProcessing.SetTicTacToeGame(this);
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
            gridObjects[i].SetPosition(i);
            buttons[i] = gridObjects[i].GetComponent<Button>();
        }
    }

    public void PlayerMove(Button button)
    {
        if (playerTurn && button.GetComponentInChildren<TextMeshProUGUI>().text == "")
        {
            ProcessMove(button, yourSymbol);
            playerTurn = false;
            OpponentMove(); //for ai only
            
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

    }

    void ProcessMove(Button button,string text)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = text; // Player's mark
        if (text == "X")
        {           
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            button.GetComponent<TicTacToeGrid>().SetGridType(GridType.Player1) ;
        }
        else if(text == "O")
        {       
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            button.GetComponent<TicTacToeGrid>().SetGridType(GridType.Player2);
        }
        button.interactable = false;

        string msg = (int)UserType.Playing + "," + button.GetComponent<TicTacToeGrid>().position.ToString();
        Debug.Log(msg);
        //NetworkClientProcessing.SendMessageToServer(msg, TransportPipeline.ReliableAndInOrder);
        CheckForWin();
    }

    void CheckForWin()
    {
        // Check rows
        for (int row = 0; row < 3; row++)
        {
            if (IsLineEqual(row * 3, row * 3 + 1, row * 3 + 2))
                EndGame(buttons[row * 3].GetComponentInChildren<TextMeshProUGUI>().text);
        }

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (IsLineEqual(col, col + 3, col + 6))
                EndGame(buttons[col].GetComponentInChildren<TextMeshProUGUI>().text);
        }

        // Check diagonals
        if (IsLineEqual(0, 4, 8) || IsLineEqual(2, 4, 6))
            EndGame(buttons[4].GetComponentInChildren<TextMeshProUGUI>().text);

    }

    bool IsLineEqual(int index1, int index2, int index3)
    {
        string val1 = buttons[index1].GetComponentInChildren<TextMeshProUGUI>().text;
        string val2 = buttons[index2].GetComponentInChildren<TextMeshProUGUI>().text;
        string val3 = buttons[index3].GetComponentInChildren<TextMeshProUGUI>().text;

        return val1 != "" && val1 == val2 && val2 == val3;
    }

    void EndGame(string winningPlayer)
    {

        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        Debug.Log(winningPlayer + " wins!");
    }

    public void SetPlayerTurn()
    {
        playerTurn = true;
    }

    public void SetSymbols(string symbol)
    {
        this.yourSymbol = symbol;
        if(symbol == "X")
        { 
            opponentSymbol = "O";
            playerTurn = true;
        }
        else if (symbol == "O")
        {
            opponentSymbol = "X";
            playerTurn = false;
        }
    }

}
