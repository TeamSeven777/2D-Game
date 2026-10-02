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
    public interface IProjectile
    {
        public Vector2 Position { get; set; }

        public Vector2 TravelingDirection { get; set; }

        public ISprite ProjectileSprite { get; set; }
        public float DespawnTimer { get; set; }

        public void Update(GameTime gt);

        public void Draw(SpriteBatch sprBatch);

    }
}
