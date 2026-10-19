using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;

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
                        if ((hbOwner is ITool collidedTool) && collidedTool.GetTool() != Tool.Interact) {
                            int knockback; int dmg = 1;
                            if (collidedTool.GetTool() == Tool.Fist)
                            {
                                knockback = 50;
                                dmg = 2;
                            }
                            else if (collidedTool.GetTool() == Tool.Stick) knockback = 25;
                            else knockback = 10;
                            /*float startX = enemy.Position.X;
                            float startY = enemy.Position.Y;
                            float kbSpeed = 0.1f;
                            Timer kbUpdate = new Timer(10);*/                            
                            //Then which side(s) the collision happened
                            //TODO update for diagonal
                            //this only checks one side.
                            switch (collidedTool.direction)
                            {
                                case (Direction.Left):
                                    //TODO replace these with a class that gradually gives KB rather than Teleporting
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
                            enemy.TakeDamage(dmg);
                        }
                        break;
                }
                
            }
        }
    }
}
