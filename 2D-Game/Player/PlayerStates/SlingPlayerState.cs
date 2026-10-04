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
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(32, 32), new Vector2(0, 192), 128, false), myPlayerStateMachine.spriteSheet);
                    break;
                case Direction.Up:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(32, 32), new Vector2(0, 256), 128, false), myPlayerStateMachine.spriteSheet);
                    break;
                case Direction.Left:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(32, 32), new Vector2(0, 224), 128, false), myPlayerStateMachine.spriteSheet, SpriteEffects.FlipHorizontally);
                    break;
                case Direction.Right:
                    PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(32, 32), new Vector2(0, 224), 128, false), myPlayerStateMachine.spriteSheet);
                    break;
            }
            PlayerSprite.Scale = new Vector2(4.0f);

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
