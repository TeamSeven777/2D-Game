using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class EnemyHitBox : IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }

        public EnemyHitBox(Rectangle area)
        {
            BoundingBox = area;
            Type = HitBoxType.Enemy;
        }

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
        }
    }
}
