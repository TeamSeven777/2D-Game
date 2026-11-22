using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
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
}