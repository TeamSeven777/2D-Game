using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class Slingshot : IRangedTool
    {
        public IHitbox Hitbox {  get; set; }
        public Direction direction { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }

        public Tool GetTool()
        {
            return Tool.Slingshot;
        }
        public void Update(GameTime gt)
        {

        }
        public IProjectile CreateProjectile(Vector2 pos)
        {
            return new PlayerProjectilePebble(pos, new Vector2(0, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Slingshot));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) { }
    }
}
