using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    internal class BlankCollisionHandler : ICollisionHandler
    {
        public BlankCollisionHandler()
        {
            //dummy class
        }
        public void HandleCollision(IHitbox collider)
        {
            //do nothing (the object with this handler does not handle collision).
        }
    }
}
