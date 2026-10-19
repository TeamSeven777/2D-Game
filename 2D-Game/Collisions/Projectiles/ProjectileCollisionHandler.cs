using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class ProjectileCollisionHandler : ICollisionHandler 
    {
        IProjectile projectile;

        public ProjectileCollisionHandler(IProjectile thing)
        {
            projectile = thing;
        }

        public void HandleCollision(IHitboxOwner hbOwner)
        {
            IHitbox collider = hbOwner.Hitbox;
            HashSet<Side> resultOfCollision = projectile.Hitbox.IsCollide(collider);
            if (!resultOfCollision.Contains(Side.None) && resultOfCollision.Count > 0)
            {
                if (projectile.Hitbox.Type == HitBoxType.PlayerAttack)
                {
                    if (collider.Type == HitBoxType.Enemy || collider.Type == HitBoxType.Block)
                        PlayerProjectileHandler.Instance.Remove();
                }
                else if (projectile.Hitbox.Type == HitBoxType.EnemyAttack)
                {
                    if (collider.Type == HitBoxType.Player || collider.Type == HitBoxType.Block)
                        PlayerProjectileHandler.Instance.Remove();
                }
            }
        }
    }
}
