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
        private Texture2D playerSpritesheet;
        private int spriteSize = 32*16;
        private TimeSpan FrameDelay;

        private static PlayerSpriteFactory instance = new PlayerSpriteFactory();

        public static PlayerSpriteFactory Instance
        {  get { return instance; } }

        private PlayerSpriteFactory() { }

        public void LoadTextures(ContentManager content)
        {
            playerSpritesheet = content.Load<Texture2D>("Sprites/Player_Sheet");
        }

        //idle animations
        public ISprite CreateIdlePlayerSprite(Direction direction)
        {
            ISprite PlayerSprite;
            FrameDelay = TimeSpan.FromMilliseconds(500);
            switch (direction)
            {
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 4 + 1), playerSpritesheet.Width, true), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 4 + 1), playerSpritesheet.Width, true), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 5 + 1), playerSpritesheet.Width, true), playerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 3 + 1), playerSpritesheet.Width, true), playerSpritesheet);
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
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize + 1), playerSpritesheet.Width), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize + 1), playerSpritesheet.Width), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 2 + 1), playerSpritesheet.Width), playerSpritesheet); //ex. this animation is on the third row (spriteSize * 2 + 1)
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 0), playerSpritesheet.Width), playerSpritesheet);
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
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 16 + 1), playerSpritesheet.Width, false), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 16 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 17 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 15 + 1), playerSpritesheet.Width, false), playerSpritesheet);
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
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 7 + 1), playerSpritesheet.Width, false), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 7 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 8 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 6 + 1), playerSpritesheet.Width, false), playerSpritesheet);
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
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 10 + 1), playerSpritesheet.Width, false), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 10 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 11 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 9 + 1), playerSpritesheet.Width, false), playerSpritesheet);
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
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 13 + 1), playerSpritesheet.Width, false), playerSpritesheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 13 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 14 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
                default: //down
                    PlayerSprite = new AnimatedSprite(new Animation(playerSpritesheet, FrameDelay, new Vector2(spriteSize, spriteSize), new Vector2(0, spriteSize * 12 + 1), playerSpritesheet.Width, false), playerSpritesheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(0.25f);
            return PlayerSprite;
        }
    }
}
