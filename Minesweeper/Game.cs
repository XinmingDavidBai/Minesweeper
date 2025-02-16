using System.Numerics;
using Minesweeper;
using Raylib_cs;

class Game {
    private int width;
    private int height;
    private string name;
    private Grid grid;

    public Game(int width, int height, string name) {
        this.width = width;
        this.height = height;
        this.name = name;
        grid = new Grid(9,9,10);
    }
    public void Run() {
        Raylib.InitWindow(width, height, name);
        bool first = true;
        bool lost = false;
        while (!Raylib.WindowShouldClose()) {   
            Vector2 mousePosition = Raylib.GetMousePosition();
            (int x, int y) = ((int)mousePosition.X / MagicNumbers.CELL_WIDTH, (int)mousePosition.Y / MagicNumbers.CELL_HEIGHT);
            bool board = (x < 9) && (y < 9);
            bool reset = mousePosition.X >= 9 * 40 && mousePosition.X <= 9*40 + 100 && 
                         mousePosition.Y >= 0 && mousePosition.Y <= 40;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && board && !lost) {
                if (first) {
                    first = false;
                    grid.HandleFirstClick(x,y);
                }
                if (grid.Cells[x,y].IsBomb) {
                    grid.Lose(x, y);
                    lost = true;
                } else if (grid.Cells[x,y].AdjacentBombs == 0) {
                    grid.Cells[x,y].Revealed = true;
                    grid.TouchedEmpty(x,y);
                } else {
                    grid.Cells[x,y].Revealed = true;
                }
            } else if (Raylib.IsMouseButtonPressed(MouseButton.Right) && board){
                grid.Cells[x,y].Flagged = true;
            } else if (Raylib.IsMouseButtonPressed(MouseButton.Left) && reset) {
                grid.Reset();
                lost = false;
                first = true;
            }
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            DrawMS.Reset(9);
            grid.Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}