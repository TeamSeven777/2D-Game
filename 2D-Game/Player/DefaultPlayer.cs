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

        public PlayerStateMachine stateMachine;
        //  public AnimatedSprite defaultSprite
        public Vector2 Position;
        public Vector2 PreviousPosition;
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }


        public DefaultPlayer(Texture2D playerSpriteSheet)
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine(playerSpriteSheet);
            Position = new Vector2(32, 32);
            PreviousPosition = Position;
        }

        public void Update(GameTime gt)
        {
            gameTime = gt;
            PreviousPosition = Position;
            stateMachine.Update(gt);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            stateMachine.CurrPlayerSprite.Draw(sprbatch, Position);
        }

        public void MoveLeft()
        {
            if(Position.X > 0)
                Position.X -= WalkSpeed;
            stateMachine.ChangeDirection(Direction.Left);
            //call a method of PlayerSprite to update/animate the sprite
        }
        public void MoveRight()
        {
                Position.X += WalkSpeed;
            stateMachine.ChangeDirection(Direction.Right);
        }
        public void MoveUp()
        {
                Position.Y -= WalkSpeed;
            stateMachine.ChangeDirection(Direction.Up);
        }
        public void MoveDown()
        {
                Position.Y += WalkSpeed;
            stateMachine.ChangeDirection(Direction.Down);
        }

        public void TakeDamage(int attackPoints)
        {
            Health = Health - attackPoints;
            if (Health < 0) Health = 0;
            stateMachine.BeDamaged(true);
        }
        public void UseTool(Tool tool) { 
            stateMachine.UseTool(tool);
        }
      
    }
}
