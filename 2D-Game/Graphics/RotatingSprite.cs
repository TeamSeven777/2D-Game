using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class RotatingSprite : ISprite
{
        
        //Properties of the Sprite
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; } //IN RADIANS

        private float StartingDegree { get; set; }
        private float EndingDegree { get; set; }
        private float RotationSpeed { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth { get; set; }
        public SpriteEffects Effects { get; set; }

        //Sprite Reference and Cutout
        public Texture2D Content { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public bool Paused { get; set; }
        public bool Loops { get; set; }
        public bool Clockwise { get; set; }


        public enum Direction { Left, Right, Up, Down }

        /// <summary>
        /// Uses a Texture2D and rotation information to generate a sprite that rotates
        /// </summary>
        public RotatingSprite(Texture2D spriteSheet, float startingDegree, float endingDegree, float rotationSpeed, bool clockwise, bool loops, Vector2 scale, SpriteEffects effects)
        {
            SourceRectangle = new Rectangle(0,0, spriteSheet.Width, spriteSheet.Height);
            Content = spriteSheet;
            Position = Vector2.Zero;
            Scale = scale;
            Color = Color.White;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height); //pivot from the middle of the bottom
            Rotation = MathHelper.ToRadians(startingDegree);
            StartingDegree = startingDegree;
            EndingDegree = endingDegree;
            RotationSpeed = rotationSpeed;
            LayerDepth = 0;
            Effects = effects;
            Loops = loops;
            Clockwise = clockwise;
            Paused = false;
        }

        //copy constructor, resets the rotation
        public RotatingSprite(RotatingSprite rotSpr)
        {
            SourceRectangle = rotSpr.SourceRectangle;
            Content = rotSpr.Content;
            Position = rotSpr.Position;
            Scale = rotSpr.Scale;
            Color = rotSpr.Color;
            Origin = rotSpr.Origin;
            Rotation = rotSpr.StartingDegree;
            StartingDegree = rotSpr.StartingDegree;
            EndingDegree = rotSpr.EndingDegree;
            RotationSpeed = rotSpr.RotationSpeed;
            LayerDepth = rotSpr.LayerDepth;
            Effects = rotSpr.Effects;
            Loops = rotSpr.Loops;
            Clockwise = rotSpr.Clockwise;
            Paused = false;
        }

        /*
        * Convert a sprite to a rotating sprite
        */
        public RotatingSprite(Sprite spr, float startingDegree, float endingDegree, float rotationSpeed, bool clockwise, bool loops)
        {
            SourceRectangle = spr.SourceRectangle;
            Content = spr.Content;
            Position = spr.Position;
            Scale = spr.Scale;
            Color = spr.Color;
            Origin = spr.Origin;
            Rotation = MathHelper.ToRadians(startingDegree);
            StartingDegree = startingDegree;
            EndingDegree = endingDegree;
            RotationSpeed = rotationSpeed;
            LayerDepth = spr.LayerDepth;
            Effects = spr.Effects;
            Loops = loops;
            Clockwise = clockwise;
            Paused = false;
        }

        public void Update(GameTime gameTime)
            {
            //Rotation -= RotationSpeed;

            if (!Paused)
                {
                //Time since the last frame was updated
                //sprite.Rotation = (float)(spinTimer / SpinDuration * MathHelper.TwoPi);
                    float endInRadians = MathHelper.ToRadians(EndingDegree);
                    //If the time that has passed is = or greater than the framerate
                    //Its time to update the rotation
                    if (Clockwise && Rotation < endInRadians)
                        {
                        //Subtract the rate at which the animation plays
                        //From the time checking variable
                        Rotation += RotationSpeed;

                        //The helper variable FrameIndex is used to pause progress on a loop and return
                        //If needed
                        if (Rotation >= endInRadians && Loops)
                        {
                            Rotation = MathHelper.ToRadians(StartingDegree);
                        }
                        else if (Rotation >= endInRadians && !Loops)
                        {
                            Paused = true;
                        }
                    }
                    else if (!Clockwise && Rotation > endInRadians)
                    {
                        //Subtract the rate at which the animation plays
                        //From the time checking variable
                        Rotation -= RotationSpeed;

                        //The helper variable FrameIndex is used to pause progress on a loop and return
                        //If needed
                        if (Rotation <= endInRadians && Loops)
                        {
                            Rotation = MathHelper.ToRadians(StartingDegree);
                        }
                        else if (Rotation <= endInRadians && !Loops)
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

                if(!Paused)spriteBatch.Draw(this.Content, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);

            }
        }
    }

