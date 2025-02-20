using System.Numerics;
using Raylib_cs;

namespace Minesweeper;

public static class DrawMS {
    public static void Bomb(int x, int y) {
        Raylib.DrawCircle(x + MagicNumbers.CELL_WIDTH / 2, y + MagicNumbers.CELL_HEIGHT / 2, 10, Color.Black);
    }

    public static void Number(string bombCount, int x, int y) {
        switch (bombCount) {
            case "0": 
                Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_HEIGHT, Color.White);
                break;
            case "1":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Blue);
                break;
            case "2":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Green);
                break;
            case "3":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Red);
                break;
            case "4":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.DarkBlue);
                break;
            case "5":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Maroon);
                break;
            case "6":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Maroon);
                break; 
            case "7":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Black);
                break;
            case "8":
                Raylib.DrawText(bombCount, x + MagicNumbers.CELL_WIDTH / 3, y + MagicNumbers.CELL_HEIGHT / 3, 20, Color.Gray);
                break;   
        }
        
    }
    public static void Flag(int x, int y) {
        Raylib.DrawTriangle(
            new Vector2(x+4, y+4),
            new Vector2(x+4, y + MagicNumbers.CELL_HEIGHT-4),
            new Vector2(x + MagicNumbers.CELL_WIDTH-4, y + (MagicNumbers.CELL_HEIGHT-4) / 2), 
            Color.Red
        );
    }
    public static void Layer(int x, int y) {
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_WIDTH, Color.Gray);
        Raylib.DrawRectangle(x, y, 4, MagicNumbers.CELL_HEIGHT, Color.LightGray);
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, 4, Color.LightGray);
        Raylib.DrawRectangle(x+4, y + MagicNumbers.CELL_HEIGHT-4, MagicNumbers.CELL_WIDTH-4, 4, Color.DarkGray);
        Raylib.DrawRectangle(x+MagicNumbers.CELL_WIDTH-4, y+4, 4, MagicNumbers.CELL_HEIGHT-4, Color.DarkGray);
    }
    public static void LoseBomb(int x, int y) {
        Raylib.DrawRectangle(x, y, MagicNumbers.CELL_WIDTH, MagicNumbers.CELL_WIDTH, Color.Red);
        Raylib.DrawCircle(x + MagicNumbers.CELL_WIDTH / 2, y + MagicNumbers.CELL_HEIGHT / 2, 10, Color.Black);
    }
    public static void FalseFlag(int x, int y) {
        Raylib.DrawRectangle(x,y, MagicNumbers.CELL_WIDTH,MagicNumbers.CELL_HEIGHT,Color.White);
        Raylib.DrawCircle(x + MagicNumbers.CELL_WIDTH / 2, y + MagicNumbers.CELL_HEIGHT / 2, 10, Color.Black);
        Raylib.DrawLine(x,y,x+MagicNumbers.CELL_WIDTH,y+MagicNumbers.CELL_HEIGHT,Color.Red);
        Raylib.DrawLine(x, y+MagicNumbers.CELL_HEIGHT,x+MagicNumbers.CELL_WIDTH,y,Color.Red);
    }
    public static void Reset(int width) {
        Raylib.DrawText("Reset", width*40, 0, 40, Color.Black);
    }
    public static void Win(int height) {
        Raylib.DrawText("You Win!!!", 0, height * 40, 40, Color.Black);
    }
    public static void Time(string time) {
        Raylib.DrawText(time, Raylib.GetScreenWidth() - 100, 50, 20, Color.Black);
    }
}