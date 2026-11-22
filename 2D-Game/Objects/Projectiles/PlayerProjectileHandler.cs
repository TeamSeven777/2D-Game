using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class PlayerProjectileHandler : IProjectileHandler
    {
        public List<IHitboxOwner> InActiveProjectiles { get; set; }
        public List<IHitboxOwner> ActiveProjectiles { get; set; }
        public GameTime GameTime { get; set; }
        public DefaultPlayer playerRef;
        public float Cooldown = 0;
        public int RemoveCount = 0;
        private static PlayerProjectileHandler instance = new PlayerProjectileHandler();

        public static PlayerProjectileHandler Instance
        { get { return instance; } }
        public PlayerProjectileHandler()
        {
        }

        public void Initialize(ref DefaultPlayer player)
        {
            ActiveProjectiles = new List<IHitboxOwner>();
            playerRef = player;

        }

        public void Update(GameTime gt)
        {
            for (int i = 0; i < ActiveProjectiles.Count; i++)
            {
                IProjectile curProjectile = (IProjectile)ActiveProjectiles[i];
                curProjectile.Update(gt);
                curProjectile.ProjectileSprite.Update(gt);
                if (curProjectile.DespawnTimer <= 0) this.Remove(ActiveProjectiles[i]);
            }
            if (Cooldown > 0) Cooldown--;
        }

        public void Draw(SpriteBatch spr)
        {
            foreach(IProjectile projectile in ActiveProjectiles)
            {
                projectile.Draw(spr);
            }
        }


        public void Add(IProjectile projectile)
        {
            Vector2 TravelingDirection;
                            switch (playerRef.stateMachine.direction)
                            {
                                case (Direction.Down):
                                    TravelingDirection = new Vector2(0, -10);
                                    break;
                                case (Direction.Left):
                                    TravelingDirection = new Vector2(-10,0);   
                                    break;
                                case (Direction.Right):
                                    TravelingDirection = new Vector2(10,0);
                                    break;
                                case (Direction.Up):
                                    TravelingDirection = new Vector2(0, 10);
                                    break;
                                default:
                                    TravelingDirection = new Vector2(0, 0);
                                    break;
                            }
            projectile.TravelingDirection = TravelingDirection;
            ActiveProjectiles.Add((IHitboxOwner)projectile);
            Cooldown = 10;
        }

                

        public void Remove(IHitboxOwner x)
        {
            if (ActiveProjectiles.Count > 0 && (ActiveProjectiles.IndexOf(x) >= 0))
            {
                RoomCollisionHandler.Instance.Remove(x);
                ActiveProjectiles.Remove(x);
            }
        }

        public void Clear()
        {
            if(ActiveProjectiles.Count > 0) ActiveProjectiles.Clear();
        }
    }
}
