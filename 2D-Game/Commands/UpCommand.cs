using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class UpCommand : ICommand
    {
        private Game1 myGame;

        public UpCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.player.MoveUp();
        }
    }
}
