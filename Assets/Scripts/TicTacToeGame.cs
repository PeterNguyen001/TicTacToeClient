using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeGame : MonoBehaviour
{
    public Button[] buttons;
    private bool playerTurn = false;
    public string yourSymbol { get; private set; }
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
        foreach (TicTacToeGrid grid in gridObjects)
        {
            buttons[grid.GetPosition()] = grid.GetComponent<Button>();
        }
    }

    public void PlayerMove(Button button)
    {
        if (playerTurn && button.GetComponentInChildren<TextMeshProUGUI>().text == "")
        {
            ProcessMove(button, yourSymbol);
            string msg = ClientToServerSignifiers.Playing + "," + button.GetComponent<TicTacToeGrid>().GetPosition().ToString() + "," + yourSymbol;
            NetworkClientProcessing.SendMessageToServer(msg, TransportPipeline.ReliableAndInOrder);
            playerTurn = false;
        }
    }

    void ProcessMove(Button button, string text)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;
        buttonText.color = text == "X" ? Color.red : Color.green;
        button.GetComponent<TicTacToeGrid>().SetGridType(text == "X" ? GridType.Player1 : GridType.Player2);
        button.interactable = false;
        CheckForWin();
    }

    public void FindButton(int index, string symbol)
    {
        ProcessMove(buttons[index], symbol);
        if (symbol != yourSymbol) playerTurn = true;
    }

    void CheckForWin()
    {
        for (int row = 0; row < 3; row++)
        {
            if (IsLineEqual(row * 3, row * 3 + 1, row * 3 + 2))
                EndGame(buttons[row * 3].GetComponentInChildren<TextMeshProUGUI>().text);
        }

        for (int col = 0; col < 3; col++)
        {
            if (IsLineEqual(col, col + 3, col + 6))
                EndGame(buttons[col].GetComponentInChildren<TextMeshProUGUI>().text);
        }

        if (IsLineEqual(0, 4, 8) || IsLineEqual(2, 4, 6))
            EndGame(buttons[4].GetComponentInChildren<TextMeshProUGUI>().text);
    }

    bool IsLineEqual(int index1, int index2, int index3)
    {
        string val1 = buttons[index1].GetComponentInChildren<TextMeshProUGUI>().text;
        return val1 != "" && val1 == buttons[index2].GetComponentInChildren<TextMeshProUGUI>().text && val1 == buttons[index3].GetComponentInChildren<TextMeshProUGUI>().text;
    }

    void EndGame(string winningPlayer)
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        Debug.Log(winningPlayer + " wins!");
    }

    public void SetPlayerTurn() => playerTurn = true;

    public void SetSymbols(string symbol)
    {
        yourSymbol = symbol;
        opponentSymbol = symbol == "X" ? "O" : "X";
        playerTurn = symbol == "X";
    }
}
