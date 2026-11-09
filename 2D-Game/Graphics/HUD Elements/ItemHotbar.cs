using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace _2D_Game
{
    public class ItemHotbar : IHotbar
    {
        private readonly Point position;
        private readonly int cellWidth;
        private readonly int cellHeight;
        private readonly float slotSpacing;
        private readonly int totalSlots;

        private readonly Texture2D slotTexture;
        private readonly Texture2D selectionTexture;
        private readonly SpriteFont font;
        private readonly Texture2D pixel;

        private int selectedIndex = 0;
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2;

        public ItemHotbar(GraphicsDevice device, Point position, int cellWidth, int totalSlots = 7, Texture2D slotTexture = null, Texture2D selectionTexture = null, SpriteFont font = null, float slotSpacing = 4f)
        {
            this.position = position;
            this.cellWidth = cellWidth;
            this.cellHeight = cellWidth;
            this.totalSlots = totalSlots;
            this.slotSpacing = slotSpacing;
            this.slotTexture = slotTexture;
            this.selectionTexture = selectionTexture;
            this.font = font;

            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            // temporary(?) keyboard controls for selection cycling
            var keyboardState = Keyboard.GetState();
            if (cooldownTimer <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.I) || keyboardState.IsKeyDown(Keys.Right))
                {
                    Next();
                }
                else if (keyboardState.IsKeyDown(Keys.U) || keyboardState.IsKeyDown(Keys.Left))
                {
                    Previous();
                }
            }
        }

        public void Next()
        {
            if (cooldownTimer > 0) return;
            selectedIndex = (selectedIndex + 1) % totalSlots;
            cooldownTimer = CooldownDuration;
        }

        public void Previous()
        {
            if (cooldownTimer > 0) return;
            selectedIndex = (selectedIndex - 1 + totalSlots) % totalSlots;
            cooldownTimer = CooldownDuration;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var inventory = ItemInventory.Instance;

            for (int i = 0; i < totalSlots; i++)
            {
                Vector2 slotPos = new Vector2(
                    position.X + (i * (cellWidth + slotSpacing)), 
                    position.Y
                );

                Rectangle rect = new Rectangle((int)slotPos.X, (int)slotPos.Y, cellWidth, cellHeight);

                // Slot background + highlight
                if (slotTexture != null)
                {
                    spriteBatch.Draw(slotTexture, rect, Color.White);
                    if (i == selectedIndex && selectionTexture != null)
                    {
                        spriteBatch.Draw(selectionTexture, rect, Color.Yellow * 0.8f);
                    }
                }
                else
                {
                    Color fillColor = (i == selectedIndex) ? Color.White * 0.5f : Color.Black * 0.8f;
                    spriteBatch.Draw(pixel, rect, fillColor);
                }

                // Pull items from inventory dictionary, picked-up items show up in slots
                if (inventory.items != null && i < inventory.items.Count)
                {
                    var kvp = inventory.items.ElementAt(i);
                    IItem item = kvp.Key;
                    int quantity = kvp.Value;

                    // Draw item icon
                    if (item != null && item.Icon != null)
                    {
                        Vector2 iconPos = slotPos + new Vector2(cellWidth / 2f, cellHeight / 2f);
                        item.Icon.Draw(spriteBatch, iconPos);
                    }

                    // Stack quantity text
                    if (font != null && quantity > 1)
                    {
                        Vector2 textPosition = slotPos + new Vector2(cellWidth - 18, cellHeight - 22);
                        spriteBatch.DrawString(font, quantity.ToString(), textPosition, Color.White);
                    }
                }
            }
        }
    }
}
