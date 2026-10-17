using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class PlayerProjectilePebble : IProjectile, IHitboxOwner
    {
        public Vector2 Position { get; set; }

        public Vector2 TravelingDirection { get; set; }

        public ISprite ProjectileSprite { get; set; }

        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public float DespawnTimer { get; set; }

        public PlayerProjectilePebble(Vector2 position, Vector2 travelingDirection, ISprite sprite)
        {
            this.Position = position;
            this.TravelingDirection = travelingDirection;
            this.ProjectileSprite = sprite;
            this.Hitbox = new ProjectileHitBox(new Rectangle((int)position.X,(int)position.Y,64,64),HitBoxType.PlayerAttack);
            this.CollisionHandler = new ProjectileCollisionHandler(this);
            DespawnTimer = 1000;
            RoomCollisionHandler.Instance.Add(this, true);
        }

        public void Update(GameTime gt)
        {
            Position += TravelingDirection;
            Hitbox.BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            if (DespawnTimer > 0) DespawnTimer--;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
