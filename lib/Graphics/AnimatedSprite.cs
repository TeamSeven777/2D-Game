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
        public Animation reel { get;set; }
        //public Texture2D currentFrame {get; set; }
        public TimeSpan timeSinceLastFrame { get; set; }
        bool paused = false;

        public AnimatedSprite(Texture2D spriteSheet, Vector2 spriteRegion, Vector2 startPoint)
        {
            //I know this is bad, but im using it for now
            reel = new Animation(spriteSheet, TimeSpan.FromMilliseconds(17), spriteRegion, startPoint);
            sourceRectangle = reel.frames.Peek();
            this.content = spriteSheet;
            //default framerate 60
            
        }
        public AnimatedSprite(Texture2D spriteSheet, TimeSpan animationRate, Vector2 spriteRegion, Vector2 startPoint)
        {
            //I know this is bad, but im using it for now
            reel = new Animation(spriteSheet, animationRate, spriteRegion, startPoint);
            sourceRectangle = reel.frames.Peek();
            this.content = spriteSheet;
            //default framerate 60

        }
        public void Update(GameTime gameTime)
        {
            /*if(pauseAnimation()){
            *pause = true;}
            *else if(!pauseAnimation()){
            *pause = false;}
            */
            Debug.WriteLine("Sprite is checking if paused");
            if (!paused)
            {
                Debug.WriteLine("Sprite is about to be updated to next frame");
                timeSinceLastFrame += gameTime.ElapsedGameTime;

                if (gameTime.ElapsedGameTime <= reel.frameRate)
                {
                    Debug.WriteLine("Sprite was updated to next frame");
                    timeSinceLastFrame -= gameTime.ElapsedGameTime;
                    sourceRectangle = reel.frames.Dequeue();
                    reel.frames.Enqueue(sourceRectangle);
                }
            }
        }

    }
}
