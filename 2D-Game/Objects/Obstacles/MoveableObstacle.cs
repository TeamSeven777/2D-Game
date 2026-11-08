using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace _2D_Game
{
    public class MoveableObstacle : IObstacle, IHitboxOwner, IInteractable
    {
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public Vector2 OriginPos { get; set; }
        public ISprite Sprite { get; set; }
        public bool CanMove = true;
        public bool MarkedForRemoval { get; set; }
        public int MoveDistance = 10; 

        /// <summary>
        /// Creates an obstacle with <paramref name="boundingBox"/> hitbox at <paramref name="originPos"/>
        /// </summary>
        /// <param name="boundingBox">In case sprite doesnt match boundaries</param>
        /// <param name="originPos"></param>
        /// <param name="sprite"></param>
        public MoveableObstacle(Vector2 size, Vector2 originPos, ISprite sprite, bool doesDamage = false, int moveDistance = 10)
        {
            this.MoveDistance = moveDistance;
            this.OriginPos = originPos;
            this.Sprite = sprite;
            MarkedForRemoval = false;
            this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint() / 2, size.ToPoint()), HitBoxType.MoveableBlock);

            CollisionHandler = new MoveableBlockCollisionHandler(this);
            RoomCollisionHandler.Instance.Add(this, true);
            MoveDistance = moveDistance;
        }

        public void HandleInteraction()
        {
            if (CanMove)
            {
                Direction s = RoomCollisionHandler.Instance.myGame.player.stateMachine.direction;
                switch (s)
                {
                    case (Direction.Up):
                        OriginPos += new Vector2(0, -MoveDistance);
                        Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint() - Hitbox.BoundingBox.Size / 2, Hitbox.BoundingBox.Size), HitBoxType.MoveableBlock);
                        break;
                    case (Direction.Down):
                        OriginPos += new Vector2(0, MoveDistance);
                        Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint() - Hitbox.BoundingBox.Size / 2, Hitbox.BoundingBox.Size), HitBoxType.MoveableBlock);
                        break;
                    case (Direction.Right):
                        OriginPos += new Vector2(MoveDistance, 0);
                        Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint() - Hitbox.BoundingBox.Size / 2, Hitbox.BoundingBox.Size), HitBoxType.MoveableBlock);
                        break;
                    case (Direction.Left):
                        OriginPos += new Vector2(-MoveDistance, 0);
                        Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint() - Hitbox.BoundingBox.Size / 2, Hitbox.BoundingBox.Size), HitBoxType.MoveableBlock);
                        break;
                }
            }
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

        }



    }
}
