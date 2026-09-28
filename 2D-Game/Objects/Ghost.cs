using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class Ghost : IEnemy
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite idleSprite;
        private AnimatedSprite attackSprite;
        private AnimatedSprite currentSprite;
        private Vector2 startPosition;
        private float speed = 1.5f;
        private float range = 100f;
        private int direction = 1;

        // Attack timing
        private bool attacking = false;
        private double attackTimer = 0;
        private const double TimeBetweenAttacks = 3.0; // seconds of floating before each attack
        private readonly double attackDuration;       // how long one play of the attack animation takes
        private readonly Rectangle attackFirstFrame;

        public Ghost(AnimatedSprite idleSprite, AnimatedSprite attackSprite, Vector2 startPosition)
        {
            this.idleSprite = idleSprite;
            this.attackSprite = attackSprite;
            currentSprite = idleSprite;
            this.startPosition = startPosition;
            Position = startPosition;

            attackFirstFrame = attackSprite.Animation.Frames.Peek();
            attackDuration = attackSprite.Animation.Frames.Count * attackSprite.Animation.FrameRate.TotalSeconds;
        }

        public void Update(GameTime gameTime)
        {
            attackTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (attacking)
            {
                //stay in place until the attack animation has played once
                if (attackTimer >= attackDuration)
                {
                    attacking = false;
                    attackTimer = 0;
                    currentSprite = idleSprite;
                }
            }
            else
            {
                Position += new Vector2(speed * direction, 0);
                if (Math.Abs(Position.X - startPosition.X) >= range)
                    direction *= -1;

                if (attackTimer >= TimeBetweenAttacks)
                {
                    attacking = true;
                    attackTimer = 0;
                    RestartAttackAnimation();
                    currentSprite = attackSprite;
                }
            }

            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch, Position);
        }

        //Rewinds the attack so it always plays from the first frame
        private void RestartAttackAnimation()
        {
            var frames = attackSprite.Animation.Frames;
            while (frames.Peek() != attackFirstFrame)
                frames.Enqueue(frames.Dequeue());

            //show the first frame now, and move it to the back so the next frame shown is frame 2
            attackSprite.SourceRectangle = frames.Dequeue();
            frames.Enqueue(attackSprite.SourceRectangle);
            attackSprite.TimeSinceLastFrame = TimeSpan.Zero;
        }
    }
}
