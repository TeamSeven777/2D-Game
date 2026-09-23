using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class AnimatedSpriteStateMachine
    {
        public Dictionary<Direction, AnimatedSprite> spriteSet;
        public enum Direction {Up, Down, Left, Right};
        //Here for other animations of the player
        //private enum 

        public AnimatedSprite getDirectionSprite(Direction direction)
        {
            if (spriteSet.ContainsKey(direction))
            {
                return spriteSet[direction];
            }
            else
            {
                return spriteSet[Direction.Up];
            }
         
        }

        // Then other functions for other states etc...
    }
}
