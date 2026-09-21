using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Fourest_Seven.lib.Graphics
{
    public interface ISprite
    {
        //Properties of the sprite
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale {  get; set; }
        public float LayerDepth { get; set; }
        public SpriteEffects Effects { get; set; }

        //Actual sprite and its bounding box
        public Texture2D Content { get; set; }
        public Rectangle SourceRectangle {  get; set; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
