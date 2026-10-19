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
    public class Boomerang : ITool
    {
        public Direction direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
        }

        public Tool GetTool()
        {
            return Tool.Boomerang;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
