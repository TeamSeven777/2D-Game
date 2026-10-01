using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DownSlingPlayerState : IPlayerState
    {
        public ISprite PlayerSprite {get; set;}
        private PlayerStateMachine stateMachine;
        private double stateTime = 0;
        private double timeSling = 0.5;
        public DownSlingPlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            PlayerSprite = new Sprite(myPlayerStateMachine.spriteSheet, Vector2.Zero, new Vector2(4.0f), Color.White, new Rectangle(107, 10, 17, 17));
            stateMachine = myPlayerStateMachine;
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
