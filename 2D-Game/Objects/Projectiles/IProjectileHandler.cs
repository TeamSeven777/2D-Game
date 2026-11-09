using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public interface IProjectileHandler
    {
        public List<IHitboxOwner> InActiveProjectiles { get; set; }
        public List<IHitboxOwner> ActiveProjectiles { get; set; }
        public GameTime GameTime { get; set; }

        public void Update(GameTime gt);

        public void Draw(SpriteBatch spr);

        public void Add();

        public void Remove(IHitboxOwner x);

        public void Clear();
    }
}
