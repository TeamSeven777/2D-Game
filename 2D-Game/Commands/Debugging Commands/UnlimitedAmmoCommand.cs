using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class UnlimitedAmmoCommand : ICommand
    {
        private Game1 myGame;

        public UnlimitedAmmoCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            if (WeaponInventory.Instance.UnlimitedAmmo)
            WeaponInventory.Instance.UnlimitedAmmo = true;
            else
                WeaponInventory.Instance.UnlimitedAmmo = false;

        }

    }
}
