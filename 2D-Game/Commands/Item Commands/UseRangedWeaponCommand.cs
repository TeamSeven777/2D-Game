using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2D_Game
{
    public class UseRangedWeaponCommand : ICommand
    {
        private Game1 myGame;
        private TimeSpan gameTime;
        private TimeSpan previousGameTime;

        public UseRangedWeaponCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            
            gameTime = myGame.CurrentGameTime.TotalGameTime;

            if (gameTime > previousGameTime)
            {
                myGame.player.UseTool(myGame.player.CurrentRangedWeapon.GetTool());
                if(WeaponInventory.Instance.GetAmmo(myGame.player.CurrentRangedWeapon.GetTool()) > 0)
                    PlayerProjectileHandler.Instance.Add(myGame.player.CurrentRangedWeapon.CreateProjectile(myGame.player.Position));
            }

            previousGameTime = gameTime + TimeSpan.FromMilliseconds(100);
        }

    }
}
