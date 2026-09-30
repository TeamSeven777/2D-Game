
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
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth { get; set; }
        public SpriteEffects Effects { get; set; }
        //The sprite sheet and its location on the sheet
        public Texture2D Content { get; set; }
        public Rectangle SourceRectangle { get; set; }

        //This is assuming that the frames get inserted to the
        //list in the proper order
        public Animation Animation { get; set; }
        //public Texture2D currentFrame {get; set; }
        public TimeSpan TimeSinceLastFrame { get; set; }
        public bool Paused { get; set; }

        public enum State {Walk, Run, Other}
        public enum Direction {Left, Right, Up, Down}

        public AnimatedSprite(Animation animation, Texture2D spriteSheet)
        {
            this.Animation = animation;
            SourceRectangle = animation.Frames.Peek();
            Content = spriteSheet;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Color = Color.White;
            //default framerate 60
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            Rotation = 0;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }

        public AnimatedSprite(Animation animation, Texture2D spriteSheet, SpriteEffects effect)
        {
            this.Animation = animation;
            SourceRectangle = animation.Frames.Peek();
            Content = spriteSheet;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Color = Color.White;
            //default framerate 60
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            Rotation = 0;
            LayerDepth = 0;
            Effects = effect;
        }

        public void Update(GameTime gameTime)
        {
            /*if(pauseAnimation()){
            *pause = true;}
            *else if(!pauseAnimation()){
            *pause = false;}
            */
            //Debug.WriteLine("Sprite is checking if paused");
            if (!Paused)
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
                    Animation.FrameIndex++;
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

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            spriteBatch.Draw(this.Content, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);

        }
    }
}
