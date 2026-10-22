using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class ItemInventory
    {
        private readonly Dictionary<IItem, int> items = new Dictionary<IItem, int>();
        private int currentIndex = 0;
        private Room roomRef;
        public static ItemInventory instance = new ItemInventory();
        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches
        public static ItemInventory Instance
        { get { return instance; } }

        // SpriteFactory for all items
        public void LoadContent(ContentManager content, Room room)
        {
            roomRef = room;
            // Add all items to base inventory here
            // For sprint 3, start player with one of each object:
            items.Add(new LogItem(new Sprite(content.Load<Texture2D>("Images/Log2"))),99);
            items.Add(new BerryItem(new Sprite(content.Load<Texture2D>("Images/Berry2"))),99);
            items.Add(new KeyItem(new Sprite(content.Load<Texture2D>("Images/Key2"))),99);
            items.Add(new ExplosiveItem(new Sprite(content.Load<Texture2D>("Images/Explosive2"))),99);
            items.Add(new PebbleItem(new Sprite(content.Load<Texture2D>("Images/Pebble2"))),99);
        }

        public void Update(GameTime gameTime, Room room)
        {
            roomRef = room;
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Add(IItem item, int amount)
        {
            if (!items.ContainsKey(item))
                items.Add(item, amount);
            else
                items[item] += amount;
        }

        public void Remove(IItem item)
        {
            items.Remove(item);
        }

        public void RemoveFromWorld(WorldItem worldItem)
        {
            if(roomRef != null)
                roomRef.Items.Remove(worldItem);
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

    }
}
