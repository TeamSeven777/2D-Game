using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Fourest_Seven.lib.Graphics
{
    public class Animation : IAnimation
    {
        public Queue<Rectangle> frames { get; set; }
        public TimeSpan frameRate { get; set; }

        /// <summary>
        /// Creates and animation from a spriteSheet, allocating each "region" as a rectangle to be used by the
        /// Animated Sprite class
        /// </summary>
        public Animation(Texture2D spriteSheet, TimeSpan rate, Vector2 spriteRegion, Vector2 startPoint)
        {
            this.frames = new Queue<Rectangle>();
            this.frameRate = rate;

            int distance = spriteSheet.Width;
            for (int i = (int)startPoint.Y; i < distance; i += (int)spriteRegion.Y)
            {
                Debug.WriteLine("We added a frame");
                frames.Enqueue(new Rectangle((int)startPoint.X, (int)startPoint.Y,(int)spriteRegion.X,(int)spriteRegion.Y));
                startPoint.Y = i;
            }


        }
    }
}
