
using System;
using Microsoft.Xna.Framework;
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

        public Tuple<Side,bool> WillCollide(IHitbox collider)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);
            return resultOfCollision;
        }

        public Tuple<Side,bool> IsCollide(IHitbox collider)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom,false);
            if (this.BoundingBox.Intersects(collider.BoundingBox))
            {
                
                return resultOfCollision;
            }
            else
            {
             
                return resultOfCollision;
            }
        }
    }
}
