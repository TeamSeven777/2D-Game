using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class ItemCollisionHandler : ICollisionHandler
    {
        WorldItem item;

        public ItemCollisionHandler(WorldItem item)
        {
            this.item = item;
        }

        public void HandleCollision(IHitboxOwner hbOwner)
        {
            HashSet<Side> resultsOfIsCollide = item.Hitbox.IsCollide(hbOwner.Hitbox);
            if (!resultsOfIsCollide.Contains(Side.None) && hbOwner.Hitbox.Type == HitBoxType.Player)
            {
                item.ItemData.Collect();
                SoundEffectFactory.Instance.PlaySoundEffect(SoundName.Pickup, true);
                ItemInventory.Instance.RemoveFromWorld(item);
                item.IsCollected = true;
                RoomCollisionHandler.Instance.Remove(item);
            }

        }
    }
}
