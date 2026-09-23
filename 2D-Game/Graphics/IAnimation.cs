using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IAnimation
    {
        public Queue<Rectangle> Frames { get; set; }
        public TimeSpan FrameRate { get; set; }
        
    }
}
