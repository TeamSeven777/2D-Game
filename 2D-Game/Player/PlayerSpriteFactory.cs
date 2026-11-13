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
    public class PlayerSpriteFactory
{
        private Texture2D PlayerSpritesheet;
        private Texture2D Stick;
        private Texture2D SlingshotSprite;
        private int spriteSize = 32*16; //for player
        private TimeSpan FrameDelay;

        private static PlayerSpriteFactory instance = new PlayerSpriteFactory();

        public static PlayerSpriteFactory Instance
        {  get { return instance; } }

        private PlayerSpriteFactory() { }

        public void LoadTextures(ContentManager content)
        {
            PlayerSpritesheet = content.Load<Texture2D>("Sprites/Player_Sheet");
            Stick = content.Load<Texture2D>("Sprites/Stick");
            SlingshotSprite = content.Load<Texture2D>("Sprites/slingshot");
        }

        //idle animations
        public ISprite CreateIdlePlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(500);
            switch (direction)
            {
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 4 + 1), PlayerSpritesheet.Width, true), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 4 + 1), PlayerSpritesheet.Width, true), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 5 + 1), PlayerSpritesheet.Width, true), PlayerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 3 + 1), PlayerSpritesheet.Width, true), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        //moving animations
        public ISprite CreateMovingPlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(125);
            switch (direction)
            {                
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize + 1), PlayerSpritesheet.Width), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize + 1), PlayerSpritesheet.Width), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 2 + 1), PlayerSpritesheet.Width), PlayerSpritesheet); //ex. this animation is on the third row (spriteSize * 2 + 1)
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 0), PlayerSpritesheet.Width), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        //throwing animations
        public ISprite CreateThrowingPlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(125);
            switch (direction)
            {                
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 16 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 16 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 17 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 15 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        public ISprite CreateInteractingPlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(125);
            switch (direction)
            {
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 7 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 7 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 8 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 6 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        public ISprite CreateAttackingPlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(125);
            switch (direction)
            {
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 10 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 10 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 11 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 9 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        public ISprite CreateDamagedPlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(125);
            switch (direction)
            {
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 13 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 13 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 14 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(PlayerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 12 + 1), PlayerSpritesheet.Width, false), PlayerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
        public ISprite CreateToolAttack(Tool tool, Direction direction)
        {
            ISprite ToolSprite;
            float rotationStatingOffset = 20;
            float rotationEndingOffset = 20;
            float rotationSpeed = .2f;
            Vector2 scale = new Vector2(.75f);
            //TODO make work
            switch (direction)
            {
                case Direction.Left:
                    ToolSprite = new RotatingSprite(Stick, scale, SpriteEffects.None, - rotationStatingOffset, -180f + rotationEndingOffset, rotationSpeed, false, false); //CC, Left attack
                    break;
                case Direction.Right:
                    ToolSprite = new RotatingSprite(Stick, scale, SpriteEffects.None, rotationStatingOffset, 180f - rotationEndingOffset, rotationSpeed, true, false); //C, Right attack
                    break;
                case Direction.Up:
                    ToolSprite = new RotatingSprite(Stick, scale, SpriteEffects.None, - 90+rotationStatingOffset, 90f - rotationEndingOffset, rotationSpeed, true, false); //C, Up attack
                    break;
                default: //down
                    ToolSprite = new RotatingSprite(Stick, scale, SpriteEffects.None, - 90 - rotationStatingOffset, -270f + rotationEndingOffset, rotationSpeed, false, false); //CC, down attack
                    break;
            }
            
            //
            return ToolSprite;
        }

        public ISprite CreateTool(Tool tool)
        {
            ISprite ToolSprite;
            switch (tool)
            {
                case (Tool.Slingshot):
                    ToolSprite = new Sprite(SlingshotSprite, SlingshotSprite.Bounds);
                    break;
                default : ToolSprite = null; break;
            }
            return ToolSprite;
        }
    }
}
