using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Class
{
    public class Door : IObstacle, IHitboxOwner, IInteractable
{
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public Vector2 OriginPos { get; set; }
        public ISprite Sprite { get; set; }
        public bool MarkedForRemoval { get; set; }
        private bool IsOpen = false;
        

        public Door(Vector2 size, Vector2 originPos, ISprite sprite)
        {
            this.OriginPos = originPos;
            this.Sprite = sprite;
            this.Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint() - size.ToPoint() / 2, size.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);
        }

        public void Update()
        {

        }
        /// <summary>
        /// If player has a key, remove the door (or could replace the door with an open one)
        /// </summary>
        public void HandleInteraction()
        {
            var selectedItem = ItemInventory.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.Name.Equals("Key"))
            {
                RoomCollisionHandler.Instance.Remove(this);
                selectedItem.Use();
                IsOpen = true;
                Sprite.SourceRectangle = new Rectangle(128, 0, 128, 128);

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
