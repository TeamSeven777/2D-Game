using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game 
{
    public class Knife : ITool
    {
        public Tool GetTool()
        {
            return Tool.Knife;
        }
        public void Use()
        {
            //TODO add proper function
        }
    }
}
