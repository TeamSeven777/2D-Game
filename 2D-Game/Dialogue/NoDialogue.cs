using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class NoDialogue :IDialogue
{
        public void Update(GameTime gameTime)
        {

        }
        public bool Next()
        {
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 pos) { }
}
}
