using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;

namespace _2D_Game
{
    internal class InteractionCollisionHandler : ICollisionHandler
    {
        InteractTool interactTool;

        public InteractionCollisionHandler(InteractTool iT)
        {
            interactTool = iT;
        }
        public void HandleCollision(IHitboxOwner hbOwner)
        {
            IHitbox collider = hbOwner.Hitbox;
            Rectangle temp = Rectangle.Empty;
            //HashSet<Side> resultsOfWillCollide = interactTool.Hitbox.WillCollide(collider);//fix to use willcollide for raycasting,
            HashSet<Side> resultsOfIsCollide = interactTool.Hitbox.IsCollide(collider);
            //First determines if a collision will happen
            if (!interactTool.hasInteracted && !resultsOfIsCollide.Contains(Side.None))
            {
                //interactTool.Hitbox.BoundingBox = 
                //Then how the collision should be handled
                switch (collider.Type)
                {
                    case (HitBoxType.Block):
                        if (hbOwner is IInteractable interactedObj)
                        {
                            interactTool.hasInteracted = true;
                            interactedObj.HandleInteraction();
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
