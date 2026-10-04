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
    internal class Obstacle : IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public float originPos { get; set; }
        public Texture2D sprite { get; set; }

        public Obstacle(Rectangle boundingBox, float originPos, Texture2D sprite)
        {
            this.originPos = originPos;
            this.sprite = sprite;
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
