using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class SlingPlayerState : IPlayerState
    {
        public ISprite PlayerSprite { get; set; }
        private PlayerStateMachine stateMachine;
        private double stateTime = 0;
        private double timeSling = 0.5;
        public SlingPlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            
            stateMachine = myPlayerStateMachine;
            switch (stateMachine.direction)
            {
                case Direction.Down:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(107, 10, 17, 17), new Vector2(4.0f));
                    break;
                case Direction.Up:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(140, 10, 17, 17), new Vector2(4.0f));
                    break;
                case Direction.Left:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f), SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new Sprite(stateMachine.spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f));
                    break;
            }
        }
        public void Update(GameTime gameTime)
        {
            PlayerSprite.Update(gameTime);
            stateTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (stateTime >= timeSling) stateMachine.UseTool(Tool.None);
        }
        public void Draw(SpriteBatch sprBatch, Vector2 pos)
        {
            PlayerSprite.Draw(sprBatch, pos);
        }

    }
}
