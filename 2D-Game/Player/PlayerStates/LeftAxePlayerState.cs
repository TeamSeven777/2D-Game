using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class LeftAxePlayerState : IPlayerState
    {
        public ISprite PlayerSprite {get; set;}
        public LeftAxePlayerState(Texture2D spriteSheet)
        {
            PlayerSprite = new AnimatedSprite(new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(0, 78), 68), spriteSheet, SpriteEffects.FlipHorizontally);
            PlayerSprite.Scale = new Vector2(4.0f);
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
