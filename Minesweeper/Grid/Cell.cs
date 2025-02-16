namespace Minesweeper;

public interface ICell  
{  
    bool IsBomb { get; }  
    int AdjacentBombs { get; }  
    string Name { get; }
    bool Revealed { get; set;}
    bool Flagged { get; set;}
    bool RevealedBombFirst { get; set; }
}
class BombCell : ICell  {  
    public bool IsBomb => true;  
    public int AdjacentBombs => -1; // Can be ignored 
    public string Name => "Bomb";
    public bool Revealed { get; set; } = false;
    public bool Flagged { get; set; } = false;
    public bool RevealedBombFirst { get; set; } = false;
 }  


class NumberCell : ICell  {  
    public bool IsBomb => false;
    public int AdjacentBombs { get; private set; } = 0;
    public string Name => "Number"; 
    public void SetBombCount(int count)
    {
        AdjacentBombs = count;
    }
    public bool Revealed { get; set; } = false;
    public bool Flagged { get; set; } = false;
    public bool RevealedBombFirst { get; set; } = false; //ignore
}

