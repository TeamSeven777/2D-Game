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


        public void Add()
        {
            if (Cooldown == 0)
            {
                switch (playerRef.CurrentRangedWeapon)
                {
                    
                    case (Tool.Slingshot):
                        if(WeaponInventory.Instance.GetAmmo(Tool.Slingshot) != 0) {
                            switch (playerRef.stateMachine.direction)
                            {
                                case (Direction.Down):
                                    ActiveProjectiles.Add(new PlayerProjectilePebble(playerRef.Position, new Vector2(0, 10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Left):
                                    ActiveProjectiles.Add(new PlayerProjectilePebble(playerRef.Position, new Vector2(-10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Right):
                                    ActiveProjectiles.Add(new PlayerProjectilePebble(playerRef.Position, new Vector2(10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Up):
                                    ActiveProjectiles.Add(new PlayerProjectilePebble(playerRef.Position, new Vector2(0, -10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                            }    

                        }
                        break;
                    case (Tool.Dynamite):
                        if (WeaponInventory.Instance.GetAmmo(Tool.Dynamite) != 0) {
                            switch (playerRef.stateMachine.direction)
                            {
                                case (Direction.Down):
                                    ActiveProjectiles.Add(new PlayerProjectileDynamite(playerRef.Position, new Vector2(0, 10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Left):
                                    ActiveProjectiles.Add(new PlayerProjectileDynamite(playerRef.Position, new Vector2(-10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Right):
                                    ActiveProjectiles.Add(new PlayerProjectileDynamite(playerRef.Position, new Vector2(10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Up):
                                    ActiveProjectiles.Add(new PlayerProjectileDynamite(playerRef.Position, new Vector2(0, -10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;

                            }
                        }
                        break;
                    case (Tool.Boomerang):
                        if (WeaponInventory.Instance.GetAmmo(Tool.Boomerang) != 0)
                        {
                            switch (playerRef.stateMachine.direction)
                            {
                                case (Direction.Down):
                                    ActiveProjectiles.Add(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(0, 10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Left):
                                    ActiveProjectiles.Add(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(-10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Right):
                                    ActiveProjectiles.Add(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(10, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;
                                case (Direction.Up):
                                    ActiveProjectiles.Add(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(0, -10), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(playerRef.CurrentRangedWeapon)));
                                    Cooldown = 10;
                                    break;


                            }
                        }
                            break;

                }
            }

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
