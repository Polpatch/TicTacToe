using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    private static CoreGame instance = null;

    private SortedDictionary<string, PlayerContext> mapPlayerSymbol = new SortedDictionary<string, PlayerContext>();

    private List<string> symbolTurns = new List<string>();

    private int currentPlayer;

    private GameTable gridTable;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentPlayer = 0;
        this.symbolTurns.Add("X");
        this.symbolTurns.Add("O");
        int playerNumber = 1;
        foreach (var symbol in this.symbolTurns){
            this.mapPlayerSymbol.Add(symbol, new PlayerContext("Player "+playerNumber++, "X"));
        }

        this.gridTable = new GameTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CoreGame Instance{
        get{
            return instance;
        }
    }

    public string getCurrentPlayer(){
        return this.symbolTurns[currentPlayer];
    }

    public void cellIsPressed(Vector2Int cellPosition){
        Vector2Int[] victoryCells = this.gridTable.insertNewPosition(cellPosition, this.getCurrentPlayer());

        if(victoryCells == null)
            this.currentPlayer = (currentPlayer + 1)%symbolTurns.Count;
        else{
            Debug.Log(this.mapPlayerSymbol[this.getCurrentPlayer()].getName() + "is the winner with symbol " + this.getCurrentPlayer());
            ManageClick.resetEvent.Invoke();
            this.gridTable = new GameTable();
        }
    }
}
