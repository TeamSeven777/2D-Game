using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class ItemInventory
    {
        private readonly Dictionary<string, int> items = new Dictionary<string, int>();
        //private int currentIndex = 0;
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
            items.Clear();
            if (items.Count == 0)
            {
                items.Add("LogItem", 99);
                items.Add("BerryItem", 99);
                items.Add("KeyItem", 99);
                items.Add("ExplosiveItem", 99);
                items.Add("PebbleItem", 99);
            }
        }

        public void Update(GameTime gameTime, Room room)
        {
            roomRef = room;
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Add(string item, int amount)
        {
            if (!items.ContainsKey(item))
                items.Add(item, amount);
            else
                items[item] += amount;
        }

        public void Remove(string item, int amount)
        {
            if (items[item] - amount >= 0)
            {
                items[item] = items[item] - amount;
            }
            else
            {
                items[item] = 0;
            }
        }

        public int GetItemCount(string item)
        {
            if (items.ContainsKey(item))
                return items[item];
            else
                return 0;
        }

        public void RemoveFromWorld(WorldItem worldItem)
        {
            if(roomRef != null)
                roomRef.Items.Remove(worldItem);
        }


    }
}
