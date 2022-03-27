using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable{

    private const int X_SIZE = 3;
    private const int Y_SIZE = 3;
    private char[,] grid;

    public GameTable(){
        this.InitMatrix();
    }

    private void InitMatrix(){
        this.grid = new char[X_SIZE, Y_SIZE];
        for(int i = 0; i <  this.grid.GetLength(0); i++){
            for(int j = 0; j <  this.grid.GetLength(1); j++){
                this.grid[i, j] = ' ';
            }
        }
    }

    public List<Vector2Int> InsertNewPosition(Vector2Int pos, string playerSymbol){
        if(playerSymbol.Length >= 1)
            return InsertNewPosition(pos, (char)playerSymbol[0]);
        return null;
    }

    public List<Vector2Int> InsertNewPosition(Vector2Int pos, char playerSymbol){
        this.grid[pos.y, pos.x] = playerSymbol;

        return this.CheckVictory(pos, playerSymbol);
    }

    private List<Vector2Int> CheckVictory(Vector2Int pos, char symbol){

        //select direction
        foreach (Vector2Int dir in GetDirection())
        {
            //insert new position to victorylist
            List<Vector2Int> checkedPosList = new List<Vector2Int>{pos};
            Vector2Int currentDir = dir;

            Vector2Int currentPos = new Vector2Int(pos.x, pos.y);

            for (int i = 0; i < Y_SIZE+1; i++)
            {
                //1 step
                currentPos = currentPos + currentDir;

                //if is a border invert direction and continue
                if(IsOutOfBound(currentPos)){
                    currentDir = -currentDir;
                    currentPos = currentPos + currentDir;

                    continue;
                }

                //if is opponent break
                if(this.grid[currentPos.y, currentPos.x] != symbol) 
                    break;

                //if is already in list continue
                if(checkedPosList.Contains(currentPos))
                    continue;

                //the cell has the samesymbol of player
                checkedPosList.Add(currentPos);

                //if list.count == 3 return win
                if(checkedPosList.Count == Y_SIZE) return checkedPosList;
            }
        }
        List<Vector2Int> emptyCellList = GetEmptyCell();

        return emptyCellList.Count == 0? emptyCellList : null;
    }

    private List<Vector2Int> GetDirection(){
        return new List<Vector2Int>{
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.one,
            new Vector2Int(1, -1)
        };
    }

    private bool IsOutOfBound(Vector2Int pos){
        return pos.x >= X_SIZE || pos.x < 0 || pos.y >= Y_SIZE || pos.y < 0;
    }

    public bool IsEmpty(int pos){
        /*
            1 -> 0,0    1+0
            2 -> 0,1    1+1
            3 -> 0,2    1+2
            4 -> 1,0    2+0
            5 -> 1,1
            6 -> 1,2
            7 -> 2,0
            8 -> 2,1
            9 -> 2,2
        */

        return this.grid[((int)(pos/3))%3, pos%3] == ' ';
    }

    public List<Vector2Int> GetEmptyCell(){
        List<Vector2Int> v = new List<Vector2Int>();
        for(int i = 0; i <  this.grid.GetLength(0); i++){
            for(int j = 0; j <  this.grid.GetLength(1); j++){
                if(this.grid[i, j] == ' ') v.Add(new Vector2Int(j, i));
            }
        }

        return v;
    }

}