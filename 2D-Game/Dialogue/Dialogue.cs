using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _2D_Game
{
    public class Dialogue : IDialogue
{
        private Queue<string> DialogueList;
        private string currentLine;
        private Vector2 Position;
        private SpriteFont _font;
        public Dialogue(Queue<string> dialogueList, SpriteFont font, Vector2 pos)//cannot give empty list of dialogue
        {
            DialogueList = dialogueList;
            currentLine = DialogueList.Dequeue();
            _font = font;
            Position = pos;
        }
        public void Update(GameTime gameTime) { }
        public bool Next() 
        {
            bool hasNext = true;
            if (DialogueList.Count > 0) {
                currentLine = DialogueList.Dequeue();
            }
            else
            {
                hasNext = false;
            }
            return hasNext;
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(_font, currentLine, Position, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);//temporary placement for testing
        }
}
}
