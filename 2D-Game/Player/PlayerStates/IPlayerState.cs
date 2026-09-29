using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IPlayerState
{
        public ISprite PlayerSprite { get; set; }
       // float GetStateTime();//TODO use to define looping, single-time, and no animation time
        void Update(GameTime gameTime);
        void Draw(SpriteBatch sprBatch, Vector2 pos);
}
}
