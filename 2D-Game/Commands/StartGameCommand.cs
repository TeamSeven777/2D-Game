namespace _2D_Game
{
    public class StartGameCommand : ICommand
    {
        private Game1 myGame;

        public StartGameCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.StartGame();
        }
    }
}
