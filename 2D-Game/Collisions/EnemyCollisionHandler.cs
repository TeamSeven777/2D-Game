using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    internal class EnemyCollisionHandler : ICollisionHandler
{
        IEnemy enemy;

        public EnemyCollisionHandler(IEnemy thisGuy)
        {
            enemy = thisGuy;
        }
        public void HandleCollision(IHitbox collider)
        {
            HashSet<Side> resultsOfWillCollide = enemy.Hitbox.WillCollide(collider);//fix to use willcollide for raycasting, and Is for the standard hitbox.
            HashSet<Side> resultsOfIsCollide = enemy.Hitbox.IsCollide(collider);
            if (!resultsOfWillCollide.Contains(Side.None))
            {
                if (collider.Type == HitBoxType.PlayerAttack)
                {
                    enemy.Health--;
                }
            }
        }   
}
}
