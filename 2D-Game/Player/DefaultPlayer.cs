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
        public Vector2 NextPosition;
        //public Rectangle BoundingBox;
        public IHitbox Hitbox { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        private GameTime gameTime { get; set; }
        public float WalkSpeed { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Tool CurrentRangedWeapon { get; set; }
        public WeaponInventory Weapons { get; set; }
        public ISet<Direction> DisabledDirections;
        public ISet<Direction> MovingDirections;
        private double IFrames = 1;
        private double IFramesTimer = 0;
        private double HitFrames = 0.5;
        private bool hasIFrames = false;
        private bool playerDead = false;
        public ITool toolUsed;

        /// <summary>
        /// Creates a player object with sprites given in playerSpriteSheet
        /// </summary>
        public DefaultPlayer(Vector2 startPosition)
        {
            WalkSpeed = 5.0f;
            MaxHealth = 3;
            Health = MaxHealth;
            stateMachine = new PlayerStateMachine(this);
            Position = startPosition;
            PreviousPosition = Position;
            NextPosition = Position;
            CurrentRangedWeapon = Tool.Slingshot;
            //The size might be slightly smaller than intended but!
            //For reference, the sprites have been scaled up by 16 from original (spriteSizexspriteSize -> 512x512)
            //and I have set their scale 1/4th the original so it still fits on screen
            Hitbox = new PlayerHitBox(new Rectangle((int)Position.X - 16, (int)Position.Y - 16, 32, 86), this);
            CollisionHandler = new PlayerCollisionHandler(this);
            RoomCollisionHandler.Instance.Add(this, true);
            DisabledDirections = new HashSet<Direction>();
            MovingDirections = new HashSet<Direction>();
            toolUsed = new NoTool();
        }

        /// <summary>
        /// Checks player's current states and updates logic based on user input
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            gameTime = gt;
            if (!playerDead)
            {
                Position = NextPosition;
                if (PreviousPosition != Position) stateMachine.Moving = true;
                else stateMachine.Moving = false;

                //Player will be damaged based on duration
                if (hasIFrames)
                {
                    if (IFramesTimer >= HitFrames) stateMachine.Damaged = false;
                    IFramesTimer += gt.ElapsedGameTime.TotalSeconds;
                    if (IFramesTimer >= IFrames)
                    {
                        IFramesTimer = 0;
                        hasIFrames = false;
                    }
                }
            }
            else
            {
                stateMachine.KillPlayer();
            }
            PreviousPosition = Position;
            stateMachine.Update(gt);
            toolUsed.Update(gt);
            Hitbox.BoundingBox = new Rectangle((int)Position.X - Hitbox.BoundingBox.Width/2, (int)Position.Y - Hitbox.BoundingBox.Height / 4, Hitbox.BoundingBox.Width, Hitbox.BoundingBox.Height);
            DisabledDirections.Clear();
            MovingDirections.Clear();
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
                stateMachine.ChangeDirection(Direction.Left);
                MovingDirections.Add(Direction.Left);
                if (MovingDirections.Count > 1) NextPosition.X -= (float)(WalkSpeed / Math.Sqrt(2));//normalize diagonal movement
                else NextPosition.X -= WalkSpeed;
            }
        }
        public void MoveRight()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Right))
            {
                stateMachine.ChangeDirection(Direction.Right);
                MovingDirections.Add(Direction.Right);
                if (MovingDirections.Count > 1) NextPosition.X += (float)(WalkSpeed / Math.Sqrt(2));
                else NextPosition.X += WalkSpeed;
            }
        }
        public void MoveUp()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Up))
            {
                stateMachine.ChangeDirection(Direction.Up);
                MovingDirections.Add(Direction.Up);
                if (MovingDirections.Count > 1) NextPosition.Y -= (float)(WalkSpeed / Math.Sqrt(2));
                else NextPosition.Y -= WalkSpeed;
            }
        }
        public void MoveDown()
        {
            if (stateMachine.CanMove && !DisabledDirections.Contains(Direction.Down))
            {
                stateMachine.ChangeDirection(Direction.Down);
                MovingDirections.Add(Direction.Down);
                if (MovingDirections.Count > 1) NextPosition.Y += (float)(WalkSpeed / Math.Sqrt(2));
                else NextPosition.Y += WalkSpeed;
            }
        }

        public void TakeDamage(int attackPoints)
        {
            if (!stateMachine.Damaged && !hasIFrames)
            {
                Health = Health - attackPoints;
                if (Health < 1)
                {
                    Health = 0;
                    playerDead = true;
                    stateMachine.KillPlayer();                    
                }
                stateMachine.Damaged = true;
                hasIFrames = true;
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

        public void Teleport(Vector2 target)
        {
            Position = target;
            NextPosition = target;      
            PreviousPosition = target;
            Hitbox.BoundingBox = new Rectangle((int)Position.X - Hitbox.BoundingBox.Width / 2, (int)Position.Y - Hitbox.BoundingBox.Height / 4, Hitbox.BoundingBox.Width, Hitbox.BoundingBox.Height);
        }
    }
}