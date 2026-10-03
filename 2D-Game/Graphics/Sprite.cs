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

        public Sprite(Texture2D image, Rectangle src = new Rectangle())
        {
            this.Content = image;
            this.Scale = Vector2.One;
            this.Position = Vector2.Zero;
            SourceRectangle = src;
            Origin = new Vector2(src.Width / 2, src.Height / 2);
            Origin = new Vector2(0, 0);
            this.Color = Color.White;
            this.Rotation = 0;
            this.LayerDepth = 0;
            Effects = SpriteEffects.None;
        }


        public Sprite(Texture2D image, Vector2 position, Vector2 scale, Color color, Rectangle src = new Rectangle(), SpriteEffects effect = SpriteEffects.None, float rotation = 0, int layerDepth = 0)
        {
            this.Content = image;
            this.Scale = scale;
            this.Position = position;
            SourceRectangle = src;
            Origin = new Vector2(src.Width / 2, src.Height / 2);
            this.Color = Color.White;
            this.Rotation = rotation;
            this.LayerDepth = layerDepth;
            Effects = effect;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            spriteBatch.Draw(this.Content, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);  
        
        }

        public void Update(GameTime gt) { }


    }
}
