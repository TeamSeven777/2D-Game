using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IDialogue
{
        void Update(GameTime gameTime);
        bool Next(); //will either move to next line of dialogue or return false
        void Draw(SpriteBatch spriteBatch, Vector2 pos);
}
}
