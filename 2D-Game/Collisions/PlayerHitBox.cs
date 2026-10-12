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
        public PlayerHitBox(Rectangle area, DefaultPlayer player)
        {
            BoundingBox = area;
            Type = HitBoxType.Player;
            this.player = player;
        }

        
        public Tuple<Side,bool> WillCollide(IHitbox box)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom,false);
            Rectangle temp = Rectangle.Empty;

            //Attempts to predict whether or not the hitbox will collide in the next update, and uses area of intersection
            //to decide direction
            switch (player.stateMachine.direction)
            {
                case (Direction.Left):
                    //If the player is moving left, create a new rectangle where they will be
                    //And if it collides then it will prevent movement in CollisionHandler
                    temp = new Rectangle((int)(BoundingBox.X - player.WalkSpeed), BoundingBox.Y, BoundingBox.Width, BoundingBox.Height);
                    if (temp.Intersects(box.BoundingBox))
                        resultOfCollision = new Tuple<Side, bool>(Side.Right, true);
                    break;
                case (Direction.Right):
                    temp = new Rectangle((int)(BoundingBox.X + player.WalkSpeed), BoundingBox.Y, BoundingBox.Width, BoundingBox.Height);
                    if (temp.Intersects(box.BoundingBox))
                        resultOfCollision = new Tuple<Side, bool>(Side.Left, true);
                    break;
                case (Direction.Up):
                    temp = new Rectangle(BoundingBox.X,(int)(BoundingBox.Y - player.WalkSpeed), BoundingBox.Width, BoundingBox.Height);
                    if (temp.Intersects(box.BoundingBox))
                        resultOfCollision = new Tuple<Side, bool>(Side.Bottom, true);
                    break;
                case (Direction.Down):
                    temp = new Rectangle(BoundingBox.X, (int)(BoundingBox.Y + player.WalkSpeed), BoundingBox.Width, BoundingBox.Height);
                    if (temp.Intersects(box.BoundingBox))
                        resultOfCollision = new Tuple<Side, bool>(Side.Top, true);
                    break;
            }
           

            return resultOfCollision;
        }

        public Tuple<Side,bool> IsCollide(IHitbox box)
        {
            Tuple<Side, bool> resultOfCollision = new Tuple<Side, bool>(Side.Bottom, false);
            if (this.BoundingBox.Intersects(box.BoundingBox))
            { 
                //Will determine the direction depending on the difference between 
                //the area the rectangles collide
                Rectangle temp = this.BoundingBox;
                temp = Rectangle.Intersect(temp,box.BoundingBox);
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
