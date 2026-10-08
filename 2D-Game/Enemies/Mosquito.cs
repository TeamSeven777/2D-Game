using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class Mosquito : IEnemy
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite sprite;
        private IHitbox hitbox;
        private Vector2 startPosition;
        private float speed = 3f;
        private float range = 150f;
        private int direction = -1;
        private double elapsedSeconds;

        // Chasing
        private float chaseRange = 250f; // how close the player has to be before the mosquito follows
        private float chaseSpeed = 4f;
        private bool chasing = false;

        // Spin attack
        private float attackRange = 80f;             // how close the player has to be before the mosquito spins
        private const double SpinDuration = 0.5;     // seconds for one full spin
        private const double SpinCooldown = 1.0;     // seconds to wait after a spin before spinning again
        private bool spinning = false;
        private double spinTimer = 0;

        public Mosquito(AnimatedSprite sprite, Vector2 startPosition)
        {
            this.sprite = sprite;
            this.startPosition = startPosition;
            Position = startPosition;

            //Unfortunately unable to take straight from sprite due to type mismatch
            //Dont know if there is a fix to this, since I think spritebatch draw
            //takes only Rectangle from the XNA framework (This is System.Drawing for reference)
            hitbox = new EnemyHitBox(new System.Drawing.Rectangle((int)this.Position.X,(int)this.Position.Y,sprite.SourceRectangle.Width,sprite.SourceRectangle.Y));

            //spin around the middle of the frame instead of the top-left corner
            sprite.Origin = new Vector2(sprite.SourceRectangle.Width / 2, sprite.SourceRectangle.Height / 2);
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            spinTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spinning)
            {
                //stay in place and turn one full circle over SpinDuration
                sprite.Rotation = (float)(spinTimer / SpinDuration * MathHelper.TwoPi);
                if (spinTimer >= SpinDuration)
                {
                    spinning = false;
                    spinTimer = 0;
                    sprite.Rotation = 0;
                }
                sprite.Update(gameTime);
                return;
            }

            float distance = Vector2.Distance(Position, playerPosition);
            float facingX;

            if (distance < attackRange)
            {
                chasing = true;
                facingX = playerPosition.X - Position.X;
                //close enough, so spin once the cooldown is over (and hold still until then)
                if (spinTimer >= SpinCooldown)
                {
                    spinning = true;
                    spinTimer = 0;
                }
            }
            else if (distance < chaseRange)
            {
                chasing = true;
                Vector2 toPlayer = playerPosition - Position;
                facingX = toPlayer.X;
                toPlayer.Normalize(); //turn it into a direction with length 1
                Position += toPlayer * chaseSpeed;
            }
            else
            {
                if (chasing)
                {
                    //player got away, so patrol from where the mosquito is now
                    chasing = false;
                    startPosition = Position;
                    elapsedSeconds = 0; //restart the bob at the middle so it doesn't jump
                }
                elapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                float x = Position.X + speed * direction;
                if (Math.Abs(x - startPosition.X) >= range)
                    direction *= -1;
                float y = startPosition.Y + (float)Math.Sin(elapsedSeconds * 4) * 20f;
                Position = new Vector2(x, y);
                facingX = direction;
            }

            //sprite sheet row faces left, so flip when moving right
            sprite.Effects = facingX > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Position is the top-left corner, so shift by the origin to draw in the same place
            sprite.Draw(spriteBatch, Position + sprite.Origin * sprite.Scale);
        }
    }
}
