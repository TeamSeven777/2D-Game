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
    public class PlayerProjectileDynamite : IProjectile, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        public IHitbox Hitbox { get; set; }
        public Vector2 TravelingDirection { get; set; }
        public Vector2 TravelDestination { get; set; }
        public ISprite ProjectileSprite { get; set; }
        public float DespawnTimer { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }

        public PlayerProjectileDynamite(Vector2 position, Vector2 travelingDirection, ISprite sprite)
        {
            this.Position = position;
            this.TravelingDirection = travelingDirection;
            TravelDestination = position + travelingDirection * 8;
            this.ProjectileSprite = sprite;
            this.CollisionHandler = new ProjectileCollisionHandler(this);
            DespawnTimer = 150;
        }

        public void Update(GameTime gt)
        {
            if (Position != TravelDestination)
                Position += TravelingDirection;
            if (DespawnTimer == 100)
            {
                this.ProjectileSprite = ProjectileSpriteFactory.Instance.CreateExplosionAnimation();
                Hitbox = new ProjectileHitBox(new Rectangle((int)Position.X - 50,(int)Position.Y - 50,100,100), HitBoxType.Explosion);
                RoomCollisionHandler.Instance.Add(this, true); 
            }
            if (DespawnTimer > 0) DespawnTimer--;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
