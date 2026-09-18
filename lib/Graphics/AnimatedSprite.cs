using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Fourest_Seven.lib.Graphics;

namespace The_Fourest_Seven.lib.Graphics
{
    public class AnimatedSprite : Sprite
    {

        //This is assuming that the frames get inserted to the
        //list in the proper order
        public Animation animation { get; set; }
        //public Texture2D currentFrame {get; set; }
        public TimeSpan timeSinceLastFrame { get; set; }
        bool paused = false;

        public AnimatedSprite(Animation animation, Texture2D spriteSheet)
        {
            this.animation = animation;
            sourceRectangle = animation.frames.Peek();
            content = spriteSheet;
            //position = Vector2.Zero;
            color = Color.White;
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
                timeSinceLastFrame += gameTime.ElapsedGameTime;

               //Debug.WriteLine("{0}", gameTime.ElapsedGameTime);

                if (timeSinceLastFrame >= animation.frameRate)
                {
                    Debug.WriteLine("Sprite was updated to next frame");
                    Debug.WriteLine("{0}", gameTime.ElapsedGameTime);

                    timeSinceLastFrame -= animation.frameRate;
                    this.sourceRectangle = animation.frames.Dequeue();
                    animation.frames.Enqueue(this.sourceRectangle);
                }
            }
        }
    }
}
