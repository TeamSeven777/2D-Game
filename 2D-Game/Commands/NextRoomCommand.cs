using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class NextRoomCommand : ICommand
    {
        private Game1 myGame;

        public NextRoomCommand(Game1 game)
        {
            myGame = game;

        }

        public void Execute()
        {
            //TODO implement for sprint 3

        }

    }
}
