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
        private SpriteFont _font;
        private Texture2D DialogueBox;
        private int BoxOffset = 10;
        public Dialogue(Queue<string> dialogueList, SpriteFont font)//cannot give empty list of dialogue//TODO remove pos?
        {
            DialogueList = dialogueList;
            currentLine = DialogueList.Dequeue();
            _font = font;
            DialogueBox = new Texture2D(RoomCollisionHandler.Instance.myGame.GraphicsDevice, 1, 1);
            DialogueBox.SetData<Color>(new Color[] { Color.Black });
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
        public void Draw(SpriteBatch spriteBatch, Vector2 pos) {
            Vector2 center = _font.MeasureString(currentLine) / 2f;
            Rectangle textBox = new Rectangle(pos.ToPoint() - center.ToPoint() - new Point(BoxOffset, BoxOffset), _font.MeasureString(currentLine).ToPoint() + new Point(BoxOffset, BoxOffset) * 2);
            spriteBatch.Draw(DialogueBox, textBox, Rectangle.Empty, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, currentLine, pos, Color.White, 0f, center, 1f, SpriteEffects.None, 1f);//temporary placement for testing
        }
}
}
