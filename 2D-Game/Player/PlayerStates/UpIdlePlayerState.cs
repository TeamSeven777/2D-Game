using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class UpIdlePlayerState : IPlayerState
    {
        public ISprite PlayerSprite {get; set;}
        public UpIdlePlayerState(Texture2D spriteSheet)
        {
            PlayerSprite = new Sprite(spriteSheet, new Rectangle(68, 10, 17, 17), new Vector2(4.0f));
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
