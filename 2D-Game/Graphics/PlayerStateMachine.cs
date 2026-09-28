using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerStateMachine : IStateMachine
    {

        //Here for other animations of the player
        //private enum 


        public AnimatedSprite GetDirectionalSprite(IStateMachine.Direction direction, Dictionary<IStateMachine.Direction, AnimatedSprite> spriteSet)
        {
            if (spriteSet.ContainsKey(direction))
            {
                return spriteSet[direction];
            }
            else
            {
                return spriteSet[IStateMachine.Direction.Down];
            }
        }

        // Then other functions for other states etc...
    }
}
