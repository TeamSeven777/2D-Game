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
        public List<IHitboxOwner> InActiveProjectiles { get; set; }
        public List<IHitboxOwner> ActiveProjectiles { get; set; }
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
            ActiveProjectiles = new List<IHitboxOwner>();
            enemiesRef = enemies;
            this.targetRef = playerRef;

        }

        public void Update(GameTime gt)
        {
            for (int i = 0; i < ActiveProjectiles.Count; i++)
            {
                IProjectile curProjectile = (IProjectile)ActiveProjectiles[i]; 
                curProjectile.Update(gt);
                if (curProjectile.DespawnTimer <= 0) ActiveProjectiles.Remove(ActiveProjectiles[i]);
            }
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
                    ActiveProjectiles.Add(new MosquitoProjectile(enemy.Position, targetRef.Position, ProjectileSpriteFactory.Instance.CreateEnemyProjectile(enemy)));
                break;
                case (Spider):
                    ActiveProjectiles.Add(new SpiderProjectile(enemy.Position, targetRef.Position, ProjectileSpriteFactory.Instance.CreateEnemyProjectile(enemy)));
                break;
                default:
                break;
            }

        }

        public void Remove(IHitboxOwner x)
        {
            if (ActiveProjectiles.Count > 0 && (ActiveProjectiles.IndexOf(x) > 0))
            {
                RoomCollisionHandler.Instance.Remove(x);
            }
        }

        public void Clear()
        {
            if(ActiveProjectiles.Count > 0) ActiveProjectiles.Clear();
        }
    }
}
