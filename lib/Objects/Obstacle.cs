using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace The_Fourest_Seven.lib.Objects
{
    public class Obstacle : IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public Vector2 originPos { get; set; }

        public Obstacle(Rectangle boundingBox, Vector2 originPos)
        {
            this.originPos = originPos;
            this.boundingBox = boundingBox;
        }

        public void Update(GameTime gametime)
        {
            //If player.boundingBox.getArea == this.boundingBox.getArea (?) do player.Pos.X / Y - distance to edge
            //OR
            //If player.boundingBox.getArea == this.boundingBox.getArea do clamp player.Pos.X && || Y
            //OR (more likely)
            //CommandCheckCollisions();
        }
    }
}
