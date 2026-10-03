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

        public PlayerProjectileHandler(ref DefaultPlayer player, Texture2D newSprite)
        {
            ActiveProjectiles = new Queue<IProjectile>();
            CurProjectileSprite = new ISprite[4];
            playerRef = player;
            for (int i = 0; i < 4; i++)
            {
                CurProjectileSprite[i] = new Sprite(newSprite, Vector2.Zero, new Vector2(4.0f), Color.White, new Rectangle(i * 16, 0, 16, 16));
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
                switch (playerRef.stateMachine.direction)
                {
                    case (Direction.Down):
                        ActiveProjectiles.Enqueue(new PlayerProjectile(playerRef.Position, new Vector2(0, 10), CurProjectileSprite[0]));
                        Cooldown = 10;
                        break;
                    case (Direction.Left):
                        ActiveProjectiles.Enqueue(new PlayerProjectile(playerRef.Position, new Vector2(-10, 0), CurProjectileSprite[2]));
                        Cooldown = 10;
                        break;
                    case (Direction.Right):
                        ActiveProjectiles.Enqueue(new PlayerProjectile(playerRef.Position, new Vector2(10, 0), CurProjectileSprite[3]));
                        Cooldown = 10;
                        break;
                    case (Direction.Up):
                        ActiveProjectiles.Enqueue(new PlayerProjectile(playerRef.Position, new Vector2(0, -10), CurProjectileSprite[1]));
                        Cooldown = 10;
                        break;

                }
            }

        }

        public void Remove()
        {
            if(ActiveProjectiles.Count > 0) ActiveProjectiles.Dequeue();
        }

        public void Clear()
        {
            if(ActiveProjectiles.Count > 0) ActiveProjectiles.Clear();
        }
    }
}
