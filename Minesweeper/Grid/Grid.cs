namespace Minesweeper;
using System;
using System.Collections.Generic;
class Grid {
    public ICell[,] Cells { get; private set; }
    private int width;
    private int height;
    private int bombCount;
    private HashSet<(int,int)> bombPositions;
    private bool isFirstClick = true;
    public int FlagCount { get; private set; }
    public Grid(int width, int height, int bombCount)
    {
        this.width = width;
        this.height = height;
        this.bombCount = bombCount;
        FlagCount = bombCount;
        Cells = new ICell[width, height];
        bombPositions = new HashSet<(int, int)>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cells[i, j] = new NumberCell(); // Default empty cell
            }
        }

    }
    public void Reset() {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cells[i, j] = new NumberCell(); // Default empty cell
            }
        }
        isFirstClick = true;
        FlagCount = bombCount;

    }
    private static readonly (int, int)[] Directions = {
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),         (1,  0),
        (-1,  1), (0,  1), (1,  1)
    };

    public bool CheckWinNumber() {
        foreach (var cell in Cells) {
            if (!cell.IsBomb && !cell.Revealed) {
                return false;
            }
        }
        return true;
    }
    public void PutFlag(int x, int y) {
        Cells[x,y].Flagged = true;
        FlagCount--;
    }
    public void TakeFlag(int x, int y) {
        Cells[x,y].Flagged = false;
        FlagCount++;
    }
    private bool IsInBounds(int x, int y) =>
        x >= 0 && x < Cells.GetLength(0) &&
        y >= 0 && y < Cells.GetLength(1);
    public void HandleFirstClick(int startX, int startY) {
        if (!isFirstClick) return;

        isFirstClick = false; 
        PlaceBombs(width, height, bombCount, startX, startY);
        InitializeNumberCells(width, height);
        UpdateNumbers();
    }

    private void PlaceBombs(int width, int height, int bombCount, int startX, int startY) {
        List<(int, int)> allPositions = new List<(int, int)>();

        // Add all positions except the ones near the first click
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // Skip the first clicked cell and its adjacent cells
                if (Math.Abs(x - startX) <= 1 && Math.Abs(y - startY) <= 1) {
                    continue;
                }
                allPositions.Add((x, y));
            }
        }

        // Shuffle and pick the first `bombCount` positions
        Random random = new Random();
        allPositions = allPositions.OrderBy(_ => random.Next()).ToList();

        for (int i = 0; i < bombCount && i < allPositions.Count; i++) {
            (int x, int y) = allPositions[i];
            bombPositions.Add((x, y));
            Cells[x, y] = new BombCell();
        }
    }


    private void InitializeNumberCells(int width, int height) {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (Cells[x, y] == null) { // If it's not a bomb 
                    Cells[x, y] = new NumberCell();
                }
            }
        }
    }

    private void UpdateNumbers() {
        for (int x = 0; x < Cells.GetLength(0); x++) {
            for (int y = 0; y < Cells.GetLength(1); y++) {
                if (Cells[x, y] is BombCell)
                    continue;

                int bombCount = 0;
                foreach (var (dx, dy) in Directions) {
                    int nx = x + dx, ny = y + dy;
                    if (IsInBounds(nx, ny) && Cells[nx, ny] is BombCell)
                        bombCount++;
                }

                ((NumberCell)Cells[x, y]).SetBombCount(bombCount);
            }
        }
        
    }
    
    public void TouchedEmpty(int x, int y) {
        if (Cells[x, y].AdjacentBombs != 0) {
            throw new Exception("error empty is not empty");
        }

        Cells[x, y].Revealed = true; // Mark this cell as revealed

        foreach (var (dx, dy) in Directions) {
            int nx = x + dx, ny = y + dy;
            
            if (IsInBounds(nx, ny)) {
                if (!Cells[nx, ny].Revealed && !Cells[nx, ny].Flagged) {
                    Cells[nx, ny].Revealed = true;
                    if (Cells[nx, ny].AdjacentBombs == 0) {
                        TouchedEmpty(nx, ny);
                    }
                }
            }
        }
    }
    public bool CheckFlagCountSurrounding(int x, int y) {
        int c = 0;
        foreach (var (dx, dy) in Directions) {
            int nx = x + dx, ny = y + dy;
            
            if (IsInBounds(nx, ny)) {
                if (Cells[nx, ny].Flagged) c++;
            }
        }
        return Cells[x,y].AdjacentBombs == c;
    }

    public bool RemoveSurroundingCells(int x, int y) {
        foreach (var (dx, dy) in Directions) {
            int nx = x + dx, ny = y + dy;
            
            if (IsInBounds(nx, ny)) {
                if (!Cells[nx, ny].Flagged && !Cells[nx,ny].Revealed) Cells[nx,ny].Revealed = true;
                if (Cells[nx,ny].AdjacentBombs == 0) TouchedEmpty(nx,ny);
                if (Cells[nx, ny].IsBomb && !Cells[nx, ny].Flagged) {
                    Lose(nx,ny);
                    return true;
                }
            }
        }
        return false;
    }

    public bool BothMouseClick(int x, int y) {
        if (CheckFlagCountSurrounding(x,y)) return RemoveSurroundingCells(x,y);
        return false;
    }
    public bool CheckWin() {
        foreach (var pos in bombPositions) {
            (int x, int y) = pos;
            if (!Cells[x,y].Flagged) {
                return false;
            }
        }
        return true;
    }
    public void Lose(int posX, int posY) {
        Cells[posX, posY].RevealedBombFirst = true;
        foreach (var cell in Cells) {
            if (!cell.Flagged && cell.IsBomb) cell.Revealed = true;
            if (cell.Flagged && !cell.IsBomb) cell.FalseFlagged = true;
        }
    }
    public void Draw() {
        int cellWidth = MagicNumbers.CELL_WIDTH;  // Width of each cell
        int cellHeight = MagicNumbers.CELL_HEIGHT; // Height of each cell

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // Calculate position with padding
                int xPos = i * cellWidth;
                int yPos = j * cellHeight;

                // Drawing cell content (bomb or number)
                switch (Cells[i, j].Name) {
                    case "Bomb":
                        DrawMS.Bomb(xPos, yPos);
                        break;
                    case "Number":
                        string bombCount = Cells[i, j].AdjacentBombs.ToString();
                        DrawMS.Number(bombCount, xPos, yPos);
                        break;
                    default:
                        throw new Exception("Unknown cell type");
                }
                // Drawing layer on top of it
                if (!Cells[i, j].Revealed) { 
                    DrawMS.Layer(xPos, yPos);
                }
                if (Cells[i,j].Flagged) {
                    // Draw flag
                    DrawMS.Flag(xPos, yPos);
                }
                if (Cells[i,j].RevealedBombFirst) {
                    DrawMS.LoseBomb(xPos, yPos);
                }
                if (Cells[i,j].FalseFlagged) {
                    DrawMS.FalseFlag(xPos, yPos);
                }
            }
        }

    }
}
