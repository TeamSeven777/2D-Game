
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
    public class AnimatedSprite : ISprite
    {
        //Properties of the Sprite
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth { get; set; }
        public SpriteEffects Effects { get; set; }

        //Sprite Reference and Cutout
        public Texture2D Content { get; set; }
        public Rectangle SourceRectangle { get; set; }

        //Animation Information
        public Animation Animation { get; set; }
        public TimeSpan TimeSinceLastFrame { get; set; }
        public bool Paused { get; set; }

        public enum State {Walk, Run, Other}
        public enum Direction {Left, Right, Up, Down}

        /// <summary>
        /// Uses a Texture2D and animation information to generate a sprite that animates
        /// </summary>
        public AnimatedSprite(Animation animation, Texture2D spriteSheet)
        {
            this.Animation = animation;
            SourceRectangle = animation.Frames.Peek();
            Content = spriteSheet;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Color = Color.White;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            Rotation = 0;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }

        /// <summary>
        /// Uses a Texture2D and animation information to generate a sprite that animates
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="effect"></param>
        public AnimatedSprite(Animation animation, Texture2D spriteSheet, SpriteEffects effect)
        {
            this.Animation = animation;
            SourceRectangle = animation.Frames.Peek();
            Content = spriteSheet;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Color = Color.White;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            Rotation = 0;
            LayerDepth = 0;
            Effects = effect;
        }

        public void Update(GameTime gameTime)
        {
         
            if (!Paused)
            {
                //Time since the last frame was updated
                TimeSinceLastFrame += gameTime.ElapsedGameTime;

                //If the time that has passed is = or greater than the framerate
                //Its time to update the animation
                if (TimeSinceLastFrame >= Animation.FrameRate)
                { 
                    //Subtract the rate at which the animation plays
                    //From the time checking variable
                    TimeSinceLastFrame -= Animation.FrameRate;

                    //Remove the reference to the last frame of animation and set
                    //the current sprite to the new frame reference
                    this.SourceRectangle = Animation.Frames.Dequeue();
                    Animation.Frames.Enqueue(this.SourceRectangle);
                    Animation.FrameIndex++;

                    //The helper variable FrameIndex is used to pause progress on a loop and return
                    //If needed
                    if(Animation.FrameIndex >= Animation.Frames.Count && Animation.Loops)
                    {
                        Animation.FrameIndex = 0;
                    }else if (Animation.FrameIndex >= Animation.Frames.Count && !Animation.Loops)
                    {
                        Paused = true;
                    }
                }
            }
        }
        /// <summary>
        /// Uses SpriteBatch to draw AnimatedSprite to screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            spriteBatch.Draw(this.Content, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);

        }
    }
}
