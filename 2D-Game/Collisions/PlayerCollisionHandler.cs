using _2D_Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerCollisionHandler : ICollisionHandler
    {
        DefaultPlayer player;


        public PlayerCollisionHandler(DefaultPlayer player)
        {
            this.player = player;
        }
        public void HandleCollision(IHitbox collider)
        {
            Rectangle temp = Rectangle.Empty;
            switch (collider.Type)
            {
                case (HitBoxType.Enemy):
                    if (player.HitBox.IsCollide(collider))
                    {
                        temp = player.HitBox.BoundingBox;
                        temp.Intersect(collider.BoundingBox);
                        if (temp.Width > temp.Height)
                        {
                            //How do we control X vs. Y movement?
                            //Maybe variable in player class that bounds X and Y, and will
                            //Set that here? otherwise it will be float.MAX or something
                            //to that effect?
                            player.stateMachine.CanMove = false;
                        }
                        player.TakeDamage(1);
                    }
                    break;
                case (HitBoxType.Block):
                    break;
            }
            
            
        }
    }
}
