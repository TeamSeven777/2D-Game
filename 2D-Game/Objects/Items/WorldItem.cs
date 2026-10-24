using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class WorldItem : IHitboxOwner
    {
        public IItem ItemData { get; private set; }
        public Vector2 Position { get; set; }
        public bool IsCollected { get; set; } = false;
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public WorldItem(IItem itemData, Vector2 position)
        {
            ItemData = itemData;
            Position = position;
            CollisionHandler = new ItemCollisionHandler(this);
            Hitbox = new ItemHitbox(new Rectangle((int)Position.X,(int)Position.Y,16,16),HitBoxType.Item);
            RoomCollisionHandler.Instance.Add(this, true);
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