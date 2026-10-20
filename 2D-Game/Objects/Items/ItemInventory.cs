using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class ItemInventory
    {
        private readonly List<IItem> items = new();
        private int currentIndex = 0;
        private readonly Vector2 drawOffset = new Vector2(300, 200);
        private Vector2 drawPosition;
        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        // SpriteFactory for all items
        public void LoadContent(ContentManager content, Vector2 roomOrigin)
        {
            drawPosition = roomOrigin + drawOffset;
            
            // Add all items to base inventory here
            // For sprint 3, start player with one of each object:
            items.Add(new LogItem(new Sprite(content.Load<Texture2D>("Images/Log2"))));
            items.Add(new BerryItem(new Sprite(content.Load<Texture2D>("Images/Berry2"))));
            items.Add(new KeyItem(new Sprite(content.Load<Texture2D>("Images/Key2"))));
            items.Add(new ExplosiveItem(new Sprite(content.Load<Texture2D>("Images/Explosive2"))));
            items.Add(new PebbleItem(new Sprite(content.Load<Texture2D>("Images/Pebble2"))));
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Next()
        {
            if (items.Count == 0 || cooldownTimer > 0) return;
            
            currentIndex = (currentIndex + 1) % items.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }

        public void Previous()
        {
            if (items.Count == 0 || cooldownTimer > 0) return;

            currentIndex = (currentIndex - 1 + items.Count) % items.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (items.Count == 0) return;

            IItem currentItem = items[currentIndex];
            if (currentItem.Icon != null)
            {
                spriteBatch.Draw(currentItem.Icon.Content, drawPosition, Color.White);
            }
        }
    }
}
