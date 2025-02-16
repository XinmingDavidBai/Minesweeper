namespace HelloWorld;

class Program {
    public static void Main() {
        Game game = new Game(800,800,"Minesweeper v0.1");
        game.Run();
    }
}