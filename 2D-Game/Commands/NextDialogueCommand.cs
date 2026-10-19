using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class NextDialogueCommand : ICommand
    {
        private Game1 myGame;

        private TimeSpan gameTime;
        private TimeSpan previousGameTime;
        public NextDialogueCommand(Game1 game)
        {
            myGame = game;
            gameTime = myGame.CurrentGameTime.TotalGameTime;
            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

        public void Execute()
        {
            gameTime = myGame.CurrentGameTime.TotalGameTime;

            if (gameTime > previousGameTime) myGame.HandleDialogue();

            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

    }
}