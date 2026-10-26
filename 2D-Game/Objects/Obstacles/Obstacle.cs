using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Obstacle : IObstacle, IHitboxOwner, IInteractable
    {
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public Vector2 originPos { get; set; }
        public ISprite sprite { get; set; }
        private bool isReversed = false;
        private bool doesDamage = false;
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
            this.originPos = originPos;
            this.sprite = sprite;
            if (doesDamage == false)
            {
                this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint()/2, size.ToPoint()));
            }
            else
            {
                this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint()/2, size.ToPoint()),HitBoxType.HurtBlock);
            }
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);
            this.doesDamage = doesDamage;

        }

     

        public void HandleInteraction() {
            //interaction testing
            RoomCollisionHandler.Instance.myGame.CurrentDialogue = DialogueFactory.Instance.CreateBlockDialogue();
            RoomCollisionHandler.Instance.myGame.HandleDialogue();
        }

        

        /// <summary>
        /// Draws obstacle to screen using <paramref name="sprbatch"/>
        /// </summary>
        /// <param name="sprbatch"></param>
        public void Draw(SpriteBatch sprbatch)
        {
            sprite.Draw(sprbatch, originPos);
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
