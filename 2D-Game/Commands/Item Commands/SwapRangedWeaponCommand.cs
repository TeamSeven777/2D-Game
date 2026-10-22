using _2D_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
     public class SwapRangedWeaponCommand : ICommand
{
    private readonly Game1 myGame;

    public SwapRangedWeaponCommand(Game1 game)
    {
        myGame = game;
    }

    public void Execute()
    {
        WeaponInventory.Instance.Next();
        myGame.player.CurrentRangedWeapon = WeaponInventory.Instance.GetTop();
    }
}
}
