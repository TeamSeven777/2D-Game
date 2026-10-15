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


        public HashSet<Side> WillCollide(IHitbox box)
        {
            HashSet<Side> result = new HashSet<Side>();
            //Unnecessary for this HB type
            result.Add(Side.None);
            return result;
        }

        public HashSet<Side> IsCollide(IHitbox box)
        {
            //TODO modify
            HashSet<Side> result = new HashSet<Side>();
            if (this.BoundingBox.Intersects(box.BoundingBox))
            {
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp, box.BoundingBox);
                if (temp.Width > temp.Height)
                {
                    if (this.BoundingBox.X > box.BoundingBox.X)
                        result.Add(Side.Right);
                    else
                        result.Add(Side.Left);

                }
                else
                {
                    if (this.BoundingBox.Y > box.BoundingBox.Y)
                        result.Add(Side.Top);
                    else
                        result.Add(Side.Bottom);
                }
            }
            else
            {
                result.Add(Side.None);
            }
            return result;
        }
    }
}
