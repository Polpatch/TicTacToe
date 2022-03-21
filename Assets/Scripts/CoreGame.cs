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

    private ModalInfoController modalInfo;

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

        modalInfo = ModalInfoController.Instance;
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

    public string getCurrentSymbol(){
        return this.symbolTurns[currentPlayer];
    }

    public void cellIsPressed(Vector2Int cellPosition){
        Vector2Int[] moveResult = this.gridTable.insertNewPosition(cellPosition, this.getCurrentSymbol());

        if(moveResult == null){
            this.currentPlayer = (currentPlayer + 1)%symbolTurns.Count;
            modalInfo.CloseModal();
        }
        else{
            ManageWin(moveResult);
        }
    }

    private PlayerContext GetCurrentPlayer(){
        return this.mapPlayerSymbol[this.getCurrentSymbol()];
    }

    private void ManageWin(Vector2Int[] moveResult){
        string message = "";
        if(moveResult.Length == 3){
            PlayerContext currentPl = GetCurrentPlayer();
            currentPl.addPoint();

            Debug.Log(GetCurrentPlayer().getName() + "is the winner with symbol " + this.getCurrentSymbol());
            message = currentPl.getName() + " wins this match!";
        }
        else{
            message = "The match is a draw!";
        }

        modalInfo.OpenModal(message, RestartGame);

    }

    private void RestartGame(){
        ManageClick.resetEvent.Invoke();
        this.gridTable = new GameTable();
    }
}
