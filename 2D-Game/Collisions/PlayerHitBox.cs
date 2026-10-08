using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerHitBox : IHitbox
    {
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }

        public PlayerHitBox(Rectangle area)
        {
            BoundingBox = area;
            Type = HitBoxType.Player;
        }

        public bool WillCollide(IHitbox box)
        {

            return true;
        }

        public bool IsCollide(IHitbox box)
        {
            if (this.BoundingBox.IntersectsWith(box.BoundingBox))
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
