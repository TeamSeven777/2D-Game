using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            
        }

        public void Next() => ItemInventory.Instance.Next();
        public void Previous() => ItemInventory.Instance.Previous();

        public void Draw(SpriteBatch spriteBatch)
        {
            var inventory = ItemInventory.Instance;
            int selectedIndex = inventory.GetSelectedIndex();

            for (int i = 0; i < totalSlots; i++)
            {
                Vector2 slotPos = new Vector2(
                    position.X + (i * (cellWidth + slotSpacing)), 
                    position.Y
                );

                Rectangle rect = new Rectangle((int)slotPos.X, (int)slotPos.Y, cellWidth, cellHeight);

                // Slot Background & Highlight based on global inventory selection
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

                // Draw items from inventory dictionary
                if (inventory.items != null && i < inventory.items.Count)
                {
                    var kvp = inventory.items.ElementAt(i);
                    IItem item = kvp.Key;
                    int quantity = kvp.Value;

                    if (item != null && item.Icon != null)
                    {
                        Vector2 iconPos = slotPos + new Vector2(cellWidth / 2f, cellHeight / 2f);
                        item.Icon.Draw(spriteBatch, iconPos);
                    }

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
