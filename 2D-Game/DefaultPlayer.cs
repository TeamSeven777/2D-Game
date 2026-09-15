using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DefaultPlayer : IPlayer
{
        //private Sprite/AnimatedSprite PlayerSprite;


        public Vector2 Position;
        public Vector2 PreviousPosition;
        private GameTime gameTime { get; set; }

        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }


        public DefaultPlayer()
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;

            Position = new Vector2(0, 0);
            PreviousPosition = Position;
        }

        public void Update(GameTime gt) {
            gameTime = gt;
            PreviousPosition = Position;
        }
        
        public void MoveLeft()
        { 
            Position.X -= WalkSpeed;
            //call a method of PlayerSprite to update/animate the sprite
        }
        public void MoveRight()
        {
            Position.X += WalkSpeed;
        }
        public void MoveUp()
        {
            Position.Y -= WalkSpeed;
        }
        public void MoveDown()
        {
            Position.Y += WalkSpeed;
        }
    }
}
