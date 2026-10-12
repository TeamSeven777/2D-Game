using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class BlockHitBox : IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }

        public BlockHitBox(Rectangle boundingBox)
        {
            this.BoundingBox = boundingBox;
            this.Type = HitBoxType.Block;
        }
        public Tuple<Side,bool> WillCollide(IHitbox collider)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);
            return resultOfCollision;
        }
        public Tuple<Side,bool> IsCollide(IHitbox collider)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);
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
