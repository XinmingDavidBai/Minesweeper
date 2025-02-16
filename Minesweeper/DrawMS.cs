using System.Numerics;
using Raylib_cs;

namespace Minesweeper;

public static class DrawMS {
    public static void Bomb(int x, int y) {
        Raylib.DrawCircle(x + MagicNumbers.CELL_WIDTH / 2, y + MagicNumbers.CELL_HEIGHT / 2, 10, Color.Black);
    }
    public static void Empty(int x, int y) {
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_HEIGHT, Color.White);
    }
    public static void Number(string bombCount, int x, int y) {
        Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Black);
    }
    public static void Flag(int x, int y) {
        Raylib.DrawTriangle(
            new Vector2(x, y),
            new Vector2(x, y + MagicNumbers.CELL_HEIGHT),
            new Vector2(x + MagicNumbers.CELL_WIDTH, y + MagicNumbers.CELL_HEIGHT / 2), 
            Color.Red
        );
    }
    public static void Layer(int x, int y) {
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_WIDTH, Color.Gray);
    }
    public static void LoseBomb(int x, int y) {
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_WIDTH, Color.Red);
        Raylib.DrawCircle(x + MagicNumbers.CELL_WIDTH / 2, y + MagicNumbers.CELL_HEIGHT / 2, 10, Color.Black);
    }
    public static void Reset(int width) {
        Raylib.DrawText("Reset", width*40, 0, 40, Color.Black);
    }
}