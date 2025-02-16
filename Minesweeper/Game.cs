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
        grid = new Grid(9,9,9);
    }
    public void Run() {
        Raylib.InitWindow(width, height, name);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            grid.Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}