using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface ICollisionHandler
    {
        // for classes, have a constructor that passes up reference to player, enemy, etc.
        void HandleCollision(); 


    }
}
