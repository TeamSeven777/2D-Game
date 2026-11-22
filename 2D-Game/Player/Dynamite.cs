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
    internal class Dynamite : IRangedTool
    {
        public Direction direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
        }

        public Tool GetTool()
        {
            return Tool.Dynamite;
        }
        public IProjectile CreateProjectile(Vector2 pos)
        {
            return new PlayerProjectileDynamite(pos, new Vector2(0, 0), ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Dynamite));
        }
        public void Update(GameTime gameTime)
        {
        }
    }
}
