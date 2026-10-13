using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Slingshot : ITool
{
        public Tool GetTool()
        {
            return Tool.Slingshot;
        }
        public void Use() {
            //TODO add proper function
        }
        public void Update(GameTime gt)
        {

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position) { }
    }
}
