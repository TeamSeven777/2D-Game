using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace _2D_Game
{
    public class Obstacle : IObstacle
    {
        public Rectangle boundingBox { get; set; }
        public Vector2 originPos { get; set; }
        public Sprite sprite { get; set; }
        public Queue<Sprite> spriteSet;
        private bool isReversed = false;
        private double cooldownTimer = 0.21;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// Creates an obstacle with <paramref name="boundingBox"/> hitbox at <paramref name="originPos"/>
        /// </summary>
        /// <param name="boundingBox">In case sprite doesnt match boundaries</param>
        /// <param name="originPos"></param>
        /// <param name="sprite"></param>
        public Obstacle(Rectangle boundingBox, Vector2 originPos, Sprite sprite)
        {
            this.originPos = originPos;
            this.sprite = sprite;
            this.boundingBox = boundingBox;
        }

        /// <summary>
        /// (DEBUG)Creates an obstacle with <paramref name="boundingBox"/> hitbox at <paramref name="originPos"/>
        /// with ability to swap sprites
        /// </summary>
        /// <param name="boundingBox">In case sprite doesnt match boundaries</param>
        /// <param name="originPos"></param>
        /// <param name="sprite"></param>
        public Obstacle(Rectangle boundingBox, Vector2 originPos, Queue<Sprite> sprites)
        {
            this.originPos = originPos;
            spriteSet = sprites;
            this.sprite = sprites.First();
            this.boundingBox = boundingBox;
        }

        /// <summary>
        /// (DEBUG) Used to swap test block for another
        /// </summary>
        public void NextObstacle()
        {

            if (spriteSet != null && cooldownTimer > CooldownDuration)
            {
                if (isReversed) 
                { 
                    spriteSet = new Queue<Sprite>(spriteSet.Reverse());
                    spriteSet.Enqueue(spriteSet.Dequeue());
                    isReversed = false;
                }
                sprite = spriteSet.First();
                spriteSet.Enqueue(spriteSet.Dequeue());
                cooldownTimer = 0;
            }
        }

        /// <summary>
        /// (DEBUG) Used to swap test block for another
        /// </summary>
        public void PrevObstacle()
        {
            if (spriteSet != null && cooldownTimer > CooldownDuration)
            {
                if (!isReversed)
                {
                    spriteSet = new Queue<Sprite>(spriteSet.Reverse());
                    spriteSet.Enqueue(spriteSet.Dequeue());
                    isReversed = true;
                }
                sprite = spriteSet.First();
                spriteSet.Enqueue(spriteSet.Dequeue());
                cooldownTimer = 0;
            }
        }

        /// <summary>
        /// Draws obstacle to screen using <paramref name="sprbatch"/>
        /// </summary>
        /// <param name="sprbatch"></param>
        public void Draw(SpriteBatch sprbatch)
        {
            sprite.Draw(sprbatch, originPos);
        }
        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //If player.boundingBox.getArea == this.boundingBox.getArea (?) do player.Pos.X / Y - distance to edge
            //OR
            //If player.boundingBox.getArea == this.boundingBox.getArea do clamp player.Pos.X && || Y
            //OR (more likely)
            //CommandCheckCollisions
            cooldownTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }



    }
}
