using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }
        public bool WillCollide(IHitbox hitbox);
        public bool IsCollide(IHitbox hitbox);
    }
}
