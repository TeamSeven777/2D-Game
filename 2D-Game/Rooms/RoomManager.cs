using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class RoomManager
    {
        private readonly Dictionary<Point, Room> rooms = new();
        public Room CurrentRoom { get; private set; }
        
        private int roomWidth;
        private int roomHeight;

        // Use factory class
        private readonly RoomContentFactory contentFactory = new();

        /// <summary>
        /// Reads an XML file and creates room structures that contain information about what to spawn
        /// with a maximum size of <paramref name="height"/> x <paramref name="width"/>
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="blockSheet"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void LoadDungeon(string filePath, ContentManager content, Texture2D blockSheet, int width, int height)
        {
            roomWidth = width;
            roomHeight = height;

            // Load assets via factory
            contentFactory.LoadContent(content, blockSheet);

            XDocument doc = XDocument.Load(filePath);

            // Parse XML document to load rooms and their contents one-by-one
            foreach (var roomElement in doc.Descendants("Room"))
            {
                int rx = ParseIntAttribute(roomElement, "RoomX", 0);
                int ry = ParseIntAttribute(roomElement, "RoomY", 0);
                Point gridPos = new(rx, ry);

                Room room = new(gridPos);

                ParseObstacles(roomElement, rx, ry, room);
                ParseEnemies(roomElement, rx, ry, room);
                ParseNPCs(roomElement, rx, ry, room);
                ParseItems(roomElement, rx, ry, room);

                rooms[gridPos] = room;
            }

            // Set initial starting room (bottom-left-most room is [0,2])
            Point startGrid = new(0, 2);
            CurrentRoom = rooms.ContainsKey(startGrid) ? rooms[startGrid] : null;

            foreach (var room in rooms.Values)
            {
                if (room != CurrentRoom) room.Deactivate();
            }
        }

        // XML Parsing Helpers:

        private Vector2 CalculateWorldPosition(XElement element, int rx, int ry)
        {
            float x = ParseFloatAttribute(element, "X", 0f);
            float y = ParseFloatAttribute(element, "Y", 0f);
            return new Vector2((rx * roomWidth) + x, (ry * roomHeight) + y);
        }

        private static int ParseIntAttribute(XElement element, string attrName, int defaultValue)
        {
            return int.TryParse(element.Attribute(attrName)?.Value, out int val) ? val : defaultValue;
        }

        private static float ParseFloatAttribute(XElement element, string attrName, float defaultValue)
        {
            return float.TryParse(element.Attribute(attrName)?.Value, out float val) ? val : defaultValue;
        }

        /// <summary>
        /// Creates the respecitve obstacle object for every element in <paramref name="roomElement"/>
        /// </summary>
        /// <param name="roomElement"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="room"></param>
        private void ParseObstacles(XElement roomElement, int rx, int ry, Room room)
        {
            var obstaclesElement = roomElement.Element("Obstacles");
            if (obstaclesElement == null) return;

            foreach (var obsElement in obstaclesElement.Elements("Entity"))
            {
                string type = obsElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(obsElement, rx, ry);
                Obstacle block = (Obstacle)contentFactory.CreateBlock(type, worldPos);
                room.Obstacles.Add(block);
            }
        }
        /// <summary>
        /// Creates the respecitve enemy object for every element in <paramref name="roomElement"/>
        /// </summary>
        /// <param name="roomElement"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="room"></param>
        private void ParseEnemies(XElement roomElement, int rx, int ry, Room room)
        {
            var enemiesElement = roomElement.Element("Enemies");
            if (enemiesElement == null) return;

            foreach (var enemyElement in enemiesElement.Elements("Entity"))
            {
                string type = enemyElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(enemyElement, rx, ry);

                IEnemy enemy = contentFactory.CreateEnemy(type, worldPos);
                if (enemy != null)
                {
                    room.Enemies.Add(enemy);
                }
            }
        }
        /// <summary>
        /// Creates the respecitve NPC object for every element in <paramref name="roomElement"/>
        /// </summary>
        /// <param name="roomElement"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="room"></param>
        private void ParseNPCs(XElement roomElement, int rx, int ry, Room room)
        {
            var npcsElement = roomElement.Element("NPCs");
            if (npcsElement == null) return;

            foreach (var npcElement in npcsElement.Elements("Entity"))
            {
                string type = npcElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(npcElement, rx, ry);

                INPC npc = contentFactory.CreateNPC(type, worldPos);
                if (npc != null)
                {
                    room.NPCs.Add(npc);
                }
            }
        }

        /// <summary>
        /// Creates the respecitve Item object for every element in <paramref name="roomElement"/>
        /// </summary>
        /// <param name="roomElement"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="room"></param>
        private void ParseItems(XElement roomElement, int rx, int ry, Room room)
        {
            var itemsElement = roomElement.Element("Items");
            if (itemsElement == null) return;

            foreach (var itemElement in itemsElement.Elements("Entity"))
            {
                string type = itemElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(itemElement, rx, ry);

                IItem itemData = contentFactory.CreateItem(type);
                if (itemData != null)
                {
                    room.Items.Add(new WorldItem(itemData, worldPos));
                }
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            int gridX = (int)(playerPosition.X / roomWidth);
            int gridY = (int)(playerPosition.Y / roomHeight);
            Point currentGridPos = new(gridX, gridY);

            if (rooms.ContainsKey(currentGridPos) && rooms[currentGridPos] != CurrentRoom)
            {
                CurrentRoom?.Deactivate();
                CurrentRoom = rooms[currentGridPos];
                CurrentRoom.Activate();
            }

            CurrentRoom?.Update(gameTime, playerPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var room in rooms.Values)
            {
                room.Draw(spriteBatch);
            }
        }
    }
}