using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }
        public HashSet<Side> WillCollide(IHitbox hitbox);
        public HashSet<Side> IsCollide(IHitbox hitbox);
    }
}
