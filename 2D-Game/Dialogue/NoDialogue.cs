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
        public Queue<string> DialogueList { get; set; }
        public SpriteFont Font { get; set; }
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
