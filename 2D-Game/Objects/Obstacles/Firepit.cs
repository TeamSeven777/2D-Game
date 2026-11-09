using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Class
{
    public class Firepit : IObstacle, IHitboxOwner, IInteractable
    {
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public Vector2 OriginPos { get; set; }
        public ISprite Sprite { get; set; }
        public bool MarkedForRemoval { get; set; }

        public Firepit(Vector2 size, Vector2 originPos, ISprite sprite)
        {
            OriginPos = originPos;
            Sprite = sprite;
            Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint() / 2, size.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);
        }

        public void HandleInteraction()
        {
            var selectedItem = ItemInventory.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.Name.Equals("Log"))
            {
            Game1 game = RoomCollisionHandler.Instance.myGame;
            int logsHeld = ItemInventory.Instance.GetItemCount("Log");    
            int logsToAdd = Math.Min(logsHeld, Game1.LogsNeeded - game.LogsInFire);
            if (logsToAdd <= 0) return;
            ItemInventory.Instance.ChangeValue("Log", -logsToAdd);
            game.AddLogsToFire(logsToAdd);
            }

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprbatch)
        {
            Sprite.Draw(sprbatch, OriginPos);
        }
    }
}
