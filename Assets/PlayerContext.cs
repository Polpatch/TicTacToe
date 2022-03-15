/// <summary>
/// The class stores information about a player, such as number of points and associated symbol
/// </summary>
public class PlayerContext{
    private string name;
    private string symbol;
    private int score;

    public PlayerContext(string playerName, string playerSymbol){
        this.name = playerName;
        this.symbol = playerSymbol;
        this.score = 0;
    }

    public string getName(){
        return this.name;
    }

    public string getSymbol(){
        return this.symbol;
    }

    /// <summary>
    /// cahnge player symbol in X or O
    /// </summary>
    /// <param name="value">X or O string</param>
    /// <returns>true if the symbol has changed, false otherwise</returns>
    public bool changeSymbol(string value){
        if(value == "X" || value == "O"){
            this.symbol = value;
            return true;
        }
        return false;
    }

    public int getScore(){
        return this.score;
    }

    public int addPoint(){
        return ++this.score;
    }

    public void resetScore(){
        this.score = 0;
    }


}