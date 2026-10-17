using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class Boundary : IObstacle, IHitboxOwner
{
        public ISprite sprite { get; set; }
        public Vector2 originPos {get; set;}
        public Vector2 destinationPos { get; set;}
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }

        public Boundary(Vector2 topLeftCorner, Vector2 bottomRightCorner)
        {
            originPos = topLeftCorner;
            destinationPos = bottomRightCorner;
            Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint(), destinationPos.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            sprite = null;
            RoomCollisionHandler.Instance.Add(this, false);
        }
        public Boundary(Vector2 topLeftCorner, Vector2 bottomRightCorner, Texture2D texture)
        {
            sprite = new Sprite(texture, topLeftCorner, bottomRightCorner, Color.White);
            originPos = topLeftCorner;
            destinationPos = bottomRightCorner;
            Hitbox = new BlockHitBox(new Rectangle(originPos.ToPoint(), destinationPos.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);
        }

        public void Update(GameTime gt)
        {

        }
        public void Draw(SpriteBatch sprbatch)
        {
            if(sprite!=null)sprite.Draw(sprbatch, originPos);
        }

    }
}
