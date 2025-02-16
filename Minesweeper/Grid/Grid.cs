namespace Minesweeper;
using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
class Grid
{

    public ICell[,] Cells { get; private set; }
    private int width;
    private int height;
    public Grid(int width, int height, int bombCount)
    {
        this.width = width;
        this.height = height;
        Cells = new ICell[width, height];
        PlaceBombs(width, height, bombCount);
        InitializeNumberCells(width, height);
        UpdateNumbers();
    }
    private static readonly (int, int)[] Directions = {
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),         (1,  0),
        (-1,  1), (0,  1), (1,  1)
    };
    private bool IsInBounds(int x, int y) =>
        x >= 0 && x < Cells.GetLength(0) &&
        y >= 0 && y < Cells.GetLength(1);
    private void PlaceBombs(int width, int height, int bombCount)
    {
        Random random = new Random();
        HashSet<(int, int)> bombPositions = new HashSet<(int, int)>();

        while (bombPositions.Count < bombCount)
        {
            int x = random.Next(width);
            int y = random.Next(height);

            if (!bombPositions.Contains((x, y)))
            {
                bombPositions.Add((x, y));
                Cells[x, y] = new BombCell();
            }
        }
    }

    private void InitializeNumberCells(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Cells[x, y] == null) // If it's not a bomb
                {
                    Cells[x, y] = new NumberCell();
                }
            }
        }
    }

    private void UpdateNumbers()
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                if (Cells[x, y] is BombCell)
                    continue;

                int bombCount = 0;
                foreach (var (dx, dy) in Directions)
                {
                    int nx = x + dx, ny = y + dy;
                    if (IsInBounds(nx, ny) && Cells[nx, ny] is BombCell)
                        bombCount++;
                }

                ((NumberCell)Cells[x, y]).SetBombCount(bombCount);
            }
        }
        
    }

    public void Draw() {
        int cellWidth = MagicNumbers.CELL_WIDTH;  // Width of each cell
        int cellHeight = MagicNumbers.CELL_HEIGHT; // Height of each cell

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Calculate position with padding
                int xPos = i * cellWidth;
                int yPos = j * cellHeight;

                // Drawing cell content (bomb or number)
                switch (Cells[i, j].Name)
                {
                    case "Bomb":
                        Raylib.DrawCircle(xPos + cellWidth / 2, yPos + cellHeight / 2, 10, Color.Black);
                        break;
                    case "Number":
                        string bombCount = Cells[i, j].AdjacentBombs.ToString();
                        // To center the text in the cell
                        Raylib.DrawText(bombCount, xPos + cellWidth / 3, yPos + cellHeight / 3, 20, Color.Black);
                        break;
                    default:
                        throw new Exception("Unknown cell type");
                }
                // Drawing layer on top of it
                if (!Cells[i, j].Revealed) { 
                    Raylib.DrawRectangle(xPos, yPos, cellWidth, cellHeight, Color.Gray);
                }
                if (Cells[i,j].Flagged) {
                    // Draw flag
                    Raylib.DrawTriangle(
                        new Vector2(xPos, yPos),
                        new Vector2(xPos, yPos + cellHeight),
                        new Vector2(xPos + cellWidth, yPos + cellHeight / 2), 
                        Color.Red
                    );
                }
            }
        }

    }
}
