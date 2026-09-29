using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public Texture2D Icon { get; set; }
        public int MaxStackSize { get; set; } = 99;
        public int CurrentStack { get; set; } = 1;
        
        public bool IsStackable => MaxStackSize > 1;

        public Item(string name, Texture2D icon, int maxStackSize = 99)
        {
            Name = name;
            Icon = icon;
            MaxStackSize = maxStackSize;
        }

        public virtual void Use()
        {
            // Base usage logic here
        }
    }
}
