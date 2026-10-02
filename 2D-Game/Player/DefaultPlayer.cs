using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        public Rectangle BoundingBox;
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        private double IFrames = 0.5;
        private double IFramesTimer = 0;


        public DefaultPlayer(Texture2D playerSpriteSheet)
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine(playerSpriteSheet);
            Position = new Vector2(100, 100);
            PreviousPosition = Position;
            BoundingBox = new Rectangle((int)Position.X,(int)Position.Y,16,16);
        }

        public void Update(GameTime gt)
        {
            gameTime = gt;
            if (PreviousPosition != Position) stateMachine.Moving = true;
            else stateMachine.Moving = false;
            if (stateMachine.Damaged)
            {
                IFramesTimer += gt.ElapsedGameTime.TotalSeconds;
                if(IFramesTimer >= IFrames)
                {
                    IFramesTimer = 0;
                    stateMachine.Damaged = false;
                }
            }
            Debug.WriteLine("Rectangle Position = {0},{1}", BoundingBox.X, BoundingBox.Y);
            BoundingBox.X = (int)Position.X; BoundingBox.Y = (int)Position.Y;
            PreviousPosition = Position;
            stateMachine.Update(gt);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            stateMachine.CurrPlayerState.Draw(sprbatch, Position);
        }
        //TODO add way to disable/enable certain actions at given times
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
