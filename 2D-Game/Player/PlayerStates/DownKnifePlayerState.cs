using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DownKnifePlayerState : IPlayerState
    {
        public ISprite PlayerSprite {get; set;}
        private PlayerStateMachine stateMachine;
        public DownKnifePlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            PlayerSprite = new AnimatedSprite(new Animation(myPlayerStateMachine.spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(94, 47), 68, false), myPlayerStateMachine.spriteSheet);
            PlayerSprite.Scale = new Vector2(4.0f);
            stateMachine = myPlayerStateMachine;
        }
        public void Update(GameTime gameTime)
        {
            PlayerSprite.Update(gameTime);
            if (((AnimatedSprite)PlayerSprite).Paused)
            {
                stateMachine.UseTool(Tool.None);
            }
        }
        public void Draw(SpriteBatch sprBatch, Vector2 pos)
        {
            PlayerSprite.Draw(sprBatch, pos);
        }
    
    }
}
