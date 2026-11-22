using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class KeyItem : Item
    {
        public KeyItem(Sprite icon) : base("Key", icon, 99) { }
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
            SoundEffectFactory.Instance.PlaySoundEffect(SoundName.PuzzleSolve1, false);
        }
        public override void Use()
        {
            ItemInventory.Instance.ChangeValue("Key", -1);
        }
    }

}