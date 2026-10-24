using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class ProjectileSpriteFactory
    {
        private Texture2D PlayerProjectileSpritesheet;
        private Texture2D EnemyProjectileSpriteSheet;
        private Texture2D ExplosionSprites;
        private AnimatedSprite Explosion;
        private Animation ExplosionAnimation;
        private Texture2D stick;
        private int spriteSize = 64 * 64; //for player
        private TimeSpan FrameDelay;

        private static ProjectileSpriteFactory instance = new ProjectileSpriteFactory();

        public static ProjectileSpriteFactory Instance
        { get { return instance; } }

        private ProjectileSpriteFactory() { }

        public void LoadTextures(ContentManager content)
        {
            PlayerProjectileSpritesheet = content.Load<Texture2D>("Sprites/playerprojectiles");
            EnemyProjectileSpriteSheet = content.Load<Texture2D>("Sprites/enemy-projectiles");
            ExplosionSprites = content.Load<Texture2D>("Sprites/tempexplosionsprites");
        }

        /// <summary>
        /// Returns an animated explosion, typically for explosive projectiles, can be used
        /// for other things
        /// </summary>
        /// <returns></returns>
        public ISprite CreateExplosionAnimation()
        {
            ExplosionAnimation = new Animation(ExplosionSprites, TimeSpan.FromSeconds(0.25), new Vector2(95, 64), Vector2.Zero);
            Explosion = new AnimatedSprite(ExplosionAnimation, ExplosionSprites);
            return Explosion;
        }

        /// <summary>
        /// Returns a sprite based on <paramref name="tool"/>
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        public ISprite CreatePlayerProjectile(Tool tool)
        {
            ISprite sprite;
            switch (tool)
            {
                case (Tool.Slingshot):
                    sprite = new Sprite(PlayerProjectileSpritesheet, new Rectangle(64, 0, 64, 64));
                    break;
                case (Tool.Boomerang):
                    sprite = new Sprite(PlayerProjectileSpritesheet, new Rectangle(0, 0, 64, 64));
                    break;
                case (Tool.Dynamite):
                        sprite = new Sprite(PlayerProjectileSpritesheet, new Rectangle(128, 0, 64, 64));
                    break;
                default:
                    sprite = new Sprite();
                    break;
            }

            return sprite;
        }
        /// <summary>
        /// Returns a projectile sprite fired by the respective <paramref name="enemy"/>
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public ISprite CreateEnemyProjectile(IEnemy enemy)
        {
            ISprite sprite;
            switch (enemy)
            {
                case (Mosquito):
                    sprite = new Sprite(EnemyProjectileSpriteSheet, new Rectangle(64,0, 64, 64));
                    break;
                case (Spider):
                    sprite = new Sprite(EnemyProjectileSpriteSheet, new Rectangle(0,0, 64, 64));
                    break;
                default:
                    sprite = new Sprite();
                    break;
            }
            return sprite;
        }

    }
}
       