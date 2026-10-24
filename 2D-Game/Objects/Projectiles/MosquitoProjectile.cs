using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;
using static _2D_Game.RotatingSprite;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class MosquitoProjectile : IProjectile, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        public IHitbox Hitbox { get; set; }
        public Vector2 TravelingDirection { get; set; }
        public ISprite ProjectileSprite { get; set; }
        public float DespawnTimer { get; set; }
        public int projectileSpeed = 5;
        public ICollisionHandler CollisionHandler { get; set; }

        public MosquitoProjectile(Vector2 position, Vector2 targetPosition, ISprite sprite)
        {
            this.Position = position;
            Vector2 target = targetPosition - position;
            target.Normalize();
            TravelingDirection = target;
            this.ProjectileSprite = sprite;
            DespawnTimer = 500;
            CollisionHandler = new ProjectileCollisionHandler(this);
            Hitbox = new ProjectileHitBox(new Rectangle((int)Position.X-16, (int)Position.Y-16, 32, 32), HitBoxType.EnemyProjectile);
            RoomCollisionHandler.Instance.Add(this, true);
            ProjectileSprite.Rotation = -(float)(Math.PI / 2) + (float)Math.Atan2(target.Y, target.X);

        }

        public void Update(GameTime gt)
        {
            Position += TravelingDirection * projectileSpeed;
            Hitbox.BoundingBox = new Rectangle((int)Position.X-16, (int)Position.Y-16, 32, 32);
            if (DespawnTimer > 0) DespawnTimer--;
        }

        public void Draw(SpriteBatch sprbatch)
        {

            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
