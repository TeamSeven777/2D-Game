using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _2D_Game
{
    internal class EnemyCollisionHandler : ICollisionHandler
    {
        IEnemy enemy;

        public EnemyCollisionHandler(IEnemy thisGuy)
        {
            enemy = thisGuy;
        }
        public void HandleCollision(IHitboxOwner hbOwner)
        {
            IHitbox collider = hbOwner.Hitbox;
            Rectangle temp = Rectangle.Empty;
            HashSet<Side> resultsOfWillCollide = enemy.Hitbox.WillCollide(collider);//fix to use willcollide for raycasting,
            HashSet<Side> resultsOfIsCollide = enemy.Hitbox.IsCollide(collider);
            //First determines if a collision will happen
            if (!resultsOfIsCollide.Contains(Side.None))
            {
                //Then how the collision should be handled
                switch (collider.Type)
                {
                    case (HitBoxType.PlayerAttack):                        
                        if (hbOwner is ITool collidedTool) {
                            int knockback;
                            if (collidedTool.GetTool() == Tool.Fist) knockback = 50;
                            else if (collidedTool.GetTool() == Tool.Stick) knockback = 25;
                            else knockback = 10;
                            //Then which side(s) the collision happened
                            //TODO update for diagonal
                             //this only checks one side.
                            switch (collidedTool.direction)
                            {
                                case (Direction.Left):
                                    enemy.Position = new Vector2(enemy.Position.X - knockback, enemy.Position.Y);
                                    break;
                                case (Direction.Right):
                                    enemy.Position = new Vector2(enemy.Position.X + knockback, enemy.Position.Y);
                                    break;
                                case (Direction.Up):
                                    enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - knockback);
                                    break;
                                case (Direction.Down):
                                    enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + knockback);
                                    break;
                            }
                            //Ouch!
                            // player.TakeDamage(1);
                        }
                        break;
                }
                enemy.Update(RoomCollisionHandler.Instance.RoomGameTime, enemy.Position);                
            }
        }
    }
}
