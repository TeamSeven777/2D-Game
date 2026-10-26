using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _2D_Game
{
    public class Room
    {
        public Point GridPosition { get; private set; }
        public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();
        public List<IEnemy> Enemies { get; private set; } = new List<IEnemy>(); // Room-specific enemies
        public List<INPC> NPCs { get; private set; } = new List<INPC>(); // Room-specific NPCs
        public List<WorldItem> Items { get; private set; } = new List<WorldItem>();
        
        
        public Room(Point gridPosition)
        {
            GridPosition = gridPosition;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            foreach (var obstacle in Obstacles) 
                obstacle.Update(gameTime);

            for (int i = Enemies.Count - 1; i >= 0; i--) 
            {
                Enemies[i].Update(gameTime, playerPosition);
                if (Enemies[i].Health <= 0)
                {
                    Enemies[i].Unload();  
                    Enemies.RemoveAt(i);
                }
            }
            
            foreach (var npc in NPCs) 
                npc.Update(gameTime);

            foreach (var item in Items) 
                item.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in Obstacles) 
                obstacle.Draw(spriteBatch);

            foreach (var enemy in Enemies) 
                enemy.Draw(spriteBatch);

            foreach (var npc in NPCs)
                npc.Draw(spriteBatch);

            foreach (var item in Items) 
                item.Draw(spriteBatch);
        }
        /// <summary>
        /// Turns on Enemy AI
        /// </summary>
        public void Activate()   { foreach (var e in Enemies) e.Load(); }
        /// <summary>
        /// Turns off Enemy AI
        /// </summary>
        public void Deactivate() { foreach (var e in Enemies) e.Unload(); }
    }
}