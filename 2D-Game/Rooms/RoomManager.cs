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
        private Dictionary<Point, Room> rooms = new Dictionary<Point, Room>();
        public Room CurrentRoom { get; private set; }
        
        private int roomWidth;
        private int roomHeight;

        // Textures for enemy creation during room loading
        private Texture2D ghostSheet, ghostAttackSheet;
        private Texture2D mosquitoSheet;

        // Textures for item creation during room loading
        private Texture2D berryTexture, explosiveTexture, logTexture, keyTexture, pebbleTexture;


        public void LoadContent(ContentManager content)
        {
            // Pre-load enemy sprite sheets so rooms can instantiate them
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");

            // Pre-load item sprites so rooms can instantiate them
            berryTexture = content.Load<Texture2D>("Images/Berry2");
            explosiveTexture = content.Load<Texture2D>("Images/Explosive2");
            logTexture = content.Load<Texture2D>("Images/Log2");
            keyTexture = content.Load<Texture2D>("Images/Key2");
            pebbleTexture = content.Load<Texture2D>("Images/Pebble2");
        }

        public void LoadDungeon(string filePath, ContentManager content, Texture2D blockSpriteSheet, int roomWidth, int roomHeight)
        {
            this.roomWidth = roomWidth;
            this.roomHeight = roomHeight;

            LoadContent(content);

            XDocument doc = XDocument.Load(filePath);

            // Load all obstacles, enemies, and items room by room
            foreach (var roomElement in doc.Descendants("Room"))
            {
                int rx = int.Parse(roomElement.Attribute("RoomX").Value);
                int ry = int.Parse(roomElement.Attribute("RoomY").Value);
                Point gridPos = new Point(rx, ry);

                Room room = new Room(gridPos);

                // Load Obstacles
                ParseObstaclesFromXML(blockSpriteSheet, roomWidth, roomHeight, roomElement, rx, ry, room);
                // Load Enemies
                ParseEnemiesFromXML(roomWidth, roomHeight, roomElement, rx, ry, room);
                // Load Items
                ParseItemsFromXML(roomWidth, roomHeight, roomElement, rx, ry, room);

                rooms[gridPos] = room;
            }

            // Set initial starting room (bottom-left-most room is [0,2])
            Point startGrid = new Point(0, 2);
            CurrentRoom = rooms.ContainsKey(startGrid) ? rooms[startGrid] : null;
        
            foreach (var room in rooms.Values)
            {
            if (room != CurrentRoom) room.Deactivate();
            }
        }

        private static void ParseObstaclesFromXML(Texture2D blockSpriteSheet, int roomWidth, int roomHeight, XElement roomElement, int rx, int ry, Room room)
        {
            var obstaclesElement = roomElement.Element("Obstacles");
            if (obstaclesElement != null)
            {
                foreach (var obsElement in obstaclesElement.Elements("Entity"))
                {
                    float x = float.Parse(obsElement.Attribute("X").Value);
                    float y = float.Parse(obsElement.Attribute("Y").Value);
                    Vector2 worldPos = new Vector2((rx * roomWidth) + x, (ry * roomHeight) + y);

                    Queue<Sprite> blockSpriteSet = new Queue<Sprite>();
                    blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));

                    Obstacle block = new Obstacle(new Vector2(25), worldPos, blockSpriteSet);
                    room.Obstacles.Add(block);
                }
            }
        }

        private void ParseEnemiesFromXML(int roomWidth, int roomHeight, XElement roomElement, int rx, int ry, Room room)
        {
            var enemiesElement = roomElement.Element("Enemies");
            if (enemiesElement != null)
            {
                foreach (var enemyElement in enemiesElement.Elements("Entity"))
                {
                    string type = enemyElement.Attribute("Type")?.Value;
                    float x = float.Parse(enemyElement.Attribute("X").Value);
                    float y = float.Parse(enemyElement.Attribute("Y").Value);
                    Vector2 worldPos = new Vector2((rx * roomWidth) + x, (ry * roomHeight) + y);

                    // TODO: refactor to utilize the EnemyManager script instead !!!

                    if (type == "Ghost")
                    {
                        Animation ghostIdle = new Animation(ghostSheet, TimeSpan.FromMilliseconds(100), new Vector2(256, 256), Vector2.Zero, ghostSheet.Width);
                        AnimatedSprite ghostSpr = new AnimatedSprite(ghostIdle, ghostSheet);
                        ghostSpr.Scale = new Vector2(0.5f);

                        Animation ghostAttack = new Animation(ghostAttackSheet, TimeSpan.FromMilliseconds(50), new Vector2(256, 256), Vector2.Zero, ghostAttackSheet.Width);
                        AnimatedSprite ghostAttackSpr = new AnimatedSprite(ghostAttack, ghostAttackSheet);
                        ghostAttackSpr.Scale = new Vector2(0.5f);

                        Ghost ghost = new Ghost(ghostSpr, ghostAttackSpr, worldPos);
                        room.Enemies.Add(ghost);
                    }
                    else if (type == "Mosquito")
                    {
                        Animation mosquitoFly = new Animation(mosquitoSheet, TimeSpan.FromMilliseconds(80), new Vector2(96, 96), new Vector2(0, 96), mosquitoSheet.Width);
                        AnimatedSprite mosquitoSpr = new AnimatedSprite(mosquitoFly, mosquitoSheet);
                        mosquitoSpr.Scale = Vector2.One;

                        Mosquito mosquito = new Mosquito(mosquitoSpr, worldPos);
                        room.Enemies.Add(mosquito);
                    }
                }
            }
        }



        private void ParseItemsFromXML(int roomWidth, int roomHeight, XElement roomElement, int rx, int ry, Room room)
        {
            // Load items from XML
            var itemsElement = roomElement.Element("Items");
            if (itemsElement != null)
            {
                foreach (var itemElement in itemsElement.Elements("Entity"))
                {
                    string type = itemElement.Attribute("Type")?.Value ?? string.Empty;
                    float x = float.TryParse(itemElement.Attribute("X")?.Value, out float px) ? px : 0f;
                    float y = float.TryParse(itemElement.Attribute("Y")?.Value, out float py) ? py : 0f;
                    Vector2 worldPos = new Vector2((rx * roomWidth) + x, (ry * roomHeight) + y);

                    IItem itemData = null;
                    if (type.Equals("Berry", StringComparison.OrdinalIgnoreCase))
                    {
                        itemData = new BerryItem(new Sprite(berryTexture, new Rectangle(0, 0, berryTexture.Width, berryTexture.Height)));
                    }
                    else if (type.Equals("Explosive", StringComparison.OrdinalIgnoreCase))
                    {
                        itemData = new ExplosiveItem(new Sprite(explosiveTexture, new Rectangle(0, 0, explosiveTexture.Width, explosiveTexture.Height)));
                    }
                    else if (type.Equals("Log", StringComparison.OrdinalIgnoreCase))
                    {
                        itemData = new ExplosiveItem(new Sprite(logTexture, new Rectangle(0, 0, logTexture.Width, logTexture.Height)));
                    }
                    else if (type.Equals("Key", StringComparison.OrdinalIgnoreCase))
                    {
                        itemData = new ExplosiveItem(new Sprite(keyTexture, new Rectangle(0, 0, keyTexture.Width, keyTexture.Height)));
                    }
                    else if (type.Equals("Pebble", StringComparison.OrdinalIgnoreCase))
                    {
                        itemData = new ExplosiveItem(new Sprite(pebbleTexture, new Rectangle(0, 0, pebbleTexture.Width, pebbleTexture.Height)));
                    }


                    if (itemData != null)
                    {
                        room.Items.Add(new WorldItem(itemData, worldPos));
                    }
                }
            }
        }



        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Determine active room based on player's world coords
            int gridX = (int)(playerPosition.X / roomWidth);
            int gridY = (int)(playerPosition.Y / roomHeight);
            Point currentGridPos = new Point(gridX, gridY);

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
