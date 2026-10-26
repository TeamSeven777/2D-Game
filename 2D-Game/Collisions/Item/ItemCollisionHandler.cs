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
                ItemInventory.instance.Add(item.ItemData, 1);
                ItemInventory.instance.RemoveFromWorld(item);
                RoomCollisionHandler.Instance.Remove(item);
            }

        }
    }
}
