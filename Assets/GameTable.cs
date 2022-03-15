using UnityEngine;

class GameTable{

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

    public Vector2Int[] insertNewPosition(Vector2Int pos, string playerSymbol){
        if(playerSymbol.Length >= 1)
            return insertNewPosition(pos, (char)playerSymbol[0]);
        return null;
    }

    public Vector2Int[] insertNewPosition(Vector2Int pos, char playerSymbol){
        this.grid[pos.x, pos.y] = playerSymbol;

        return this.CheckIfWin(pos, playerSymbol);
    }

    private Vector2Int[] CheckIfWin(Vector2Int pos, char symbol){
        
        Vector2Int[] res = null;

        /// check vertical
        if(this.grid[pos.x, (Y_SIZE+pos.y-1)%Y_SIZE] == symbol &&
                this.grid[pos.x, pos.y] == symbol &&
                this.grid[pos.x, (pos.y+1)%Y_SIZE] == symbol){
            res = new Vector2Int[3];
            res[0] = new Vector2Int(pos.x, (Y_SIZE+pos.y-1)%Y_SIZE);
            res[1] = new Vector2Int(pos.x, pos.y);
            res[2] = new Vector2Int(pos.x, (pos.y+1)%Y_SIZE);

            return res;
        }

        /// check horizontal
        if(this.grid[(X_SIZE+pos.x-1)%X_SIZE, pos.y] == symbol &&
                this.grid[pos.x, pos.y] == symbol &&
                this.grid[(pos.x+1)%X_SIZE, pos.y] == symbol){
            res = new Vector2Int[3];
            res[0] = new Vector2Int((X_SIZE+pos.x-1)%X_SIZE, pos.y);
            res[1] = new Vector2Int(pos.x, pos.y);
            res[2] = new Vector2Int((pos.x+1)%X_SIZE, pos.y);

            return res;
        }

        /// check diagonals
        if(pos.y ==  pos.x && 
                this.grid[(X_SIZE+pos.x-1)%X_SIZE, (Y_SIZE+pos.y-1)%Y_SIZE] == symbol &&
                this.grid[pos.x, pos.y] == symbol &&
                this.grid[(pos.x+1)%X_SIZE, (pos.y+1)%Y_SIZE] == symbol){
            res = new Vector2Int[3];
            res[0] = new Vector2Int((X_SIZE+pos.x-1)%X_SIZE, (Y_SIZE+pos.y-1)%Y_SIZE);
            res[1] = new Vector2Int(pos.x, pos.y);
            res[2] = new Vector2Int((pos.x+1)%X_SIZE, (pos.y+1)%Y_SIZE);

            return res;
        }
        
        if(pos.y + pos.x == 2 &&
            this.grid[(pos.x+1)%X_SIZE, (Y_SIZE+pos.y-1)%Y_SIZE] == symbol &&
                this.grid[pos.x, pos.y] == symbol &&
                this.grid[(X_SIZE+pos.x-1)%X_SIZE, (pos.y+1)%Y_SIZE] == symbol){
            res = new Vector2Int[3];
            res[0] = new Vector2Int((pos.x+1)%X_SIZE, (Y_SIZE+pos.y-1)%Y_SIZE);
            res[1] = new Vector2Int(pos.x, pos.y);
            res[2] = new Vector2Int((X_SIZE+pos.x-1)%X_SIZE, (pos.y+1)%Y_SIZE);

            return res;
        }

        return null;
    }

}