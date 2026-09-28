using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DefaultPlayer : IPlayer
    {
        private Sprite PlayerSprite; //change later

        public IStateMachine stateMachine;
        public Dictionary<IStateMachine.Direction, AnimatedSprite> spriteSet;
        public AnimatedSprite defaultSprite;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }


        public DefaultPlayer(Dictionary<IStateMachine.Direction, AnimatedSprite> spriteSet)
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine();
            defaultSprite = spriteSet.First().Value;
            this.spriteSet = spriteSet;
            Position = new Vector2(0, 0);
            PreviousPosition = Position;
        }

        public void Update(GameTime gt)
        {
            gameTime = gt;
            PreviousPosition = Position;
            defaultSprite.Update(gt);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            defaultSprite.Draw(sprbatch, Position);
        }

        public void MoveLeft()
        {
            if(Position.X > 0)
                Position.X -= WalkSpeed;
            defaultSprite = stateMachine.GetDirectionalSprite(IStateMachine.Direction.Left, spriteSet);
            //call a method of PlayerSprite to update/animate the sprite
        }
        public void MoveRight()
        {
                Position.X += WalkSpeed;
            defaultSprite = stateMachine.GetDirectionalSprite(IStateMachine.Direction.Right, spriteSet);
        }
        public void MoveUp()
        {
                Position.Y -= WalkSpeed;
            defaultSprite = stateMachine.GetDirectionalSprite(IStateMachine.Direction.Up,spriteSet);
        }
        public void MoveDown()
        {
                Position.Y += WalkSpeed;
            defaultSprite = stateMachine.GetDirectionalSprite(IStateMachine.Direction.Down,spriteSet);
        }

        public void TakeDamage(int attackPoints)
        {
            Health = Health - attackPoints;
            if (Health < 0) Health = 0;
            //call damage animation
        }
      
    }
}
