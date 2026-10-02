using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IHitbox
{
        bool WillCollide(IHitbox hitbox);
        bool IsCollide(IHitbox hitbox);
    }
}
