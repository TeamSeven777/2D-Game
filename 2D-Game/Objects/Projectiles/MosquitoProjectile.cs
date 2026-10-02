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
        public Vector2 Position { get; set; }

        float angle;
        public Vector2 TravelingDirection { get; set; }

        public ISprite ProjectileSprite { get; set; }

        public float DespawnTimer { get; set; }

        public MosquitoProjectile(Vector2 position, Vector2 targetPosition, Sprite sprite)
        {
            this.Position = position;
            angle = (float)(Math.PI / 2) + (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            this.TravelingDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            this.ProjectileSprite = sprite;
        }

        public void Update(GameTime gt)
        {
            Position += TravelingDirection;
        }
        public void Update(GameTime gt, Vector2 targetPosition)
        {

            //Temporarily this method will accept a Target Position
            //Should be removed later, and inherit the position of
            //the mosquitos "target"
            angle = (float)(Math.PI / 2) + (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            TravelingDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            Position += TravelingDirection;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
