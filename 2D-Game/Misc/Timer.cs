using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Timer
{
        /*
         * This class provides implementation of one of those timers that you wind up.
         * good for cooldowns.
         */
        private double TimeLeft;
        public double StartTime { get; set; }
        public bool Ringing {  get; private set; }
        public Timer(double timeCount) {
            TimeLeft = timeCount;
            StartTime = TimeLeft;
        }
        public void Update(GameTime gt) {
            if (TimeLeft > 0)
            {
                TimeLeft -= gt.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Ringing = true;
            }
        }
        public void Reset()
        {
            TimeLeft = StartTime;
            Ringing = false;
        }
}
}
