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
        public HashSet<Side> WillCollide(IHitbox collider)
        {
            HashSet<Side> result = new HashSet<Side>();
            result.Add(Side.None);
            return result;
        }
        public HashSet<Side> IsCollide(IHitbox collider)
        {
            HashSet<Side> result = new HashSet<Side>();
            if (this.BoundingBox.Intersects(collider.BoundingBox))
            {
                result.Add(Side.SomeSide);
                return result;
            }
            else
            {
                result.Add(Side.None);
                return result;
            }
        }
}
}
