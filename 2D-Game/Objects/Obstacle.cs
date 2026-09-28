using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace _2D_Game
{
    public class Obstacle : IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public Vector2 originPos { get; set; }
        public Sprite sprite { get; set; }
        public Queue<Sprite> spriteSet;
        public Obstacle(Rectangle boundingBox, Vector2 originPos, Sprite sprite)
        {
            this.originPos = originPos;
            this.sprite = sprite;
            this.boundingBox = boundingBox;
        }

        public Obstacle(Rectangle boundingBox, Vector2 originPos, Queue<Sprite> sprites)
        {
            this.originPos = originPos;
            spriteSet = sprites;
            this.sprite = sprites.First();
            this.boundingBox = boundingBox;
        }


        public void NextObstacle()
        {

            if (spriteSet != null)
            {
                sprite = spriteSet.First();
                spriteSet.Enqueue(spriteSet.Dequeue());
            }
        }

        public void PrevObstacle()
        {
            if (spriteSet != null)
            {
                spriteSet.Reverse();
                sprite = spriteSet.First();
                spriteSet.Enqueue(spriteSet.Dequeue());
                spriteSet.Reverse();
            }
        }

        public void Draw(SpriteBatch sprbatch)
        {
            sprite.Draw(sprbatch, originPos);
        }
        public void Update(GameTime gametime)
        {
            //If player.boundingBox.getArea == this.boundingBox.getArea (?) do player.Pos.X / Y - distance to edge
            //OR
            //If player.boundingBox.getArea == this.boundingBox.getArea do clamp player.Pos.X && || Y
            //OR (more likely)
            //CommandCheckCollisions
        }



    }
}
