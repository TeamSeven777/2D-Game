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
        //some list of hamndler types? later
        //tool classes

        public PlayerCollisionHandler(DefaultPlayer player)
        {
            this.player = player;
        }
        /// <summary>
        /// Will stop the player from moving any further into the specified <paramref name="collider"/>
        /// </summary>
        /// <param name="collider"></param>
        public void HandleCollision(IHitbox collider)
        {
            Rectangle temp = Rectangle.Empty;
            Tuple<Side, bool> resultOfCollision = player.HitBox.WillCollide(collider);
            //First determines if a collision happened
            if (resultOfCollision.Item2)
            {
                //Then how the collision should be handled
                switch (collider.Type)
                {
                    case (HitBoxType.Enemy):

                        //Then which side the collision happened
                        switch (resultOfCollision.Item1) {
                            case (Side.Left):
                                player.ToggleAllowedDirection(Direction.Right, false);
                                player.Position.X = player.Position.X - (int)collider.BoundingBox.Width;
                                player.ToggleAllowedDirection(Direction.Right, true);
                                break;
                            case (Side.Right):
                                player.stateMachine.CanMove = false;
                                player.Position.X = player.Position.X + (int)collider.BoundingBox.Width;
                                player.stateMachine.CanMove = true;
                                break;
                            case (Side.Top):
                                player.stateMachine.CanMove = false;
                                player.Position.X = player.Position.X - (int)collider.BoundingBox.Height;
                                player.stateMachine.CanMove = true;
                                break;
                            case (Side.Bottom):
                                player.stateMachine.CanMove = false;
                                player.Position.X = player.Position.X + (int)collider.BoundingBox.Height;
                                player.stateMachine.CanMove = true;
                                break;
                        }
                        //Ouch!
                        player.TakeDamage(1);
                        break;
                    case (HitBoxType.Block):
                        switch (resultOfCollision.Item1)
                        {
                            case (Side.Left):
                                player.ToggleAllowedDirection(Direction.Right, false);
                                //player.Position.X = player.Position.X - (int)collider.BoundingBox.Width;
                                //player.ToggleAllowedDirection(Direction.Right, true);
                                break;
                            case (Side.Right):
                                player.ToggleAllowedDirection(Direction.Left, false);
                                //player.Position.X = player.Position.X + (int)collider.BoundingBox.Width;
                                //player.stateMachine.CanMove = true;
                                break;
                            case (Side.Top):
                                player.ToggleAllowedDirection(Direction.Down, false);
                                //player.Position.X = player.Position.X - (int)collider.BoundingBox.Height;
                                //player.stateMachine.CanMove = true;
                                break;
                            case (Side.Bottom):
                                player.ToggleAllowedDirection(Direction.Up, false);
                                //player.Position.X = player.Position.X + (int)collider.BoundingBox.Height;
                                //player.stateMachine.CanMove = true;
                                break;
                        }
                        break;
            
                }
            }
            else if(player.DisabledDirections.Count > 0)
            {
                player.DisabledDirections.Clear();
            }
            
            
        }
    }
}
