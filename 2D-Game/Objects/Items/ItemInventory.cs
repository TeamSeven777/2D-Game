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
        private readonly Vector2 drawPosition = new Vector2(300, 200);

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// SpriteFactory for all items
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            // Add all items here
            items.Add(new Item("Log", new Sprite(content.Load<Texture2D>("Images/Log")), 10));
            items.Add(new Item("Berry", new Sprite(content.Load<Texture2D>("Images/Berry")), 5));
            items.Add(new Item("Key", new Sprite(content.Load<Texture2D>("Images/Key")), 1));
            items.Add(new Item("Explosive", new Sprite(content.Load<Texture2D>("Images/Explosive")), 1));
            items.Add(new Item("Pebble", new Sprite(content.Load<Texture2D>("Images/Pebble")), 10));
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
