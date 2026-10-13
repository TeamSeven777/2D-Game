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
    public class DefaultPlayer : IPlayer, IHitboxOwner
    {

        public PlayerStateMachine stateMachine;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        //public Rectangle BoundingBox;
        public IHitbox Hitbox { get; set; }
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public ISet<Direction> DisabledDirections;
        private double IFrames = 0.5;
        private double IFramesTimer = 0;
        public ITool toolUsed;

        /// <summary>
        /// Creates a player object with sprites given in playerSpriteSheet
        /// </summary>
        public DefaultPlayer()
        {
            WalkSpeed = 5.0f;
            MaxHealth = 5;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine();
            Position = new Vector2(100, 100);
            PreviousPosition = Position;
            //The size might be slightly smaller than intended but!
            //For reference, the sprites have been scaled up by 16 from original (spriteSizexspriteSize -> 512x512)
            //and I have set their scale 1/4th the original so it still fits on screen
            Hitbox = new PlayerHitBox(new Rectangle((int)Position.X, (int)Position.Y, 64, 128), this);
            DisabledDirections = new HashSet<Direction>();
            toolUsed = new NoTool();
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
                if (IFramesTimer >= IFrames)
                {
                    IFramesTimer = 0;
                    stateMachine.Damaged = false;
                }
            }

            PreviousPosition = Position;
            stateMachine.Update(gt);
            toolUsed.Update(gt);
            Hitbox.BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Hitbox.BoundingBox.Width, Hitbox.BoundingBox.Height);
        }

        public void Draw(SpriteBatch sprbatch)
        {
            //In theory should draw the player's center at its current position?
            stateMachine.CurrPlayerState.Draw(sprbatch, Position - (stateMachine.CurrPlayerState.PlayerSprite.Size / 2));
            toolUsed.Draw(sprbatch, Position);
        }

        public void MoveLeft()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Left))
            {
                Position.X -= WalkSpeed;
                stateMachine.ChangeDirection(Direction.Left);
            }
        }
        public void MoveRight()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Right))
            {
                Position.X += WalkSpeed;
                stateMachine.ChangeDirection(Direction.Right);
            }
        }
        public void MoveUp()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Up))
            {
                Position.Y -= WalkSpeed;
                stateMachine.ChangeDirection(Direction.Up);
            }
        }
        public void MoveDown()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Down))
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
        public void UseTool(Tool tool)
        {
            if (stateMachine.toolUsed == Tool.None) stateMachine.UseTool(tool);
            toolUsed = stateMachine.toolEquiped;
        }

        //can disable/enable a particular direction
        public void ToggleAllowedDirection(Direction dir, bool allowed)
        {
            if (allowed && DisabledDirections.Contains(dir)) DisabledDirections.Remove(dir);
            else DisabledDirections.Add(dir);
        }
    }
}