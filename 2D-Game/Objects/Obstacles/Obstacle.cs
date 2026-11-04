using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Obstacle : IObstacle, IHitboxOwner
    {
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public Vector2 OriginPos { get; set; }
        public ISprite Sprite { get; set; }
        private bool isDestructible = false;
        public bool MarkedForRemoval { get; set; }
        private double cooldownTimer = 0.21;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// Creates an obstacle with <paramref name="boundingBox"/> hitbox at <paramref name="originPos"/>
        /// </summary>
        /// <param name="boundingBox">In case sprite doesnt match boundaries</param>
        /// <param name="originPos"></param>
        /// <param name="sprite"></param>
        public Obstacle(Vector2 size, Vector2 originPos, ISprite sprite, bool doesDamage = false)
        {
            this.OriginPos = originPos;
            this.Sprite = sprite;
            MarkedForRemoval = false;

            if (doesDamage == false)
            {
                this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint() / 2, size.ToPoint()));
            }
            else
            {
                this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint()/2, size.ToPoint()),HitBoxType.HurtBlock);
            }

            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);

        }


        

        /// <summary>
        /// Draws obstacle to screen using <paramref name="sprbatch"/>
        /// </summary>
        /// <param name="sprbatch"></param>
        public void Draw(SpriteBatch sprbatch)
        {
            Sprite.Draw(sprbatch, OriginPos);
        }
        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            cooldownTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }



    }
}
