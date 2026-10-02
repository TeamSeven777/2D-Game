using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class PlayerProjectile : IProjectile
    {
        public Vector2 Position { get; set; }

        public Vector2 TravelingDirection { get; set; }

        public ISprite ProjectileSprite { get; set; }

        public float DespawnTimer { get; set; }

        public PlayerProjectile(Vector2 position, Vector2 travelingDirection, ISprite sprite)
        {
            this.Position = position;
            this.TravelingDirection = travelingDirection;
            this.ProjectileSprite = sprite;
            DespawnTimer = 1000;
        }

        public void Update(GameTime gt)
        {
            Position += TravelingDirection;
            if (DespawnTimer > 0) DespawnTimer--;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
