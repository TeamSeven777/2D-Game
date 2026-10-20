using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_Game
{
    public class RoomManager
    {
        private readonly Dictionary<Point, Room> rooms = new();
        public Room CurrentRoom { get; private set; }
        
        private int roomWidth;
        private int roomHeight;

        // Assets
        private Texture2D ghostSheet, ghostAttackSheet, mosquitoSheet, spiderSheet;
        private readonly Dictionary<string, Texture2D> itemTextures = new(StringComparer.OrdinalIgnoreCase);
        private Texture2D blockSpriteSheet;

        public void LoadContent(ContentManager content)
        {
            // Pre-load enemy sprite sheets
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
            spiderSheet = content.Load<Texture2D>("Sprites/Spider");

            // Pre-load item textures (lookup dictionary)
            itemTextures["Berry"] = content.Load<Texture2D>("Images/Berry2");
            itemTextures["Explosive"] = content.Load<Texture2D>("Images/Explosive2");
            itemTextures["Log"] = content.Load<Texture2D>("Images/Log2");
            itemTextures["Key"] = content.Load<Texture2D>("Images/Key2");
            itemTextures["Pebble"] = content.Load<Texture2D>("Images/Pebble2");
        }

        public void LoadDungeon(string filePath, ContentManager content, Texture2D blockSheet, int width, int height)
        {
            roomWidth = width;
            roomHeight = height;
            blockSpriteSheet = blockSheet;

            LoadContent(content);

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

        private void ParseObstacles(XElement roomElement, int rx, int ry, Room room)
        {
            var obstaclesElement = roomElement.Element("Obstacles");
            if (obstaclesElement == null) return;

            foreach (var obsElement in obstaclesElement.Elements("Entity"))
            {
                Vector2 worldPos = CalculateWorldPosition(obsElement, rx, ry);

                Queue<Sprite> blockSpriteSet = new();
                blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));

                Obstacle block = new(new Vector2(25), worldPos, blockSpriteSet);
                room.Obstacles.Add(block);
            }
        }

        private void ParseEnemies(XElement roomElement, int rx, int ry, Room room)
        {
            var enemiesElement = roomElement.Element("Enemies");
            if (enemiesElement == null) return;

            foreach (var enemyElement in enemiesElement.Elements("Entity"))
            {
                string type = enemyElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(enemyElement, rx, ry);

                IEnemy enemy = CreateEnemy(type, worldPos);
                if (enemy != null)
                {
                    room.Enemies.Add(enemy);
                }
            }
        }

        private IEnemy CreateEnemy(string type, Vector2 position)
        {
            if (type.Equals("Ghost", StringComparison.OrdinalIgnoreCase))
            {
                Animation ghostIdle = new(ghostSheet, TimeSpan.FromMilliseconds(100), new Vector2(256, 256), Vector2.Zero, ghostSheet.Width);
                AnimatedSprite ghostSpr = new(ghostIdle, ghostSheet) { Scale = new Vector2(0.5f) };

                Animation ghostAttack = new(ghostAttackSheet, TimeSpan.FromMilliseconds(50), new Vector2(256, 256), Vector2.Zero, ghostAttackSheet.Width);
                AnimatedSprite ghostAttackSpr = new(ghostAttack, ghostAttackSheet) { Scale = new Vector2(0.5f) };

                return new Ghost(ghostSpr, ghostAttackSpr, position);
            }
            else if (type.Equals("Mosquito", StringComparison.OrdinalIgnoreCase))
            {
                Animation mosquitoFly = new(mosquitoSheet, TimeSpan.FromMilliseconds(80), new Vector2(96, 96), new Vector2(0, 96), mosquitoSheet.Width);
                AnimatedSprite mosquitoSpr = new(mosquitoFly, mosquitoSheet) { Scale = Vector2.One };

                return new Mosquito(mosquitoSpr, position);
            }
            else if (type == "Spider")
            {
                Animation spiderIdle = new Animation(spiderSheet, TimeSpan.FromMilliseconds(80), new Vector2(64, 64), new Vector2(0, 64), spiderSheet.Width);
                AnimatedSprite spiderSpr = new AnimatedSprite(spiderIdle, spiderSheet) { Scale = Vector2.One };
                        
                return new Spider(spiderSpr, position);
            }

            return null;
        }

        private void ParseItems(XElement roomElement, int rx, int ry, Room room)
        {
            var itemsElement = roomElement.Element("Items");
            if (itemsElement == null) return;

            foreach (var itemElement in itemsElement.Elements("Entity"))
            {
                string type = itemElement.Attribute("Type")?.Value ?? string.Empty;
                Vector2 worldPos = CalculateWorldPosition(itemElement, rx, ry);

                IItem itemData = CreateItem(type);
                if (itemData != null)
                {
                    room.Items.Add(new WorldItem(itemData, worldPos));
                }
            }
        }

        private IItem CreateItem(string type)
        {
            if (!itemTextures.TryGetValue(type, out Texture2D texture) || texture == null)
                return type.Equals("Pebble", StringComparison.OrdinalIgnoreCase) ? null : new ExplosiveItem(null); // Fallback or specific mapping

            Sprite sprite = new(texture, new Rectangle(0, 0, texture.Width, texture.Height));

            return type switch
            {
                "Berry" => new BerryItem(sprite),
                "Explosive" => new ExplosiveItem(sprite),
                "Log" => new LogItem(sprite),
                "Key" => new KeyItem(sprite),
                "Pebble" => new PebbleItem(sprite),
                _ => null
            };
        }




        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Determine active room based on player's world coords
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
            // Draw all loaded rooms, makes everything visible across the world
            foreach (var room in rooms.Values)
            {
                room.Draw(spriteBatch);
            }
        }
    }
}
