using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class UseSlingshotCommand : ICommand
    {
        private Game1 myGame;
        private TimeSpan gameTime;
        private TimeSpan previousGameTime;

        public UseSlingshotCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            
            gameTime = myGame.CurrentGameTime.TotalGameTime;

            if (gameTime > previousGameTime)
            {
                myGame.player.UseTool(Tool.Slingshot);
                myGame.projectileHandler.Add();
            }

            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

    }
}
