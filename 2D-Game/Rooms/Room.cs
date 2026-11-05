using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace _2D_Game
{
    public class Room
    {
        public Point GridPosition { get; private set; }
        public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();
        public List<IEnemy> Enemies { get; private set; } = new List<IEnemy>(); // Room-specific enemies
        public List<INPC> NPCs { get; private set; } = new List<INPC>(); // Room-specific NPCs
        public List<WorldItem> Items { get; private set; } = new List<WorldItem>();
        public SongName RoomSongName { get; private set; }
        
        public Room(Point gridPosition)
        {
            GridPosition = gridPosition;
            DetermineRoomSongName(gridPosition);//TODO maybe just load this in via the xml stuff?

        }
        private void DetermineRoomSongName(Point gridPos) {
            int row1 = 0, col1 = 0;
            int row2 = 1, col2 = 1;
            int row3=2, col3=2;
            int xCoord = gridPos.X, yCoord = gridPos.Y;
            RoomSongName = SongName.Lost;
            if (xCoord == col1) 
            {
                if (yCoord == row1) RoomSongName = SongName.StarterRoomTheme; //Room 2
                else if (yCoord == row2) RoomSongName = SongName.StarterRoomTheme; //Room 1
                else if (yCoord == row3) RoomSongName = SongName.CozyFire; //Room 0
            }
            else if (xCoord == col2) {
                if (yCoord == row1) RoomSongName = SongName.OminousLair; //Room 3
                else if (yCoord == row2) RoomSongName = SongName.TitleTheme; //Room 6, song TBC
                else if (yCoord == row3) RoomSongName = SongName.EndTheme; //Room 7, song TBC
            }
            else if (xCoord == col3)
            {
                if (yCoord == row1) RoomSongName = SongName.CozyFireSR; //Room 4, song TBC
                else if (yCoord == row2) RoomSongName = SongName.TheDead; //Room 5, song TBC
                else if (yCoord == row3) RoomSongName = SongName.Lost; //Room 8, song TBC
            }
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
        /// Turns on Enemy AI, and room song
        /// </summary>
        public void Activate()   { 
            foreach (var e in Enemies) e.Load();
            Playlist.Instance.ChangeSong(RoomSongName, true);
        }
        /// <summary>
        /// Turns off Enemy AI
        /// </summary>
        public void Deactivate() { 
            foreach (var e in Enemies) e.Unload();
            Playlist.Instance.ChangeSong(RoomSongName, true); //RoomSongName should be an empty string (no song will play)
        }

        //TODO music changing methods         
    }
}