using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class MoveableBlockCollisionHandler : ICollisionHandler
    {
        MoveableObstacle thisBlock; 
        public MoveableBlockCollisionHandler(MoveableObstacle thisBlock)
        {
            this.thisBlock = thisBlock;
        }
        public void HandleCollision(IHitboxOwner hbOwner)
        {
            IHitbox collider = hbOwner.Hitbox;
            HashSet<Side> resultsOfWillCollide = thisBlock.Hitbox.WillCollide(collider);//fix to use willcollide for raycasting, and Is for the standard hitbox.
            HashSet<Side> resultsOfIsCollide = thisBlock.Hitbox.IsCollide(collider);
            //First determines if a collision will happen
            if (resultsOfIsCollide.Contains(Side.None))
            {
                thisBlock.CanMove = true;
            }
            else
            {
                thisBlock.CanMove = false;
            }
        }
    }
}
