using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class StatusDisplay
    {
        private readonly Game1 MyGame;

        private readonly DefaultPlayer Player;

        private SpriteFont HudFont;

        private string HealthString;
        private string LogString;
        private const int LogsNeeded = 7;

        public StatusDisplay(Game1 game, SpriteFont font)
        {
            MyGame = game;
            Player = MyGame.player;
            HudFont = font;
            HealthString = $" ( {Player.Health} / {Player.MaxHealth} ) ";
            LogString  = $" ( {MyGame.StateManager.LogsInFire} / {LogsNeeded} ) ";

        }
        public void Update()
        {
            HealthString = $" ( {Player.Health} / {Player.MaxHealth} ) ";
            LogString = $" ( {MyGame.StateManager.LogsInFire} / {LogsNeeded} ) ";
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 healthCounterSize = HudFont.MeasureString(HealthString);
            Vector2 healthCounterPos = new Vector2(sb.GraphicsDevice.Viewport.Width - healthCounterSize.X - 16, 32);
            sb.DrawString(HudFont, HealthString, healthCounterPos + new Vector2(1, 1), Color.White); //shadow so it reads on any background
            sb.DrawString(HudFont, HealthString, healthCounterPos, Color.OrangeRed);
            Vector2 logCounterSize = HudFont.MeasureString(LogString);
            Vector2 logCounterPos = new Vector2(sb.GraphicsDevice.Viewport.Width - logCounterSize.X - 16, sb.GraphicsDevice.Viewport.Height - logCounterSize.Y - 16);
            sb.DrawString(HudFont, LogString, logCounterPos + new Vector2(2, 2), Color.Black); //shadow so it reads on any background
            sb.DrawString(HudFont, LogString, logCounterPos, Color.White);
        }

}
}
