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
        // TODO: refactor to utilize the EnemyManager script instead !!!
        private Texture2D ghostSheet;
        private Texture2D ghostAttackSheet;
        private Texture2D mosquitoSheet;

        public void LoadContent(ContentManager content)
        {
            // Pre-load enemy sprite sheets so rooms can instantiate them
            // TODO: refactor to utilize the EnemyManager script instead !!!
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
        }

        public void LoadDungeon(string filePath, ContentManager content, Texture2D blockSpriteSheet, int roomWidth, int roomHeight)
        {
            this.roomWidth = roomWidth;
            this.roomHeight = roomHeight;

            LoadContent(content);

            XDocument doc = XDocument.Load(filePath);

            foreach (var roomElement in doc.Descendants("Room"))
            {
                int rx = int.Parse(roomElement.Attribute("RoomX").Value);
                int ry = int.Parse(roomElement.Attribute("RoomY").Value);
                Point gridPos = new Point(rx, ry);

                Room room = new Room(gridPos);

                // Load Obstacles
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

                // Load Enemies from XML
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

                rooms[gridPos] = room;
            }

            // Set initial starting room (bottom-left-most room is [0,2])
            Point startGrid = new Point(0, 2);
            CurrentRoom = rooms.ContainsKey(startGrid) ? rooms[startGrid] : null;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Determine active room based on player's world coords
            int gridX = (int)(playerPosition.X / roomWidth);
            int gridY = (int)(playerPosition.Y / roomHeight);
            Point currentGridPos = new Point(gridX, gridY);

            if (rooms.ContainsKey(currentGridPos))
            {
                CurrentRoom = rooms[currentGridPos];
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