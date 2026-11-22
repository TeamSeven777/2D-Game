using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    // Base Item Class
    public abstract class Item : IItem
    {
        public string Name { get; set; }
        public Sprite Icon { get; set; }
        public int MaxStackSize { get; set; } = 99;
        public int CurrentStack { get; set; } = 1;
        public bool IsStackable => MaxStackSize > 1;
        

        protected Item(string name, Sprite icon, int maxStackSize)
        {
            Name = name;
            Icon = icon;
            MaxStackSize = maxStackSize;
        }

        // Abstract so subclasses (specific item types) can define custom behavior
        public abstract void Use();
        public abstract void Collect();
    }
}