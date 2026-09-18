using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Fourest_Seven.lib.Graphics
{
    public interface IAnimation
    {
        public Queue<Rectangle> frames { get; set; }
        public TimeSpan frameRate { get; set; }
        
    }
}
