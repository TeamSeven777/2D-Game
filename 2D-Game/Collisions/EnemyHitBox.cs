
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class EnemyHitBox : IHitbox
{
        public Rectangle BoundingBox { get; set; }
        public HitBoxType Type { get; set; }

        public EnemyHitBox(Rectangle area)
        {
            BoundingBox = area;
            Type = HitBoxType.Enemy;
        }

        public HashSet<Side> WillCollide(IHitbox collider)
        {
            HashSet<Side> result = new HashSet<Side>();
            result.Add(Side.None);
            return result;
        }

        public HashSet<Side> IsCollide(IHitbox collider)
        {
            HashSet<Side> result = new HashSet<Side>();  
            if (this.BoundingBox.Intersects(collider.BoundingBox))
            {
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp, collider.BoundingBox);
                if (temp.Width > temp.Height)
                {
                    if (this.BoundingBox.X > collider.BoundingBox.X)
                        result.Add(Side.Right);
                    else
                        result.Add(Side.Left);

                }
                else
                {
                    if (this.BoundingBox.Y > collider.BoundingBox.Y)
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
