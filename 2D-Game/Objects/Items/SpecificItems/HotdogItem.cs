using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class HotdogItem : Item
    {
        public HotdogItem(Sprite icon) : base("Hotdog", icon, 99) { }
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            DefaultPlayer.Instance.Heal(7);
            SoundEffectFactory.Instance.PlaySoundEffect(SoundName.EatBerry, true);
            DefaultPlayer.Instance.stateMachine.CurrPlayerState.PlayerSprite.Color = Color.LimeGreen;
            ItemInventory.Instance.ChangeValue("Hotdog", -1);
        }
        
    }
}