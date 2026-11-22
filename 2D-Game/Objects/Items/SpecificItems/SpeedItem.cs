using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
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