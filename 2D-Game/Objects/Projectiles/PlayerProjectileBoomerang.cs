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
    public class PlayerProjectileBoomerang : IProjectile, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        public IHitbox Hitbox { get; set; }
        public Vector2 TravelingDirection { get; set; }
        public Vector2 TravelApex { get; set; }
        public ISprite ProjectileSprite { get; set; }
        public float DespawnTimer { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }

        public PlayerProjectileBoomerang(Vector2 position, Vector2 travelingDirection, ISprite sprite)
        {
            this.Position = position;
            this.Hitbox = new ProjectileHitBox(new Rectangle((int)Position.X - 32, (int)Position.Y - 32, 64, 64), HitBoxType.PlayerProjectile);
            RoomCollisionHandler.Instance.Add(this, true);
            this.CollisionHandler = new ProjectileCollisionHandler(this);
            TravelApex = position + travelingDirection * 50;
            this.TravelingDirection = travelingDirection;
            this.ProjectileSprite = sprite;
            DespawnTimer = 1000;
           
        }

        public void Update(GameTime gt)
        {
            Hitbox.BoundingBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 32, 64, 64);
            Position += TravelingDirection;
            if (DespawnTimer > 0) DespawnTimer--;
            if (Position == TravelApex)
                TravelingDirection = -TravelingDirection;
            ProjectileSprite.Update(gt);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            //IDK how the rotating sprite draw works so I just did this :p
            //hopefully it doesn't break anything
            if (ProjectileSprite.Rotation != 360)
                ProjectileSprite.Rotation += 0.5f;
            else ProjectileSprite.Rotation = 0;
            ProjectileSprite.Draw(sprbatch, Position);
        }
    }
}
