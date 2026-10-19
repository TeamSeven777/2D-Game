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
    internal class Dyanmite : ITool
    {
        public Direction direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            throw new NotImplementedException();
        }

        public Tool GetTool()
        {
            return Tool.Dynamite;
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
