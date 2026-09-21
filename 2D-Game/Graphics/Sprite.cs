using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Fourest_Seven.lib.Graphics
{
    public class Sprite : ISprite
    {
        //Properties of the Sprite
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth {  get; set; }
        public SpriteEffects Effects { get; set; }

        //The sprite sheet and its location on the sheet
        public Texture2D Content { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public Sprite() 
        { }

        //Constructor must at least have an image to set
        public Sprite(Texture2D image)
        {
            this.Content = image;
            Scale = Vector2.One;
            Position = Vector2.Zero;
            SourceRectangle = image.Bounds;
            //origin = new Vector2(image.Width / 2, image.Height / 2);
            Origin = Vector2.Zero;
            Color = Color.White;
            Rotation = 0.0f;
            LayerDepth = 0.0f;
            Effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position)
        {
            this.Content = image;
            Scale = Vector2.One;
            this.Position = position;
            SourceRectangle = image.Bounds;
            Origin = new Vector2(image.Width / 2, image.Height / 2);
            Color = Color.White;
            Rotation = 0;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale)
        {
            this.Content = image;
            this.Scale = scale;
            this.Position = position;
            SourceRectangle = image.Bounds;
            Origin = new Vector2(image.Width / 2, image.Height / 2);
            Color = Color.White;
            Rotation = 0;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color)
        {
            this.Content = image;
            this.Scale = scale;
            this.Position = position;
            SourceRectangle = image.Bounds;
            Origin = new Vector2(image.Width / 2, image.Height / 2);
            this.Color = Color.White;
            Rotation = 0;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }
        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color, float rotation)
        {
            this.Content = image;
            this.Scale = scale;
            this.Position = position;
            SourceRectangle = image.Bounds;
            Origin = new Vector2(image.Width / 2, image.Height / 2);
            this.Color = Color.White;
            this.Rotation = rotation;
            LayerDepth = 0;
            Effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color, float rotation, int layerDepth)
        {
            this.Content = image;
            this.Scale = scale;
            this.Position = position;
            SourceRectangle = image.Bounds;
            Origin = new Vector2(image.Width / 2, image.Height / 2);
            this.Color = Color.White;
            this.Rotation = rotation;
            this.LayerDepth = layerDepth;
            Effects = SpriteEffects.None;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            //Debug.WriteLine("Drawing {0} at {1}, {2}", this.content.Name, position.X, position.Y);
            spriteBatch.Draw(this.Content, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
            //spriteBatch.Draw(this.content, position, new Rectangle(0,0,25,25), color, rotation, origin, scale, effects, layerDepth);
            //spriteBatch.Draw(this.content, position, color);
            //spriteBatch.Draw(this.content, position, sourceRectangle, color);
        }


    }
}
