using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace _2D_Game
{
    public class Boggus
{
        public static ISprite defaultBoggus { get; private set; }
        public Boggus(Texture2D boggusPic)
        {
            defaultBoggus = new Sprite(boggusPic);
        }
        public Boggus()
        {
            //empty
        }
        public ISprite GetBoggusSprite()
        {
            return defaultBoggus;
        }
}
}
