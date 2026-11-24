using _2D_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Threading;

namespace _2D_Game
{
    public class RoomContentFactory
    {
        // Assets
        private Texture2D ghostSheet, ghostAttackSheet, mosquitoSheet, spiderSheet;

        private Texture2D teamSheet, signTexture, textGraveTexture;
        public Texture2D FogTexture { get; private set; }
        private readonly Dictionary<string, Texture2D> itemTextures = new(StringComparer.OrdinalIgnoreCase);
        private readonly Random random = new();
        private Texture2D itemSheet;
        private Texture2D hotdogTexture, fishingrodTexture;
        private readonly Dictionary<string, Rectangle> itemTextureIInfo = new(StringComparer.OrdinalIgnoreCase);

        private static RoomContentFactory instance = new RoomContentFactory();

        public static RoomContentFactory Instance
        { get { return instance; } }

        private RoomContentFactory() { }

        public void LoadContent(ContentManager content)
        {
            // Enemy sprite sheets
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
            spiderSheet = content.Load<Texture2D>("Sprites/Spider");

            // NPC sprite sheets
            teamSheet = content.Load<Texture2D>("Sprites/TeamIdles-Sheet");
            signTexture = content.Load<Texture2D>("Sprites/Sign");
            textGraveTexture = content.Load<Texture2D>("Sprites/Gravestone(tall)");

            FogTexture = content.Load<Texture2D>("Images/Fogclouds");

            // Item textures (lookup dictionary)
            itemSheet = content.Load<Texture2D>("Sprites/itemsheet2");
            Point itemSize = new Point(32, 32);
            Point itemPos = new Point(0, 0);
            itemTextureIInfo["Berry"] = new Rectangle(itemPos, itemSize);
            itemPos.X += 32;
            itemTextureIInfo["Explosive"] = new Rectangle(itemPos, itemSize);
            itemPos.X += 32;
            itemTextureIInfo["Key"] = new Rectangle(itemPos, itemSize);
            itemPos.X += 32;
            itemTextureIInfo["Log"] = new Rectangle(itemPos, itemSize);
            itemPos.X += 32;
            itemTextureIInfo["Pebble"] = new Rectangle(itemPos, itemSize);
            itemPos.X += 32;
            itemTextureIInfo["SpeedShoes"] = new Rectangle(itemPos, itemSize);
            hotdogTexture = content.Load<Texture2D>("Images/hotdog");
            fishingrodTexture = content.Load<Texture2D>("Sprites/fishingRod");
        }

        public IObstacle CreateBlock(string type, Vector2 position, Vector2 size = new Vector2())
        {
            IObstacle block;
            if (type.Equals("Hurt_Block", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateBushSprite(2), true);
            }
            else if (type.Equals("Bush", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateBushSprite(1));
            }
            else if (type.Equals("BerryBush", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateBushSprite(3));
            }
            else if (type.Equals("Tree1", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateTreeSprite(1));
            }
            else if (type.Equals("Tree2", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateTreeSprite(2));
            }
            else if (type.Equals("Firepit", StringComparison.OrdinalIgnoreCase))
            {
                block = new Obstacle(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateFirepitSprite(), false);
            }
            else
            {
                block = new Obstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateOtherSprite(1));
            }
            return block;
        }
        
        public IObstacle[] CreateFence(string type, Vector2 position, Vector2 size = new Vector2(), bool endCorner = false)
        {
            int count = 0;
            //This means AT MOST you can have 40 
            IObstacle[] fence = new IObstacle[144];
            if (type.Equals("FenceNS", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = (int)(position.X); i < (position.X + size.X); i += ((int)size.X / 9))
                {
                    fence[count] = new Obstacle(new Vector2(120, 72), new Vector2(i, position.Y), ObstacleSpriteFactory.Instance.CreateFenceSprite(1));
                    count++;
                }
                if (endCorner)
                    fence[count] = new Obstacle(new Vector2(40,72), new Vector2(fence[count].OriginPos.X+ size.X / 12, fence[count].OriginPos.X), ObstacleSpriteFactory.Instance.CreateFenceSprite(3));
            }
            else if (type.Equals("FenceEW", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = (int)position.Y; i < (position.Y + size.Y); i += ((int)size.Y / 9))
                {
                    fence[count] = new Obstacle(new Vector2(40, 72), new Vector2(position.X, i), ObstacleSpriteFactory.Instance.CreateFenceSprite(2));
                    count++;
                }
                if (endCorner)
                    fence[count] = new Obstacle(new Vector2(40, 72), new Vector2(fence[count].OriginPos.X,fence[count].OriginPos.Y + size.Y/12), ObstacleSpriteFactory.Instance.CreateFenceSprite(3));
            }
            else
            {
                fence[count] = new Obstacle(new Vector2(40, 72), new Vector2(position.X), ObstacleSpriteFactory.Instance.CreateFenceSprite());
            }
            return fence;
        }

        public IObstacle CreateSpecialBlock(string type, Vector2 position, Room CurRoom, Vector2 size = new Vector2())
        {
            IObstacle block;
            if (type.Equals("Destructible_Block", StringComparison.OrdinalIgnoreCase))
            {
                block = new DestructibleObstacle(new Vector2(48,72), position, ObstacleSpriteFactory.Instance.CreateBoulderSprite(random.Next(3)), CurRoom);
            }
            else if (type.Equals("Sign", StringComparison.OrdinalIgnoreCase))
            {
                block = new Sign(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateOtherSprite(1));
            }
            else if (type.Equals("Firepit", StringComparison.OrdinalIgnoreCase))
            {
                block = new Firepit(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateFirepitSprite());
            }
            else if (type.Equals("Door", StringComparison.OrdinalIgnoreCase))
            {
                block = new Door(new Vector2(128), position, ObstacleSpriteFactory.Instance.CreateDoorSprite());
            }
            else if (type.Equals("Moveable_Block", StringComparison.OrdinalIgnoreCase))
            {
                block = new MoveableObstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateMoveableBlockSprite());
            }
            else 
            {
                block = new Boundary(position, size, ObstacleSpriteFactory.Instance.CreateOtherSprite(1));
            }
            return block;
        }

        public IEnemy CreateEnemy(string type, Vector2 position)
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
            else if (type.Equals("Spider", StringComparison.OrdinalIgnoreCase))
            {
                Animation spiderIdle = new(spiderSheet, TimeSpan.FromMilliseconds(80), new Vector2(64, 64), new Vector2(0, 64), spiderSheet.Width);
                AnimatedSprite spiderSpr = new(spiderIdle, spiderSheet) { Scale = Vector2.One };

                return new Spider(spiderSpr, position);
            }

            return null;
        }

        public INPC CreateNPC(string type, Vector2 position, int dialogueIndex)
        {
            if (type.Equals("Ethan", StringComparison.OrdinalIgnoreCase))
            {
                Animation ethanIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), Vector2.Zero, teamSheet.Width);
                AnimatedSprite ethan = new(ethanIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new EthanNPC(ethan, position, dialogueIndex);
            }
            else if (type.Equals("Lydia", StringComparison.OrdinalIgnoreCase))
            {
                Animation lydiaIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 128), teamSheet.Width);
                AnimatedSprite lydia = new(lydiaIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(lydia, position, () => DialogueFactory.Instance.CreateLydiaDialogue(dialogueIndex));
            }
            else if (type.Equals("Joe", StringComparison.OrdinalIgnoreCase))
            {
                Animation lilyIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 256), teamSheet.Width);
                AnimatedSprite lily = new(lilyIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(lily, position, () => DialogueFactory.Instance.CreateLilyDialogue());
            }
            else if (type.Equals("Lily", StringComparison.OrdinalIgnoreCase))
            {
                Animation joeIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 384), teamSheet.Width);
                AnimatedSprite joe = new(joeIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(joe, position, () => DialogueFactory.Instance.CreateJoeDialogue());
            }
            else if (type.Equals("Sign", StringComparison.OrdinalIgnoreCase))
            {
                ISprite sign = new Sprite(signTexture, signTexture.Bounds);
                return new NPC(sign, position, () => DialogueFactory.Instance.CreateSignDialogue(dialogueIndex));
            }
            else if (type.Equals("Grave", StringComparison.OrdinalIgnoreCase))
            {
                ISprite sign = new Sprite(textGraveTexture, textGraveTexture.Bounds);
                return new NPC(sign, position, () => DialogueFactory.Instance.CreateGravestoneDialogue(dialogueIndex));
            }

            return null;
        }

                
        public IItem CreateItem(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Item type cannot be empty.", nameof(type));

            switch (type.Trim().ToLowerInvariant())
            {
                case "hotdog":
                    return new HotdogItem(new Sprite(hotdogTexture, new Rectangle(0, 0, 32, 32)));
                case "berry":
                    return new BerryItem(new Sprite(itemSheet, itemTextureIInfo["Berry"]));
                case "explosive":
                    return new ExplosiveItem(new Sprite(itemSheet, itemTextureIInfo["Explosive"]));
                case "log":
                    return new LogItem(new Sprite(itemSheet, itemTextureIInfo["Log"]));
                case "key":
                    return new KeyItem( new Sprite(itemSheet, itemTextureIInfo["Key"]));
                case "pebble":
                    return new PebbleItem(new Sprite(itemSheet, itemTextureIInfo["Pebble"]));
                case "speedshoes":
                    return new SpeedItem(new Sprite(itemSheet, itemTextureIInfo["SpeedShoes"]));
                case "fishingrod":
                    return new FishingRodItem(new Sprite(fishingrodTexture, new Rectangle(0, 0, 32, 32)));

                default:
                    throw new ArgumentException(
                        $"Unknown item type: '{type}'", nameof(type));
            }
        }


        public Sprite CreateFog()
        {   
            Sprite sprite = new Sprite(FogTexture, FogTexture.Bounds);
            sprite.Color = Color.White * 0.95f;
            sprite.Scale = new Vector2(2.25f);
            return sprite;
        }

    }
}
