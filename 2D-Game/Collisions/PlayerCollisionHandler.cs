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
            HashSet<Side> resultsOfWillCollide = player.Hitbox.WillCollide(collider);//fix to use willcollide for raycasting, and Is for the standard hitbox.
            HashSet<Side> resultsOfIsCollide = player.Hitbox.IsCollide(collider);
            //First determines if a collision will happen
            if (!resultsOfWillCollide.Contains(Side.None))
            {
                //Then how the collision should be handled
                switch (collider.Type)
                {
                    case (HitBoxType.Enemy):

                        //Then which side(s) the collision happened
                        //TODO update for diagonal
                        Side firstSide = resultsOfWillCollide.ElementAt(0); //this only checks one side.
                        switch(firstSide){
                            case (Side.Left):
                                player.ToggleAllowedDirection(Direction.Right, false);
                                //Maybe have some knockback and push the player back a set amount?
                                player.NextPosition.X = player.Position.X - 50;
                                break;
                            case (Side.Right):
                                player.ToggleAllowedDirection(Direction.Left, false);
                                player.NextPosition.X = player.Position.X + 50;
                                //player.Position.X = player.Position.X + (int)collider.BoundingBox.Width;
                                //player.stateMachine.CanMove = true;
                                break;
                            case (Side.Top):
                                player.ToggleAllowedDirection(Direction.Down, false);
                                player.NextPosition.Y = player.Position.Y - 50;
                                //player.Position.X = player.Position.X - (int)collider.BoundingBox.Height;
                                //player.stateMachine.CanMove = true;
                                break;
                            case (Side.Bottom):
                                player.ToggleAllowedDirection(Direction.Up, false);
                                player.NextPosition.Y = player.Position.Y + 50;
                                //player.Position.X = player.Position.X + (int)collider.BoundingBox.Height;
                                //player.stateMachine.CanMove = true;
                                break;
                        }
                        //Ouch!
                        player.TakeDamage(1);
                        break;
                    case (HitBoxType.Block):
                        if (resultsOfWillCollide.Contains(Side.Left)){
                            player.ToggleAllowedDirection(Direction.Right, false);
                            if (!resultsOfIsCollide.Contains(Side.None))
                                player.Position.X -= player.WalkSpeed;
                        }
                        if (resultsOfWillCollide.Contains(Side.Right)){
                            player.ToggleAllowedDirection(Direction.Left, false);
                            if (!resultsOfIsCollide.Contains(Side.None))
                                player.Position.X += player.WalkSpeed;
                        }
                        if (resultsOfWillCollide.Contains(Side.Top)){
                            player.ToggleAllowedDirection(Direction.Down, false);
                            if (!resultsOfIsCollide.Contains(Side.None))
                                player.Position.Y -= player.WalkSpeed;
                        }
                        if (resultsOfWillCollide.Contains(Side.Bottom)){
                            player.ToggleAllowedDirection(Direction.Up, false);
                            if (!resultsOfIsCollide.Contains(Side.None))
                                player.Position.Y += player.WalkSpeed;
                        }
                        break;
            
                }
            }
            else// if (player.DisabledDirections.Count > 0 && !player.stateMachine.Moving)
            {
                // player.DisabledDirections.Clear();
                switch (collider.Type)
                {
                    case(HitBoxType.Block):
                        //remove?
                        break;
                    default:
                        //nothing to be done
                        break;
                }
            }


        }
    }
}
