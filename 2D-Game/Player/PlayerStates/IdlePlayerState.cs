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
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(0, 10, 17, 17), new Vector2(4.0f));
                    break;
                case Direction.Up: 
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(68, 10, 17, 17), new Vector2(4.0f));
                    break;
                case Direction.Left:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(34, 10, 17, 17), new Vector2(4.0f), SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(34, 10, 17, 17), new Vector2(4.0f));
                    break;
            }
            
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