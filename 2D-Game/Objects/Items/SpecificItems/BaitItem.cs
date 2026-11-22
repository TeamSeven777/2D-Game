using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    // Fishing Bait - mosquito or spider guts?
    public class BaitItem : Item
    {
        public BaitItem(Sprite icon) : base("Bait", icon, 99) { }//change
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            //used in conjunction with fishing rod over water to fish
        }

    }
}