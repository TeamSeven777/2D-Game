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
    public class Slingshot : ITool, IHitboxOwner
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
        public void Draw(SpriteBatch spriteBatch, Vector2 position) { }
    }
}
