using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_Game
{
    public class ItemInventory
    {
       public Dictionary<IItem, int> items = new Dictionary<IItem, int>();
        private Room roomRef;
        private int selectedIndex = 0;
        private int totalSlots = 7;
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2;

        public static ItemInventory instance = new ItemInventory();
        public static ItemInventory Instance
        { get { return instance; } }

        public void Update(GameTime gameTime, Room room)
        {
            roomRef = room;
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (cooldownTimer > 0)
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();
            if (cooldownTimer <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.I))
                {
                    Next();
                }
                else if (keyboardState.IsKeyDown(Keys.U))
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

        public int GetSelectedIndex() => selectedIndex;

        // Returns type of object that UI is highlighting in hotbar
        public IItem GetSelectedItem()
        {
            if (items != null && selectedIndex >= 0 && selectedIndex < items.Count)
            {
                return items.ElementAt(selectedIndex).Key;
            }
            return null;
        }


        // SpriteFactory for all items
        public void LoadContent(ContentManager content, Room room)
        {
            roomRef = room;
            // Add any items to base inventory here if desired
            
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
                items.Add(item, amount);
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
