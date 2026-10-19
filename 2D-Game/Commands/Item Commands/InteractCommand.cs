using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class InteractCommand : ICommand
    {
        private Game1 myGame;

        private TimeSpan gameTime;
        private TimeSpan previousGameTime;
        public InteractCommand(Game1 game)
        {
            myGame = game;
            gameTime = myGame.CurrentGameTime.TotalGameTime;
            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

        public void Execute()
        {
            gameTime = myGame.CurrentGameTime.TotalGameTime;

            if (gameTime > previousGameTime) myGame.player.UseTool(Tool.Interact);

            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

    }
}