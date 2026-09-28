using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Axe : ITool
{
        public Tool GetTool()
        {
            return Tool.Axe;
        }
        public void Use() { 
            //TODO connect to player for proper animations
        }
}
}
