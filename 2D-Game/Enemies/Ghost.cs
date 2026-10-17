using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class Ghost : IEnemy, IHitboxOwner
    {
        public Vector2 Position { get; set; }       
        private AnimatedSprite idleSprite;
        private AnimatedSprite attackSprite;
        private AnimatedSprite currentSprite;
        private Vector2 startPosition;
        private float speed = 1.5f;
        private float range = 100f;
        private int direction = 1;

        //Hitbox
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        private int hbWidth = 50;
        private int hbHeight = 50;

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

        public int Health { get; set; }
        private double IFrames = 1;
        private double IFramesTimer = 0;
        private bool isDamaged = false;

        public Ghost(AnimatedSprite idleSprite, AnimatedSprite attackSprite, Vector2 startPosition)
        {
            this.idleSprite = idleSprite;
            this.attackSprite = attackSprite;
            currentSprite = idleSprite;
            this.startPosition = startPosition;
            Position = startPosition;
            Health = 3;

            attackFirstFrame = attackSprite.Animation.Frames.Peek();
            attackDuration = attackSprite.Animation.Frames.Count * attackSprite.Animation.FrameRate.TotalSeconds;

            Hitbox = new EnemyHitBox(new Rectangle((int)Position.X-hbWidth/2, (int)Position.Y-hbHeight/2, hbWidth, hbHeight));
            CollisionHandler = new EnemyCollisionHandler(this);
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
        public void TakeDamage(int attackPoints)
        {
            if (!isDamaged)
            {
                Health = Health - attackPoints;
                if (Health < 0) Health = 0;
                isDamaged = true;
                currentSprite.Color = Color.Red;
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            attackTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (attacking)
            {
                if(attackTimer  >= .5)
                {
                    hbWidth = hbHeight = 100;//temporary solution for enemyattack hitbox
                }
                //stay in place until the attack animation has played once
                if (attackTimer >= attackDuration)
                {
                    attacking = false;
                    attackTimer = 0;
                    currentSprite = idleSprite;
                    hbHeight = 50; hbWidth = 50;
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
                        attackSprite = new AnimatedSprite(attackSprite);
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

            IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (IFramesTimer >= IFrames)
            {
                IFramesTimer = 0;
                isDamaged = false;
                currentSprite.Color = Color.White;
            }

            Hitbox.BoundingBox = new Rectangle((int)Position.X - hbWidth / 2, (int)Position.Y - hbHeight / 2, hbWidth, hbHeight);
            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch, Position);
        }

    }
}
