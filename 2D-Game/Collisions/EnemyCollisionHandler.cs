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

        }
}
}
