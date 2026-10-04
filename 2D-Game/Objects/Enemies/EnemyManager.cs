using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class EnemyManager
    {
        private readonly List<IEnemy> enemies = new();
        private int currentIndex = 0;
        private readonly Vector2 spawnPosition = new Vector2(550, 150);

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// SpriteFactory for all enemies
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            //Ghost: 12 frames of 256x256 in one row
            Texture2D ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            Animation ghostIdle = new Animation(ghostSheet, TimeSpan.FromMilliseconds(100), new Vector2(256, 256), Vector2.Zero, ghostSheet.Width);
            AnimatedSprite ghostSpr = new AnimatedSprite(ghostIdle, ghostSheet);
            ghostSpr.Scale = new Vector2(0.5f);

            //Ghost attack: 24 frames of 256x256 in one row
            Texture2D ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            Animation ghostAttack = new Animation(ghostAttackSheet, TimeSpan.FromMilliseconds(50), new Vector2(256, 256), Vector2.Zero, ghostAttackSheet.Width);
            AnimatedSprite ghostAttackSpr = new AnimatedSprite(ghostAttack, ghostAttackSheet);
            ghostAttackSpr.Scale = new Vector2(0.5f);

            //Mosquito: 6 frames of 96x96 per row, row 1 is the side view
            Texture2D mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
            Animation mosquitoFly = new Animation(mosquitoSheet, TimeSpan.FromMilliseconds(80), new Vector2(96, 96), new Vector2(0, 96), mosquitoSheet.Width);
            AnimatedSprite mosquitoSpr = new AnimatedSprite(mosquitoFly, mosquitoSheet);
            mosquitoSpr.Scale = Vector2.One;

            // Add all enemies here
            enemies.Add(new Ghost(ghostSpr, ghostAttackSpr, spawnPosition));
            enemies.Add(new Mosquito(mosquitoSpr, spawnPosition));
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (enemies.Count == 0) return;
            enemies[currentIndex].Update(gameTime, playerPosition);
        }

        /// <summary>
        /// (DEBUG) Swap test enemy for another type 
        /// </summary>
        public void Next()
        {
            if (enemies.Count == 0 || cooldownTimer > 0) return;

            currentIndex = (currentIndex + 1) % enemies.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }

        /// <summary>
        /// (DEBUG) Swap test enemy for another type 
        /// </summary>
        public void Previous()
        {
            if (enemies.Count == 0 || cooldownTimer > 0) return;

            currentIndex = (currentIndex - 1 + enemies.Count) % enemies.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enemies.Count == 0) return;

            enemies[currentIndex].Draw(spriteBatch);
        }
    }
}
