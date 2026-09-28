using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public interface IEnemy
    {
        Vector2 Position { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}