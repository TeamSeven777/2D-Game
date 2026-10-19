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
        public Queue<IProjectile> ActiveProjectiles { get; set; }
        public GameTime GameTime { get; set; }
        public DefaultPlayer playerRef;
        public ISprite[] CurProjectileSprite { get; set; }
        public float Cooldown = 0;
        public int RemoveCount = 0;
        private static PlayerProjectileHandler instance = new PlayerProjectileHandler();

        public static PlayerProjectileHandler Instance
        { get { return instance; } }
        public PlayerProjectileHandler()
        {
        }

        public void Initialize(ref DefaultPlayer player, Texture2D newSprite)
        {
            ActiveProjectiles = new Queue<IProjectile>();
            CurProjectileSprite = new ISprite[4];
            playerRef = player;
            //This will change to rotating sprite at some point
            //CurProjectileSprite[0] = new Sprite(newSprite, Vector2.One, new Rectangle(0, 0, 64, 64), SpriteEffects.None, 0, 360, 2, true, true);
            CurProjectileSprite[0] = new Sprite(newSprite, Vector2.Zero, new Vector2(1), Color.White, new Rectangle(0, 0, 64, 64));
            for (int i = 1; i < 3; i++)
            {
                //This will be repurposed for switching between the different types
                CurProjectileSprite[i] = new Sprite(newSprite, Vector2.Zero, new Vector2(1), Color.White, new Rectangle(i * 64, 0, 64, 64));
            }

        }

        public void Update(GameTime gt)
        {
            foreach (IProjectile projectile in ActiveProjectiles)
            {
                projectile.Update(gt);
                if (projectile.DespawnTimer <= 0) RemoveCount++;
            }
            if (Cooldown > 0) Cooldown--;
            for (int i = 0; i < RemoveCount; i++) this.Remove();
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


                        switch (playerRef.stateMachine.direction)
                        {
                    case (Direction.Down):
                        ActiveProjectiles.Enqueue(new PlayerProjectilePebble(playerRef.Position, new Vector2(0, 10), CurProjectileSprite[1]));
                        Cooldown = 10;
                        break;
                    case (Direction.Left):
                        ActiveProjectiles.Enqueue(new PlayerProjectilePebble(playerRef.Position, new Vector2(-10, 0), CurProjectileSprite[1]));
                        Cooldown = 10;
                        break;
                    case (Direction.Right):
                        ActiveProjectiles.Enqueue(new PlayerProjectilePebble(playerRef.Position, new Vector2(10, 0), CurProjectileSprite[1]));
                        Cooldown = 10;
                        break;
                    case (Direction.Up):
                        ActiveProjectiles.Enqueue(new PlayerProjectilePebble(playerRef.Position, new Vector2(0, -10), CurProjectileSprite[1]));
                        Cooldown = 10;
                        break;
                                

                        }
                        break;
                    case (Tool.Dynamite):
                        switch (playerRef.stateMachine.direction)
                        {
                            case (Direction.Down):
                                ActiveProjectiles.Enqueue(new PlayerProjectileDynamite(playerRef.Position, new Vector2(0, 10), CurProjectileSprite[2]));
                                Cooldown = 10;
                                break;
                            case (Direction.Left):
                                ActiveProjectiles.Enqueue(new PlayerProjectileDynamite(playerRef.Position, new Vector2(-10, 0), CurProjectileSprite[2]));
                                Cooldown = 10;
                                break;
                            case (Direction.Right):
                                ActiveProjectiles.Enqueue(new PlayerProjectileDynamite(playerRef.Position, new Vector2(10, 0), CurProjectileSprite[2]));
                                Cooldown = 10;
                                break;
                            case (Direction.Up):
                                ActiveProjectiles.Enqueue(new PlayerProjectileDynamite(playerRef.Position, new Vector2(0, -10), CurProjectileSprite[2]));
                                Cooldown = 10;
                                break;


                        }
                        break;
                    case (Tool.Boomerang):
                        switch (playerRef.stateMachine.direction)
                        {
                            case (Direction.Down):
                                ActiveProjectiles.Enqueue(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(0, 10), CurProjectileSprite[0]));
                                Cooldown = 10;
                                break;
                            case (Direction.Left):
                                ActiveProjectiles.Enqueue(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(-10, 0), CurProjectileSprite[0]));
                                Cooldown = 10;
                                break;
                            case (Direction.Right):
                                ActiveProjectiles.Enqueue(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(10, 0), CurProjectileSprite[0]));
                                Cooldown = 10;
                                break;
                            case (Direction.Up):
                                ActiveProjectiles.Enqueue(new PlayerProjectileBoomerang(playerRef.Position, new Vector2(0, -10), CurProjectileSprite[0]));
                                Cooldown = 10;
                                break;


                        }
                        break;

                }
            }

        }

        public void Remove()
        {
            if (ActiveProjectiles.Count > 0 && (ActiveProjectiles.Dequeue() is IHitboxOwner hbOwner))
            {
                RoomCollisionHandler.Instance.Remove(hbOwner);
            }
        }

        public void Clear()
        {
            if(ActiveProjectiles.Count > 0) ActiveProjectiles.Clear();
        }
    }
}
