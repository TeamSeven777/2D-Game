using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class Mosquito : IEnemy, IHitboxOwner
    {
        public Vector2 Position { get; set; }
        public int Health   { get; set; }
        private bool isDamaged = false;
        private double IFrames = 0.5;
        private double IFramesTimer = 0;
        private AnimatedSprite sprite;        
        private Vector2 startPosition;
        private float speed = 2f;
        private float range = 150f;
        private int direction = -1;
        private double elapsedSeconds;

        //Hitbox
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        private int hbWidth = 0;
        private int hbHeight = 0;
        private int attackHBsize = 40;

        // Chasing
        private float chaseRange = 250f; // how close the player has to be before the mosquito follows
        private float chaseSpeed = 2f;
        private bool chasing = false;

        // Spin attack
        private float attackRangeX = 50f;             // how close the player has to be before the mosquito spins
        private float attackRangeYTop = 50f;
        private float attackRangeYBottom = 85f;
        private const double SpinDuration = 0.5;     // seconds for one full spin
        private const double SpinCooldown = 1.0;     // seconds to wait after a spin before spinning again
        private bool spinning = false;
        private double spinTimer = 0;

        // Projectile Attack
        private double ShootCooldown =  10;
        private Timer soundTimer;


        public Mosquito(AnimatedSprite sprite, Vector2 startPosition)
        {
            this.sprite = sprite;
            this.startPosition = startPosition;
            Position = startPosition;

            Health = 10;
            //spin around the middle of the frame instead of the top-left corner
            sprite.Origin = new Vector2(sprite.SourceRectangle.Width / 2, sprite.SourceRectangle.Height / 2);

            //hitbox
            Hitbox = new EnemyHitBox(new Rectangle((int)(Position.X + sprite.Origin.X - hbWidth / 2), (int)(Position.Y + sprite.Origin.Y - hbHeight / 2), hbWidth, hbHeight));
            CollisionHandler = new EnemyCollisionHandler(this);
            RoomCollisionHandler.Instance.Add(this, true);

            //Random rand = new Random(); rand.NextDouble() * 3
            soundTimer = new Timer(2);
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
                sprite.Color = Color.Red;
            }
        }
        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            spinTimer += gameTime.ElapsedGameTime.TotalSeconds;
            soundTimer.Update(gameTime);
            if (spinning)
            {
                //stay in place and turn one full circle over SpinDuration
                sprite.Rotation = (float)(spinTimer / SpinDuration * MathHelper.TwoPi);
                if (spinTimer < SpinDuration)
                {
                    hbWidth = hbHeight = attackHBsize;
                    Hitbox.BoundingBox = new Rectangle((int)(Position.X + sprite.Origin.X - hbWidth / 2), (int)(Position.Y + sprite.Origin.Y - hbHeight / 2), hbWidth, hbHeight);
                }
                if (spinTimer >= SpinDuration)
                {
                    spinning = false;
                    spinTimer = 0;
                    sprite.Rotation = 0;
                    hbWidth = hbHeight = 0;
                }
                sprite.Update(gameTime);
                return;
            }

            float distance = Vector2.Distance(Position + sprite.Origin, playerPosition);
            float distanceX = Math.Abs(Position.X + sprite.Origin.X - playerPosition.X);
            float distanceY = Math.Abs(Position.Y + sprite.Origin.Y - playerPosition.Y);
            float facingX; float facingY = playerPosition.Y - Position.Y - sprite.Origin.Y;

            if (distanceX < attackRangeX && ((facingY > 0 && distanceY < attackRangeYTop) || (facingY <0 && distanceY < attackRangeYBottom)))
            {
                chasing = true;
                facingX = playerPosition.X - Position.X - sprite.Origin.X;
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

                if (ShootCooldown == 0)
                {
                    EnemyProjectileHandler.Instance.Add(this);
                    ShootCooldown = 200;
                }
                else ShootCooldown--;
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

            IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (IFramesTimer >= IFrames)
            {
                IFramesTimer = 0;
                isDamaged = false;
                sprite.Color = Color.White;
            }

            if (soundTimer.Ringing && chasing)
            {
                SoundEffectFactory.Instance.PlaySoundEffect(SoundName.MosquitoBuzz, true);
                soundTimer.Reset();
            }

            //sprite sheet row faces left, so flip when moving right
            sprite.Effects = facingX > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprite.Update(gameTime);
            Hitbox.BoundingBox = new Rectangle((int)(Position.X + sprite.Origin.X - hbWidth / 2), (int)(Position.Y + sprite.Origin.Y - hbHeight / 2), hbWidth, hbHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Position is the top-left corner, so shift by the origin to draw in the same place
            sprite.Draw(spriteBatch, Position + sprite.Origin * sprite.Scale);
        }
    }
}
