using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class Mosquito : IEnemy
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite sprite;
        private Vector2 startPosition;
        private float speed = 3f;
        private float range = 150f;
        private int direction = -1;
        private double elapsedSeconds;

        public Mosquito(AnimatedSprite sprite, Vector2 startPosition)
        {
            this.sprite = sprite;
            this.startPosition = startPosition;
            Position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            elapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            float x = Position.X + speed * direction;
            if (Math.Abs(x - startPosition.X) >= range)
                direction *= -1;
            float y = startPosition.Y + (float)Math.Sin(elapsedSeconds * 4) * 20f;
            Position = new Vector2(x, y);

            //sprite sheet row faces left, so flip when moving right
            sprite.Effects = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}