using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class IdlePlayerState : IPlayerState
    {
        public ISprite PlayerSprite { get; set; }
        private PlayerStateMachine stateMachine;
        public IdlePlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            stateMachine = myPlayerStateMachine;
            switch (stateMachine.direction)
            {
                case Direction.Down:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(500), new Vector2(32*16, 32*16), new Vector2(0, 96*16), 2048, true), myPlayerStateMachine.spriteSheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(500), new Vector2(32 * 16, 32 * 16), new Vector2(0, 160*16), 2048, true), myPlayerStateMachine.spriteSheet);
                    break;
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(500), new Vector2(32 * 16, 32 * 16), new Vector2(0, 128*16), 2048, true), myPlayerStateMachine.spriteSheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(500), new Vector2(32 * 16, 32 * 16), new Vector2(0, 128*16), 2048, true), myPlayerStateMachine.spriteSheet);
                    break;
                    
            }
            PlayerSprite.Scale = new Vector2(0.25f);
        }
        public void Update(GameTime gameTime)
        {
            PlayerSprite.Update(gameTime);
        }
        public void Draw(SpriteBatch sprBatch, Vector2 pos)
        {
            PlayerSprite.Draw(sprBatch, pos);
        }

    }
}