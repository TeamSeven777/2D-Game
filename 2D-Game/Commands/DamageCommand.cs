using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class DamageCommand : ICommand
    {
        private Game1 myGame;

        public DamageCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            //TODO myGame.player.Hurt(int dmg)
        }

    }
}
