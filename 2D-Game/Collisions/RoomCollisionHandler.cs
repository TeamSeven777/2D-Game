using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class RoomCollisionHandler
    {

        private Dictionary<IHitboxOwner, IHitbox>  dynamicHitboxes;
        private Dictionary<IHitboxOwner, IHitbox> staticHitboxes;
        private static RoomCollisionHandler instance = new RoomCollisionHandler();

        public static RoomCollisionHandler Instance
        { get { return instance; } }

        private RoomCollisionHandler() { }

        public void Initialize()
        {
            dynamicHitboxes = new Dictionary<IHitboxOwner, IHitbox>();
            staticHitboxes = new Dictionary<IHitboxOwner, IHitbox>();
        }
        public List<IHitbox> GetHitboxes()
        {
            List<IHitbox> hitboxList = new List<IHitbox>();
            foreach (var hitbox in dynamicHitboxes.Values)
            {
                hitboxList.Add(hitbox);
            }
            foreach (var hitbox in staticHitboxes.Values) {
                hitboxList.Add(hitbox);
            }
            return hitboxList;
        }
        public void Add(IHitboxOwner hitboxOwner, bool dynamic)
        {
            if (dynamic && !dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes.Add(hitboxOwner, hitboxOwner.Hitbox);
            else if(!staticHitboxes.ContainsKey(hitboxOwner)) staticHitboxes.Add(hitboxOwner, hitboxOwner.Hitbox);
        }
        public void Update(IHitboxOwner hitboxOwner)
        {
            if (dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes[hitboxOwner] = hitboxOwner.Hitbox;
        }
        public void Remove(IHitboxOwner hitboxOwner)
        {
            if (dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes.Remove(hitboxOwner);
            else if(staticHitboxes.ContainsKey(hitboxOwner)) staticHitboxes.Remove(hitboxOwner);
        }
        public void HandleCollision()
        {
            //TODO
            //call all collision methods and checks from here.
        }


    }
}
