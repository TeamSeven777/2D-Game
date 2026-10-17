using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DeadPlayerState : IPlayerState
    {
        public ISprite PlayerSprite { get; set; }
        private PlayerStateMachine stateMachine;
        public DeadPlayerState(PlayerStateMachine myPlayerStateMachine)
        {
            stateMachine = myPlayerStateMachine;
            PlayerSprite = PlayerSpriteFactory.Instance.CreateDamagedPlayerSprite(stateMachine.direction);
        }
        public void Update(GameTime gameTime)
        {
            PlayerSprite.Update(gameTime);
            if (((AnimatedSprite)PlayerSprite).Paused)
            {
                //TODO add player death animation
                stateMachine.KillPlayer();
            }
        }
        public void Draw(SpriteBatch sprBatch, Vector2 pos)
        {
            PlayerSprite.Draw(sprBatch, pos);
        }

    }
}
