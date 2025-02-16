namespace Minesweeper;

public interface ICell  
{  
    bool IsBomb { get; }  
    int AdjacentBombs { get; }  
    string Name { get; }
}
class BombCell : ICell  {  
    public bool IsBomb => true;  
    public int AdjacentBombs => -1; // Can be ignored 
    public string Name => "Bomb"; 
}  


class NumberCell : ICell  {  
    public bool IsBomb => false;
    public int AdjacentBombs { get; private set; } = 0;
    public string Name => "Number"; 
    public void SetBombCount(int count)
    {
        AdjacentBombs = count;
    }

}

