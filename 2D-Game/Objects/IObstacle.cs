using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    internal interface IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public Vector2 originPos { get; set; }

        // Sprite Class not yet implemented, or something similar
        public Sprite sprite { get; set; }

        public void Update(GameTime gameTime);

    }
}
