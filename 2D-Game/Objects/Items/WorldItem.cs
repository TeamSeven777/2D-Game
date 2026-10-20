using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class WorldItem
    {
        public IItem ItemData { get; private set; }
        public Vector2 Position { get; set; }
        public bool IsCollected { get; set; } = false;

        public WorldItem(IItem itemData, Vector2 position)
        {
            ItemData = itemData;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            // Animation logic here if needed
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsCollected && ItemData?.Icon != null)
            {
                ItemData.Icon.Draw(spriteBatch, Position);
            }
        }
    }
}