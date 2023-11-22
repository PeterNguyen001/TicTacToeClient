using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGrid : MonoBehaviour
{
    private TicTacToeGame ticTacToeLogic;
    public int position {  get; private set; }
    public GridType gridType { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        ticTacToeLogic = FindObjectOfType<TicTacToeGame>();
    }

    // Update is called once per frame
    public void OnPress()
    {
        ticTacToeLogic.PlayerMove(this.GetComponent<Button>());
    }
    public void SetGridType(GridType gridType)
    {
        this.gridType = gridType;
    }
    public void SetPosition(int pos)
    { this.position = pos;}
}

public enum GridType
{
    Player = 0,
    Opponent = 1
}
