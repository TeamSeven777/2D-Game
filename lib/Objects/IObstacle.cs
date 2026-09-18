using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Fourest_Seven.lib.Objects
{
    internal interface IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public Vector2 originPos { get; set; }

        // Sprite Class not yet implemented, or something similar
        public Texture2D sprite { get; set; }

        public void Update(GameTime gameTime);

    }
}
