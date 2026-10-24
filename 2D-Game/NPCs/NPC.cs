using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class NPC : INPC, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        private double IFrames = 0.5;
        private double IFramesTimer = 0;
        private AnimatedSprite sprite;        
        private Vector2 startPosition;
        private double elapsedSeconds;

        //Hitbox
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        private int hbWidth = 30;
        private int hbHeight = 30;


        public NPC(AnimatedSprite sprite, Vector2 startPosition)
        {
            this.sprite = sprite;
            this.startPosition = startPosition;
            Position = startPosition;
            sprite.Origin = new Vector2(sprite.SourceRectangle.Width / 2, sprite.SourceRectangle.Height / 2);

            //hitbox
            Hitbox = new BlockHitBox(new Rectangle((int)(Position.X + sprite.Origin.X - hbWidth / 2), (int)(Position.Y + sprite.Origin.Y - hbHeight / 2), hbWidth, hbHeight));
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, true);
        }

        public void Unload()
        {
            RoomCollisionHandler.Instance.Remove(this);
        }
        public void Load()
        {
            RoomCollisionHandler.Instance.Add(this, true);
        }
        
        public void Update(GameTime gameTime)
        {
            IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (IFramesTimer >= IFrames)
            {
                IFramesTimer = 0;
            }
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Position is the top-left corner, so shift by the origin to draw in the same place
            sprite.Draw(spriteBatch, Position + sprite.Origin * sprite.Scale);
        }
    }
}
