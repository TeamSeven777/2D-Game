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
    public class AnimatedSprite : Sprite
    {

        //This is assuming that the frames get inserted to the
        //list in the proper order
        public Animation Animation { get; set; }
        //public Texture2D currentFrame {get; set; }
        public TimeSpan TimeSinceLastFrame { get; set; }
        public bool paused = false;

        public enum State {Walk, Run, Other}
        public enum Direction {Left, Right, Up, Down}

        public AnimatedSprite(Animation animation, Texture2D spriteSheet)
        {
            this.Animation = animation;
            SourceRectangle = animation.Frames.Peek();
            Content = spriteSheet;
            //position = Vector2.Zero;
            Color = Color.White;
            //default framerate 60
            //origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            //rotation = 0;
            //layerDepth = 0;
            //effects = SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            /*if(pauseAnimation()){
            *pause = true;}
            *else if(!pauseAnimation()){
            *pause = false;}
            */
            //Debug.WriteLine("Sprite is checking if paused");
            if (!paused)
            {
                //Debug.WriteLine("Sprite is about to be updated to next frame");
                TimeSinceLastFrame += gameTime.ElapsedGameTime;

               //Debug.WriteLine("{0}", gameTime.ElapsedGameTime);

                if (TimeSinceLastFrame >= Animation.FrameRate)
                {
                    //Debug.WriteLine("Sprite was updated to next frame");
                    //Debug.WriteLine("{0}", gameTime.ElapsedGameTime);

                    TimeSinceLastFrame -= Animation.FrameRate;
                    this.SourceRectangle = Animation.Frames.Dequeue();
                    Animation.Frames.Enqueue(this.SourceRectangle);
                }
            }
        }
    }
}
