using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2D_Game
{
    public class RoomData
    {
        public int RoomX { get; set; }
        public int RoomY { get; set; }
        public List<EntitySpawnData> Enemies { get; set; } = new List<EntitySpawnData>();
        public List<EntitySpawnData> Obstacles { get; set; } = new List<EntitySpawnData>();
        public List<EntitySpawnData> Items { get; set; } = new List<EntitySpawnData>();
    }

    public class EntitySpawnData
    {
        public string Type { get; set; } // ex: "Log", "Block", "Key", "Ghost"

        // Coordinate position
        public float X { get; set; }
        public float Y { get; set; }
    }
}