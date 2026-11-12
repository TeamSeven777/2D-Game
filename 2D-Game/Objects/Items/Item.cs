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

// Specific item subclasses:

    // Log
    public class LogItem : Item
    {
        public LogItem(Sprite icon) : base("Log", icon, 99) { }

        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }

        public override void Use()
        {
            ItemInventory.Instance.ChangeValue("Log", -1);
        }
    }

    // Berry
    public class BerryItem : Item
    {
        public BerryItem(Sprite icon) : base("Berry", icon, 99) { }
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            DefaultPlayer.Instance.Health += 5;
            SoundEffectFactory.Instance.PlaySoundEffect(SoundName.EatBerry, true);
            DefaultPlayer.Instance.stateMachine.CurrPlayerState.PlayerSprite.Color = Color.LimeGreen;
            ItemInventory.Instance.ChangeValue("Berry", -1);
        }
        
    }

    // Key
    public class KeyItem : Item
    {
        public KeyItem(Sprite icon) : base("Key", icon, 99) { }
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            ItemInventory.Instance.ChangeValue("Key", -1);
        }
    }

    // Explosive
    public class ExplosiveItem : Item
    {
        public ExplosiveItem(Sprite icon) : base("Explosive", icon, 99) { }
        public override void Collect()
        {
            WeaponInventory.Instance.ChangeAmmo(Tool.Dynamite, 10);
        }
        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }

    // Pebble
    public class PebbleItem : Item
    {
        public PebbleItem(Sprite icon) : base("Pebble", icon, 99) { }
        public override void Collect()
        {
            WeaponInventory.Instance.ChangeAmmo(Tool.Slingshot,10);
        }
        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SpeedItem : Item
    {
        public SpeedItem(Sprite icon) : base("SpeedShoe", icon, 99) { }
        public override void Collect()
        {
            DefaultPlayer.Instance.WalkSpeed += 2;
        }
        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }

}