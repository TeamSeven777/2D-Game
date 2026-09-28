using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IStateMachine
    {
        public enum Direction {Left, Right, Up, Down };

        public AnimatedSprite GetDirectionalSprite(IStateMachine.Direction direction, Dictionary<IStateMachine.Direction, AnimatedSprite> spriteSet);

    }
}