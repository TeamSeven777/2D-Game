using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerHitBox : IHitbox
    {
        public Rectangle BoundingBox { get; set; }       
        public HitBoxType Type { get; set; }
        public DefaultPlayer player;
        private int RaycastDistance = 16;
       // private int VerticalRaycastWidth;
        //private int HorizontalRaycastWidth;
        private Rectangle LeftRaycast, RightRaycast, UpRaycast, DownRaycast;
        public PlayerHitBox(Rectangle area, DefaultPlayer player)
        {
            BoundingBox = area;
            Type = HitBoxType.Player;
            this.player = player;
            UpdateRays();
        }
        private void UpdateRays() {
            LeftRaycast = new Rectangle((int)(BoundingBox.X-RaycastDistance), (int)(BoundingBox.Y), RaycastDistance, BoundingBox.Height);
            RightRaycast = new Rectangle((int)(BoundingBox.Right), (int)(BoundingBox.Y), RaycastDistance, BoundingBox.Height);
            UpRaycast = new Rectangle((int)(BoundingBox.X), (int)(BoundingBox.Y-RaycastDistance), BoundingBox.Width, RaycastDistance);
            DownRaycast = new Rectangle((int)(BoundingBox.X), (int)(BoundingBox.Bottom), BoundingBox.Width, RaycastDistance);
            RoomCollisionHandler.Instance.AddTempRectangle(LeftRaycast);
            RoomCollisionHandler.Instance.AddTempRectangle(RightRaycast);
            RoomCollisionHandler.Instance.AddTempRectangle(UpRaycast);
            RoomCollisionHandler.Instance.AddTempRectangle(DownRaycast);   
        }
        
        public HashSet<Side> WillCollide(IHitbox box)
        {
            HashSet<Side> result = new HashSet<Side>();
            UpdateRays();

            //Check each raycast, and if it intersects the collidee, add it to set of sides.
            if (LeftRaycast.Intersects(box.BoundingBox))
                result.Add(Side.Right);
            if (RightRaycast.Intersects(box.BoundingBox))
                result.Add(Side.Left);
            if (UpRaycast.Intersects(box.BoundingBox))
                result.Add(Side.Bottom);
            if (DownRaycast.Intersects(box.BoundingBox))
                result.Add(Side.Top);

            if (result.Count == 0) result.Add(Side.None); //casts don't collide

            return result;
        }

        public HashSet<Side> IsCollide(IHitbox box)
        {
            HashSet<Side> result = new HashSet<Side>();
            if (this.BoundingBox.Intersects(box.BoundingBox))
            { 
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp,box.BoundingBox);
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
