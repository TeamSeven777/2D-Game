using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public interface IEnemy
    {
        Vector2 Position { get; set; }
        public IHitbox Hitbox { get; set; }


        void Update(GameTime gameTime, Vector2 playerPosition);
        void Draw(SpriteBatch spriteBatch);
        void Unload();

         void Load();

    }
}