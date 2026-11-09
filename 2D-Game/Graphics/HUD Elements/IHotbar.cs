using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public interface IHotbar
    {
        void Update(GameTime gameTime);
        void Next();
        void Previous();
        void Draw(SpriteBatch spriteBatch);
    }
}