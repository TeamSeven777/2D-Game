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
        public Vector2 size { get; set; }
        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public Color color { get; set; }
        public float rotation { get; set; }
        public Vector2 scale { get; set; }
        public float layerDepth {  get; set; }
        public SpriteEffects effects { get; set; }

        //The sprite sheet and its location on the sheet
        public Texture2D content { get; set; }
        public Rectangle sourceRectangle { get; set; }

        public Sprite() 
        { }

        //Constructor must at least have an image to set
        public Sprite(Texture2D image)
        {
            this.content = image;
            scale = Vector2.One;
            position = Vector2.Zero;
            sourceRectangle = image.Bounds;
            //origin = new Vector2(image.Width / 2, image.Height / 2);
            origin = Vector2.Zero;
            color = Color.White;
            rotation = 0.0f;
            layerDepth = 0.0f;
            effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position)
        {
            this.content = image;
            scale = Vector2.One;
            this.position = position;
            sourceRectangle = image.Bounds;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            color = Color.White;
            rotation = 0;
            layerDepth = 0;
            effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale)
        {
            this.content = image;
            this.scale = scale;
            this.position = position;
            sourceRectangle = image.Bounds;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            color = Color.White;
            rotation = 0;
            layerDepth = 0;
            effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color)
        {
            this.content = image;
            this.scale = scale;
            this.position = position;
            sourceRectangle = image.Bounds;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            this.color = Color.White;
            rotation = 0;
            layerDepth = 0;
            effects = SpriteEffects.None;
        }
        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color, float rotation)
        {
            this.content = image;
            this.scale = scale;
            this.position = position;
            sourceRectangle = image.Bounds;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            this.color = Color.White;
            this.rotation = rotation;
            layerDepth = 0;
            effects = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color, float rotation, int layerDepth)
        {
            this.content = image;
            this.scale = scale;
            this.position = position;
            sourceRectangle = image.Bounds;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            this.color = Color.White;
            this.rotation = rotation;
            this.layerDepth = layerDepth;
            effects = SpriteEffects.None;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            //Debug.WriteLine("Drawing {0} at {1}, {2}", this.content.Name, position.X, position.Y);
            //spriteBatch.Draw(this.content, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            //spriteBatch.Draw(this.content, position, new Rectangle(0,0,25,25), color, rotation, origin, scale, effects, layerDepth);
            //spriteBatch.Draw(this.content, position, color);
            spriteBatch.Draw(this.content, position, sourceRectangle, color);
        }


    }
}
