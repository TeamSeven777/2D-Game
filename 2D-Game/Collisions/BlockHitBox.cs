using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class BlockHitBox : IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }

        public bool WillCollide(IHitbox collider)
        {
            return true;
        }
        public bool IsCollide(IHitbox collider)
        {
            if (this.BoundingBox.IntersectsWith(collider.BoundingBox))
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }
}
}
