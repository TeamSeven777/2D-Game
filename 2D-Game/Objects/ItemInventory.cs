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
        private readonly Vector2 drawPosition = new Vector2(600, 200);

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        public void LoadContent(ContentManager content)
        {
            // Add all items here
            items.Add(new Item("Log", content.Load<Texture2D>("images/Log")));
            items.Add(new Item("Berry", content.Load<Texture2D>("images/Berry")));
            items.Add(new Item("Key", content.Load<Texture2D>("images/Key")));
            items.Add(new Item("Explosive", content.Load<Texture2D>("images/Explosive")));
            items.Add(new Item("Pebble", content.Load<Texture2D>("images/Pebble")));
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
                spriteBatch.Draw(currentItem.Icon, drawPosition, Color.White);
            }
        }
    }
}
