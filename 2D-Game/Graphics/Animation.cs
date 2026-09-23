using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Animation : IAnimation
    {
        public Queue<Rectangle> Frames { get; set; }
        public TimeSpan FrameRate { get; set; }
        public enum Style {Walking, Straight, Idle };

        /// <summary>
        /// Creates and animation from a spriteSheet, allocating each "region" as a rectangle to be used by the
        /// Animated Sprite class
        /// </summary>
        public Animation(Texture2D spriteSheet, TimeSpan rate, Vector2 spriteRegion, Vector2 startPoint)
        {
            this.Frames = new Queue<Rectangle>();
            this.FrameRate = rate;

            int distance = spriteSheet.Height;
            for (int i = (int)startPoint.X; i < distance; i += (int)spriteRegion.X)
            {
                //Debug.WriteLine("We added a frame");
                Frames.Enqueue(new Rectangle((int)startPoint.X, (int)startPoint.Y, (int)spriteRegion.X, (int)spriteRegion.Y));
                startPoint.X += spriteRegion.X;
            }


        }
        public Animation(Texture2D spriteSheet, TimeSpan rate, Vector2 spriteRegion, Vector2 startPoint, int distance)
        {
            this.Frames = new Queue<Rectangle>();
            this.FrameRate = rate;

            for (int i = (int)startPoint.X; i < distance; i += (int)spriteRegion.X)
            {
                //Debug.WriteLine("We added a frame");
                Frames.Enqueue(new Rectangle((int)startPoint.X, (int)startPoint.Y, (int)spriteRegion.X, (int)spriteRegion.Y));
                startPoint.X += spriteRegion.X;
            }


        }

        public Animation(Texture2D spriteSheet, TimeSpan rate, Vector2 spriteRegion, Vector2 startPoint, int distance, Style style)
        {
            this.Frames = new Queue<Rectangle>();
            this.FrameRate = rate;
            Vector2 firstStartPoint = startPoint;
            int count = 0;
            distance += (int)startPoint.X;

            for (int i = (int)startPoint.X; i < distance; i += (int)spriteRegion.X)
            {
                if(style == Style.Walking && (count % 2 == 0))
                {
                    Frames.Enqueue(new Rectangle((int)firstStartPoint.X, (int)firstStartPoint.Y, (int)spriteRegion.X, (int)spriteRegion.Y));
                }
                //Debug.WriteLine("We added a frame");
                Frames.Enqueue(new Rectangle((int)startPoint.X, (int)startPoint.Y, (int)spriteRegion.X, (int)spriteRegion.Y));
                startPoint.X += spriteRegion.X;
                count++;
            }
            Frames.Dequeue();


        }

    }
}
