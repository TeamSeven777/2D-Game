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

        public void Initialize(ref EnemyManager enemies, ref DefaultPlayer playerRef)
        {
            ActiveProjectiles = new Queue<IProjectile>();
            enemiesRef = enemies;
            this.targetRef = playerRef;

        }

        public void Update(GameTime gt)
        {
            foreach (IProjectile projectile in ActiveProjectiles)
            {
                projectile.Update(gt);
                if (projectile.DespawnTimer <= 0) RemoveCount++;
            }
            for (int i = 0; i < RemoveCount; i++) this.Remove();
            RemoveCount = 0;
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
                    ActiveProjectiles.Enqueue(new MosquitoProjectile(enemy.Position, targetRef.Position, ProjectileSpriteFactory.Instance.CreateEnemyProjectile(enemy)));
                break;
                case (Spider):
                    ActiveProjectiles.Enqueue(new SpiderProjectile(enemy.Position, targetRef.Position, ProjectileSpriteFactory.Instance.CreateEnemyProjectile(enemy)));
                break;
                default:
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
