using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2D_Game
{
    public class Stopwatch
{
        /*
         * This class provides implementation of one of those timers that you wind up.
         * good for cooldowns.
         */
        public double Time = 0.0;
        private double milliseconds = 0.0;
        private double seconds = 0.0;
        private double minutes = 0.0;
        bool Active = true;

        SpriteFont HudFont;

        /// <summary>
        /// Creates a stopwatch
        /// </summary>
        /// <param name="timeCount"></param>
        public Stopwatch(SpriteFont font) {
            HudFont = font;
        }
        public void Update(GameTime gt) {
            if (Active)
            {
                milliseconds += gt.ElapsedGameTime.TotalMilliseconds;
                if (milliseconds >= 99)
                {
                    milliseconds = 0;
                }

                seconds += gt.ElapsedGameTime.TotalSeconds;
                if (seconds >= 60)
                {
                    seconds = 0;
                }

                minutes = gt.TotalGameTime.Minutes;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            string curTime = this.ToString();
            Vector2 curTimeSize = HudFont.MeasureString(curTime);
            Vector2 curTimePos = new Vector2(sb.GraphicsDevice.Viewport.Width - curTimeSize.X - 16, 8);
            sb.DrawString(HudFont, curTime, curTimePos + new Vector2(1, 1), Color.White); //shadow so it reads on any background
            sb.DrawString(HudFont, curTime, curTimePos, Color.OrangeRed);
        }
        public void Resume()
        {
            Active = true;
        }
        public void Pause()
        {
            Active = false;
        }
        public void Reset()
        {
            Time = 0;
        }

        override public string ToString()
        {
            string counter = $" Time = {minutes.ToString("00")} : {seconds.ToString("00")} : {milliseconds.ToString("00")}";

            return counter;
        }
    }
}
