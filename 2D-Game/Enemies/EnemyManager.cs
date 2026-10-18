using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class EnemyManager
    {
        private readonly List<IEnemy> enemies = new();
        private int currentIndex = 0;
        private readonly Vector2 spawnOffset = new Vector2(550, 150);        
        //Debugging Purposes (Sorry ; ( )
        private bool AIDisabled = false;
        private double ToggleCooldownTimer = 0;
        private const double ToggleCooldownDuration = 0.2;

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// SpriteFactory for all enemies
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content, Vector2 roomOrigin)
        {
            Vector2 spawnPosition = roomOrigin + spawnOffset;
            
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
            enemies[enemies.Count - 1].Unload();
            AIDisabled = false;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (ToggleCooldownTimer > 0)
            {
                ToggleCooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (!AIDisabled)
            {
                if (enemies.Count == 0) return;
                enemies[currentIndex].Update(gameTime, playerPosition);
            }
            if (enemies.Count > 0) 
            { 
                if (enemies[currentIndex].Health <= 0)
                {
                    enemies[currentIndex].Unload();
                    enemies.Remove(enemies[currentIndex]);
                    if(currentIndex >= enemies.Count)currentIndex = 0;
                    if (enemies.Count > 0) enemies[currentIndex].Load();
                }
            }
        }

        /// <summary>
        /// (DEBUG) Swap test enemy for another type 
        /// </summary>
        public void Next()
        {
            if (enemies.Count == 0 || cooldownTimer > 0) return;
            enemies[currentIndex].Unload();
            currentIndex = (currentIndex + 1) % enemies.Count;
            cooldownTimer = CooldownDuration; // Reset timer
            enemies[currentIndex].Load();
        }

        /// <summary>
        /// (DEBUG) Swap test enemy for another type 
        /// </summary>
        public void Previous()
        {
            if (enemies.Count == 0 || cooldownTimer > 0) return;
            enemies[currentIndex].Unload();
            currentIndex = (currentIndex - 1 + enemies.Count) % enemies.Count;
            cooldownTimer = CooldownDuration; // Reset timer
            enemies[currentIndex].Load();
        }

        public IEnemy GetCurrentEnemy()
        {
            return enemies[currentIndex];
        }

        public void ToggleAI()
        {
            if (ToggleCooldownTimer > 0) return;

            if (AIDisabled)
                AIDisabled = false;
            else
                AIDisabled = true;
            ToggleCooldownTimer = ToggleCooldownDuration;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enemies.Count == 0) return;

            enemies[currentIndex].Draw(spriteBatch);
        }
    }
}
