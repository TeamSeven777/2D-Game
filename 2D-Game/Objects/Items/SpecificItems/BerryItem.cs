using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class BerryItem : Item
    {
        public BerryItem(Sprite icon) : base("Berry", icon, 99) { }
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            DefaultPlayer.Instance.Heal(5);
            SoundEffectFactory.Instance.PlaySoundEffect(SoundName.EatBerry, true);
            DefaultPlayer.Instance.stateMachine.CurrPlayerState.PlayerSprite.Color = Color.LimeGreen;
            ItemInventory.Instance.ChangeValue("Berry", -1);
        }
        
    }
}