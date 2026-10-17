using _2D_Game;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class ProjectileHitBox : IHitbox
    {
       public Rectangle BoundingBox { get; set; }
       public HitBoxType Type { get; set; }

        public ProjectileHitBox(Rectangle BoundingBox, HitBoxType Type)
        {
            this.BoundingBox = BoundingBox;
            this.Type = Type;
        }

        public HashSet<Side> WillCollide(IHitbox collider)
        {
            return new HashSet<Side>();
        }

        public HashSet<Side> IsCollide(IHitbox Hitbox)
        {
            HashSet<Side> result = new HashSet<Side>();
            if (this.BoundingBox.Intersects(Hitbox.BoundingBox))
            {
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp, Hitbox.BoundingBox);
                if (temp.Width > temp.Height)
                {
                    if (this.BoundingBox.X > Hitbox.BoundingBox.X)
                        result.Add(Side.Right);
                    else
                        result.Add(Side.Left);

                }
                else
                {
                    if (this.BoundingBox.Y > Hitbox.BoundingBox.Y)
                        result.Add(Side.Top);
                    else
                        result.Add(Side.Bottom);
                }
            }
            return result;
        }

    }

}
