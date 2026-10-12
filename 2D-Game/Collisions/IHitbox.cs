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
        public Tuple<Side, bool> WillCollide(IHitbox hitbox);
        public Tuple<Side, bool> IsCollide(IHitbox hitbox);
    }
}
