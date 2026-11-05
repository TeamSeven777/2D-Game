using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class ItemInventory
    {
        public readonly Dictionary<IItem, int> items = new Dictionary<IItem, int>();
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
            if (items.Count == 0)
            {
                items.Add(new LogItem(new Sprite(content.Load<Texture2D>("Images/Log2"), new Rectangle(0, 0, 32, 32))), 99);
                items.Add(new BerryItem(new Sprite(content.Load<Texture2D>("Images/Berry2"), new Rectangle(0,0,32,32))), 99);
                items.Add(new KeyItem(new Sprite(content.Load<Texture2D>("Images/Key2"), new Rectangle(0, 0, 32, 32))), 99);
                items.Add(new ExplosiveItem(new Sprite(content.Load<Texture2D>("Images/Explosive2"), new Rectangle(0, 0, 32, 32))), 99);
                items.Add(new PebbleItem(new Sprite(content.Load<Texture2D>("Images/Pebble2"), new Rectangle(0, 0, 32, 32))), 99);
                items.Add(new SpeedItem(new Sprite(content.Load<Texture2D>("Images/speedshoe"), new Rectangle(0, 0, 32, 32))), 99);
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

        public void Add(IItem item, int amount)
        {
            //Big nono!
            IItem toAddTo = null;
            foreach (KeyValuePair<IItem, int> x in items)
            {
                if(x.Key.Name == item.Name)
                {
                    toAddTo = x.Key;
                }
            }
            if (toAddTo != null)
            {
                items[toAddTo] += amount;
            }
            else
            {
                items.Add(toAddTo, amount);
            }
        }

        public void Remove(string item, int amount)
        {
            IItem toRemove = null;
            foreach (KeyValuePair<IItem, int> x in items)
            {
                if (x.Key.Name == item)
                {
                    toRemove = x.Key;
                }
            }
            if (items.ContainsKey(toRemove))
            {
                items.Remove(toRemove);
            }

        }

        public int GetItemCount(string item)
        {
            foreach(KeyValuePair<IItem,int> x in items)
            {
                if (x.Key.Name.Equals(item))
                {
                    return x.Value;
                }
            }
            return 0;
        }

        public List<IItem> GetKeys()
        {
            List<IItem> keys = new List<IItem>();
            foreach(IItem x in items.Keys)
            {
                keys.Add(x);
            }

            return keys;
        }

        public void RemoveFromWorld(WorldItem worldItem)
        {
            if(roomRef != null)
                roomRef.Items.Remove(worldItem);
        }


    }
}
