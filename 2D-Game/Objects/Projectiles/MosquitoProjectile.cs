using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class MosquitoProjectile : IProjectile
    {
        public Vector2 position { get; set; }

        float angle;
        public Vector2 travelingDirection { get; set; }

        public ISprite sprite { get; set; }

        public MosquitoProjectile(Vector2 position, Vector2 targetPosition, Sprite sprite)
        {
            this.position = position;
            angle = (float)(Math.PI / 2) + (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            this.travelingDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            this.sprite = sprite;
        }

        public void Update(GameTime gt)
        {
            position += travelingDirection;
        }
        public void Update(GameTime gt, Vector2 targetPosition)
        {

            //Temporarily this method will accept a Target Position
            //Should be removed later, and inherit the position of
            //the mosquitos "target"
            angle = (float)(Math.PI / 2) + (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            travelingDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            position += travelingDirection;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            sprite.Draw(sprbatch, position);
        }
    }
}
