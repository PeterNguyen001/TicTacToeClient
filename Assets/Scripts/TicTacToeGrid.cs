using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGrid : MonoBehaviour
{
    private TicTacToeGame ticTacToeLogic;
    [SerializeField]
    private int position;
    public GridType gridType { get; set; }

    void Start()
    {
        ticTacToeLogic = FindObjectOfType<TicTacToeGame>();
    }

    public void OnPress()
    {
        ticTacToeLogic.PlayerMove(GetComponent<Button>());
    }

    public void SetGridType(GridType gridType)
    {
        this.gridType = gridType;
    }

    public void SetPosition(int pos)
    {
        position = pos;
    }

    public int GetPosition()
    {
        return position;
    }
}

public enum GridType
{
    Player1 = 0,
    Player2 = 1
}
