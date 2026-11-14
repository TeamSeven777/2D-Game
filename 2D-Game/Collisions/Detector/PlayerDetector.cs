using _2D_Game;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerDetector : IHitboxOwner
{
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public bool PlayerDetected { get; set; } = false; //set to true when player is in hitbox

        public PlayerDetector(Rectangle hbRectangle) {
            Hitbox = new PlayerDetectorHitbox(hbRectangle);
            CollisionHandler = new PlayerDetectorCollisionHandler(this);
            RoomCollisionHandler.Instance.Add(this, true); // the zone must always check if the player is in its hitbox to toggle its boolean
        }
    }
}
