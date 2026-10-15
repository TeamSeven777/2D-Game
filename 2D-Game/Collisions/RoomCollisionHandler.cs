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
        private List<Rectangle> temporaryRectangles;
        private Timer tempRectRefreshTimer = new Timer(.1);
        private static RoomCollisionHandler instance = new RoomCollisionHandler();

        public static RoomCollisionHandler Instance
        { get { return instance; } }

        private RoomCollisionHandler() { }

        public void Initialize()
        {
            dynamicHitboxes = new Dictionary<IHitboxOwner, IHitbox>();
            staticHitboxes = new Dictionary<IHitboxOwner, IHitbox>();
            temporaryRectangles = new List<Rectangle>();
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
        public List<Rectangle> GetRectangles() { 
            return temporaryRectangles;
        }
        public void AddTempRectangle(Rectangle rectangle)
        {
            if(!temporaryRectangles.Contains(rectangle)) temporaryRectangles.Add(rectangle);
        }
        private void DeleteTemps()
        {
            temporaryRectangles = new List<Rectangle>();
        }
        public void Add(IHitboxOwner hitboxOwner, bool dynamic)
        {
            if (dynamic && !dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes.Add(hitboxOwner, hitboxOwner.Hitbox);
            else if(!staticHitboxes.ContainsKey(hitboxOwner)) staticHitboxes.Add(hitboxOwner, hitboxOwner.Hitbox);
        }
        /*
         * Update a specific dynamic Hitbox
         */
        public void Update(IHitboxOwner hitboxOwner)
        {
            if (dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes[hitboxOwner] = hitboxOwner.Hitbox;            
        }

        /*
         * Update all dynamic hitboxes
         */
        public void Update(GameTime gameTime)
        {
            foreach (IHitboxOwner hbOwner in dynamicHitboxes.Keys)
            {
                dynamicHitboxes[hbOwner] = hbOwner.Hitbox;
            }
            tempRectRefreshTimer.Update(gameTime);
        }
        public void Remove(IHitboxOwner hitboxOwner)
        {
            if (dynamicHitboxes.ContainsKey(hitboxOwner)) dynamicHitboxes.Remove(hitboxOwner);
            else if(staticHitboxes.ContainsKey(hitboxOwner)) staticHitboxes.Remove(hitboxOwner);
        }
        public void HandleCollision()
        {
            if (tempRectRefreshTimer.Ringing) {
                DeleteTemps();
                tempRectRefreshTimer.Reset();
            }
            foreach(IHitboxOwner hbOwner in dynamicHitboxes.Keys)
            {
                foreach(IHitboxOwner hbOwner2 in dynamicHitboxes.Keys)
                {
                    if (!hbOwner.Equals(hbOwner2)) hbOwner.CollisionHandler.HandleCollision(hbOwner2.Hitbox);
                }
                foreach(IHitboxOwner staticHbOwner in staticHitboxes.Keys)
                {
                    hbOwner.CollisionHandler.HandleCollision(staticHbOwner.Hitbox);
                }
            }
            //TODO
            //call all collision methods and checks from here.
        }


    }
}
