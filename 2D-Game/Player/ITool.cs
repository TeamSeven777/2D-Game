using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface ITool
{
        Tool GetTool();
        void Use();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 position);
}
}
