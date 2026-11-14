using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class PlayerDetectorCollisionHandler : ICollisionHandler
    {
        PlayerDetector detector;
        private Timer toggleDelay;

        public PlayerDetectorCollisionHandler(PlayerDetector det)
        {
            detector = det;
            toggleDelay = new Timer(1);
        }

        public void HandleCollision(IHitboxOwner hbOwner)
        {
            HashSet<Side> resultsOfIsCollide = detector.Hitbox.IsCollide(hbOwner.Hitbox);
            if (!resultsOfIsCollide.Contains(Side.None) && hbOwner.Hitbox.Type == HitBoxType.Player)
            {
                detector.PlayerDetected = true;
            }
            else 
            {
                //you must do something with the detector in its context AND then reset PlayerDetected to false (otherwise it will be true for the rest of time)
            }

        }
    }
}
