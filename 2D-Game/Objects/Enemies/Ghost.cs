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

        // Chasing
        private float chaseRange = 250f; // how close the player has to be before the ghost follows
        private float chaseSpeed = 2f;
        private bool chasing = false;

        // Attack timing
        private bool attacking = false;
        private double attackTimer = 0;
        private float attackRange = 80f;               // how close the player has to be to get hit
        private const double AttackCooldown = 1.0;     // seconds to wait after an attack before attacking again
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

        public void Update(GameTime gameTime, Vector2 playerPosition)
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
                float distance = Vector2.Distance(Position, playerPosition);

                if (distance < attackRange)
                {
                    chasing = true;
                    //close enough to hit, so attack once the cooldown is over (and hold still until then)
                    if (attackTimer >= AttackCooldown)
                    {
                        attacking = true;
                        attackTimer = 0;
                        RestartAttackAnimation();
                        currentSprite = attackSprite;
                    }
                }
                else if (distance < chaseRange)
                {
                    chasing = true;
                    Vector2 toPlayer = playerPosition - Position;
                    toPlayer.Normalize(); //turn it into a direction with length 1
                    Position += toPlayer * chaseSpeed;
                }
                else
                {
                    if (chasing)
                    {
                        //player got away, so patrol from where the ghost is now
                        chasing = false;
                        startPosition = Position;
                    }
                    Position += new Vector2(speed * direction, 0);
                    if (Math.Abs(Position.X - startPosition.X) >= range)
                        direction *= -1;
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
