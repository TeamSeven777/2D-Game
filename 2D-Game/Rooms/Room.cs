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
        public List<IItem> Items { get; private set; } = new List<IItem>();

        public Room(Point gridPosition)
        {
            GridPosition = gridPosition;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            foreach (var obstacle in Obstacles) 
                obstacle.Update(gameTime);

            foreach (var enemy in Enemies) 
                enemy.Update(gameTime, playerPosition);

            // TODO: items
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in Obstacles) 
                obstacle.Draw(spriteBatch);

            foreach (var enemy in Enemies) 
                enemy.Draw(spriteBatch);

            // TODO: items
        }
    }
}