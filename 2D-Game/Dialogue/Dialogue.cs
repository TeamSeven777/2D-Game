using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _2D_Game
{
    public class Dialogue : IDialogue
    {
        public Queue<string> DialogueList { get; set; }
        public SpriteFont Font { get; set; }
        private Texture2D DialogueBox;
        private int BoxOffset = 10;
        private readonly string rewardItemName;

        private bool rewardCollected;

        public Dialogue(Queue<string> dialogueList, SpriteFont font,  string rewardItemName = null)
        {
            DialogueList = dialogueList ?? throw new ArgumentNullException(nameof(dialogueList));
            Font = font ?? throw new ArgumentNullException(nameof(font));
            this.rewardItemName = rewardItemName;

            DialogueBox = new Texture2D(RoomCollisionHandler.Instance.myGame.GraphicsDevice, 1, 1);
            DialogueBox.SetData(new Color[] { Color.Black });
        }

        public void Update(GameTime gameTime) { }

        public bool Next()
        {
            // No more lines: dialogue has ended.
            if (DialogueList.Count <= 1)
            {
                CollectReward();
                return false;
            }

            DialogueList.Dequeue();
            return true;
        }

        private void CollectReward()
        {
            if (rewardCollected || string.IsNullOrWhiteSpace(rewardItemName))
                return;

            IItem reward = RoomContentFactory.Instance.CreateItem(rewardItemName);

            if (reward == null)
            {
                throw new InvalidOperationException(
                    $"Could not create reward item: {rewardItemName}");
            }
            reward.Collect();
            rewardCollected = true;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            if (DialogueList.Count == 0) return;

            string currentText = DialogueList.Peek();
            Vector2 stringSize = Font.MeasureString(currentText);
            Vector2 center = stringSize / 2f;
            
            Rectangle textBox = new Rectangle(
                pos.ToPoint() - center.ToPoint() - new Point(BoxOffset, BoxOffset), 
                stringSize.ToPoint() + new Point(BoxOffset, BoxOffset) * 2
            );

            spriteBatch.Draw(DialogueBox, textBox, Rectangle.Empty, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Font, currentText, pos, Color.White, 0f, center, 1f, SpriteEffects.None, 1f);
        }
    }
}
