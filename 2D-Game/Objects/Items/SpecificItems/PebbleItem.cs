using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
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
}