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
        public Vector2 size { get; set; }
        public Vector2 position { get; set; }
        public Color color { get; set; }
        public float rotation { get; set; }
        public Vector2 scale {  get; set; }
        public float layerDepth { get; set; }
        public SpriteEffects effects { get; set; }

        //Actual sprite and its bounding box
        public Texture2D content { get; set; }
        public Rectangle sourceRectangle {  get; set; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
