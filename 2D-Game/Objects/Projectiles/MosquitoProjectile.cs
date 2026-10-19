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
    public class MosquitoProjectile : IProjectile, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        public IHitbox Hitbox { get; set; }
        float angle;
        public Vector2 TravelingDirection { get; set; }

        public ISprite ProjectileSprite { get; set; }

        public float DespawnTimer { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }

        public MosquitoProjectile(Vector2 position, Vector2 targetDirection, ISprite sprite)
        {
            this.Position = position;
            this.TravelingDirection = targetDirection;
            this.ProjectileSprite = sprite;
            CollisionHandler = new ProjectileCollisionHandler(this);
            Hitbox = new ProjectileHitBox(new Rectangle((int)Position.X, (int)Position.Y, 64, 64), HitBoxType.EnemyAttack);
            RoomCollisionHandler.Instance.Add(this, true);
        }

        public void Update(GameTime gt)
        {
            Position += TravelingDirection;
            Hitbox.BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
