using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DeathHandler
{
        private readonly Game1 MyGame;
        private Texture2D DarkOverlay;
        private Timer timer = new(1);
        private float opacity = 0;
        bool active = false;

        public DeathHandler(Game1 MyGame)
        {
            this.MyGame = MyGame;
            DarkOverlay = new Texture2D(MyGame.GraphicsDevice, 1, 1);
            DarkOverlay.SetData([Color.Black]);
        }



        public void Update(GameTime gt)
        {
            if(MyGame.player.Health <= 0)
            {
                timer.Update(gt);
                if (timer.Ringing)
                {
                    active = true;
                }
                if(opacity >= 1)
                {
                    MyGame.ResetGame();
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (this.active)
            {
                sb.Draw(DarkOverlay, sb.GraphicsDevice.Viewport.Bounds, Color.White * opacity);
                if (opacity < 1)
                {
                    opacity += 0.01f;
                }
            }
        }



}

}
