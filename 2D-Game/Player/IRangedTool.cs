using _2D_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IRangedTool : ITool
{
        public IProjectile CreateProjectile(Vector2 pos);
}
}
