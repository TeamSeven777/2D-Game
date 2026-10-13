using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerAttackHitBox : IHitbox
    {
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }
        public ITool Tool;
        private Direction direction;
        public PlayerAttackHitBox(Rectangle area, ITool tool, Direction dir)
        {
            BoundingBox = area;
            Type = HitBoxType.PlayerAttack;
            Tool = tool;
            direction = dir;
        }


        public Tuple<Side, bool> WillCollide(IHitbox box)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);            
            //Unnecessary for this HB type
            return resultOfCollision;
        }

        public Tuple<Side, bool> IsCollide(IHitbox box)
        {
            //TODO modify
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);
            if (this.BoundingBox.Intersects(box.BoundingBox))
            {
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp, box.BoundingBox);
                if (temp.Width > temp.Height)
                {
                    if (this.BoundingBox.X > box.BoundingBox.X)
                        resultOfCollision = new Tuple<Side, bool>(Side.Right, true);
                    else
                        resultOfCollision = new Tuple<Side, bool>(Side.Left, true);

                }
                else
                {
                    if (this.BoundingBox.Y > box.BoundingBox.Y)
                        resultOfCollision = new Tuple<Side, bool>(Side.Top, true);
                    else
                        resultOfCollision = new Tuple<Side, bool>(Side.Bottom, true);
                }
                return resultOfCollision;
            }
            else
            {
                return resultOfCollision;
            }
        }
    }
}
