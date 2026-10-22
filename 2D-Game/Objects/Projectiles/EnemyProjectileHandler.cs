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
    public class EnemyProjectileHandler : IProjectileHandler
    {
        public Queue<IProjectile> ActiveProjectiles { get; set; }
        public GameTime GameTime { get; set; }
        public EnemyManager enemiesRef;
        public DefaultPlayer targetRef;
        public ISprite[] CurProjectileSprite { get; set; }
        public float Cooldown = 0;
        public int RemoveCount = 0;
        private static EnemyProjectileHandler instance = new EnemyProjectileHandler();

        public static EnemyProjectileHandler Instance
        { get { return instance; } }
        public EnemyProjectileHandler()
        {
        }

        public void Initialize(ref EnemyManager enemies, ref DefaultPlayer playerRef, Texture2D newSprite)
        {
            ActiveProjectiles = new Queue<IProjectile>();
            CurProjectileSprite = new ISprite[4];
            enemiesRef = enemies;
            this.targetRef = playerRef;
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

        }
        public void Add(IEnemy enemy)
        {
            switch (enemy)
            {
                case (Mosquito):
                    ActiveProjectiles.Enqueue(new MosquitoProjectile(enemy.Position, new Vector2((int)targetRef.Position.X, (int)targetRef.Position.Y), CurProjectileSprite[0]));
                   
                break;
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
