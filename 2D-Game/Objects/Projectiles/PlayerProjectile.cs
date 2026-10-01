using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class PlayerProjectile : IProjectile
    {
        public Vector2 position { get; set; }

        public Vector2 travelingDirection { get; set; }

        public ISprite sprite { get; set; }

        public PlayerProjectile(Vector2 position, Vector2 travelingDirection, ISprite sprite)
        {
            this.position = position;
            this.travelingDirection = travelingDirection;
            this.sprite = sprite;
        }

        public void Update(GameTime gt)
        {
            position += travelingDirection;
        }

        public void Draw(SpriteBatch sprbatch)
        {
            sprite.Draw(sprbatch, position);
        }
    }
}
