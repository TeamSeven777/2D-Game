using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2D_Game
{
    public class Stopwatch
{
        /*
         * This class provides implementation of one of those timers that you wind up.
         * good for cooldowns.
         */
        public double Time = 0.0;
        private double milliseconds = 0.0;
        private double seconds = 0.0;
        private double minutes = 0.0;
        bool Active = true;
        /// <summary>
        /// Creates a stopwatch
        /// </summary>
        /// <param name="timeCount"></param>
        public Stopwatch() {
        }
        public void Update(GameTime gt) {
            milliseconds += gt.ElapsedGameTime.TotalMilliseconds;
            if(milliseconds >= 99)
            {
                milliseconds = 0;
            }

            seconds += gt.ElapsedGameTime.TotalSeconds;
            if(seconds >= 60)
            {
                seconds = 0;
            }

            minutes = gt.TotalGameTime.Minutes;
        }
        public void Resume()
        {
            Active = true;
        }
        public void Pause()
        {
            Active = false;
        }
        public void Reset()
        {
            Time = 0;
        }

        override public string ToString()
        {
            string counter = $" Time = {minutes.ToString("00")} : {seconds.ToString("00")} : {milliseconds.ToString("00")}";

            return counter;
        }
    }
}
