using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class UpSlingPlayerState : IPlayerState
    {
        public ISprite PlayerSprite {get; set;}
        private PlayerStateMachine stateMachine;
        private double stateTime = 0;
        private double timeSling = 0.5;
        public UpSlingPlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            PlayerSprite = new Sprite(myPlayerStateMachine.spriteSheet, new Rectangle(140, 10, 17, 17), new Vector2(4.0f));
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
