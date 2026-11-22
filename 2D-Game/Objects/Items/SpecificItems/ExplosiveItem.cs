using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
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
}