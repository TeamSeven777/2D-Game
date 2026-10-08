using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DefaultPlayer : IPlayer
    {

        public PlayerStateMachine stateMachine;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        //public Rectangle BoundingBox;
        public IHitbox HitBox;
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        private double IFrames = 0.5;
        private double IFramesTimer = 0;

        /// <summary>
        /// Creates a player object with sprites given in playerSpriteSheet
        /// </summary>
        public DefaultPlayer(Texture2D playerSpriteSheet)
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine(playerSpriteSheet);
            Position = new Vector2(100, 100);
            PreviousPosition = Position;
            HitBox = new PlayerHitBox(new System.Drawing.Rectangle((int)Position.X,(int)Position.Y,16,16));
        }

        /// <summary>
        /// Checks player's current states and updates logic based on user input
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            gameTime = gt;
            if (PreviousPosition != Position) stateMachine.Moving = true;
            else stateMachine.Moving = false;

            //Player will be damaged based on duration of IFrames (half a second)
            //Essentialy applies a red filter
            if (stateMachine.Damaged)
            {
                IFramesTimer += gt.ElapsedGameTime.TotalSeconds;
                if(IFramesTimer >= IFrames)
                {
                    IFramesTimer = 0;
                    stateMachine.Damaged = false;
                }
            }
            //HitBox.BoundingBox.X = (int)Position.X; HitBox.BoundingBox.Y = (int)Position.Y;
            PreviousPosition = Position;
            stateMachine.Update(gt);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            stateMachine.CurrPlayerState.Draw(sprbatch, Position);
        }

        public void MoveLeft()
        {
            if (stateMachine.CanMove)
            {
                Position.X -= WalkSpeed;
                stateMachine.ChangeDirection(Direction.Left);
            }
        }
        public void MoveRight()
        {
            if (stateMachine.CanMove)
            {
                Position.X += WalkSpeed;
                stateMachine.ChangeDirection(Direction.Right);
            }
        }
        public void MoveUp()
        {
            if (stateMachine.CanMove)
            {
                Position.Y -= WalkSpeed;
                stateMachine.ChangeDirection(Direction.Up);
            }
        }
        public void MoveDown()
        {
            if (stateMachine.CanMove)
            {
                Position.Y += WalkSpeed;
                stateMachine.ChangeDirection(Direction.Down);
            }
        }

        public void TakeDamage(int attackPoints)
        {
            if (!stateMachine.Damaged)
            {
                Health = Health - attackPoints;
                if (Health < 0) Health = 0;
                stateMachine.Damaged = true;
            }
        }
        public void UseTool(Tool tool) { 
            if(stateMachine.toolUsed == Tool.None)stateMachine.UseTool(tool);
        }
      
    }
}
