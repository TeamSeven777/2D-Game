using _2D_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace _2D_Game
{
    public class RoomContentFactory
    {
        // Assets
        private Texture2D ghostSheet, ghostAttackSheet, mosquitoSheet, spiderSheet;
        private Texture2D teamSheet;
        private Texture2D itemSheet;
        private readonly Dictionary<string, Rectangle> itemTextureIInfo = new(StringComparer.OrdinalIgnoreCase);


        public void LoadContent(ContentManager content, Texture2D blockSheet)
        {
            // Enemy sprite sheets
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
            spiderSheet = content.Load<Texture2D>("Sprites/Spider");

            // NPC sprite sheets
            teamSheet = content.Load<Texture2D>("Sprites/TeamIdles-Sheet");

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
        }

        public IObstacle CreateBlock(string type, Vector2 position)
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

        public IObstacle CreateSpecialBlock(string type, Vector2 position, Room CurRoom)
        {
            IObstacle block;
            if (type.Equals("Destructible_Block", StringComparison.OrdinalIgnoreCase))
            {
                block = new DestructibleObstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateOtherSprite(2), CurRoom);
            }
            else if(type.Equals("Sign", StringComparison.OrdinalIgnoreCase))
            {
                block = new Sign(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateOtherSprite(1));
            }
            else if(type.Equals("Firepit", StringComparison.OrdinalIgnoreCase))
            {
                block = new Firepit(new Vector2(64), position, ObstacleSpriteFactory.Instance.CreateFirepitSprite());
            }
            else if(type.Equals("Door", StringComparison.OrdinalIgnoreCase))
            {
                block = new Door(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateDoorSprite());
            }
            else if(type.Equals("Moveable_Block", StringComparison.OrdinalIgnoreCase))
            {
                block = new MoveableObstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateMoveableBlockSprite());
            }
            else
            {
                //A Block that does nothing
                block = new Obstacle(new Vector2(25), position, ObstacleSpriteFactory.Instance.CreateOtherSprite(1));
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

        public INPC CreateNPC(string type, Vector2 position)
        {
            if (type.Equals("Ethan", StringComparison.OrdinalIgnoreCase))
            {
                Animation ethanIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), Vector2.Zero, teamSheet.Width);
                AnimatedSprite ethan = new(ethanIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(ethan, position, DialogueFactory.Instance.CreateEthanDialogue());
            }
            else if (type.Equals("Lydia", StringComparison.OrdinalIgnoreCase))
            {
                Animation lydiaIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 128), teamSheet.Width);
                AnimatedSprite lydia = new(lydiaIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(lydia, position, DialogueFactory.Instance.CreateLydiaDialogue());
            }
            else if (type.Equals("Joe", StringComparison.OrdinalIgnoreCase))
            {
                Animation lilyIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 256), teamSheet.Width);
                AnimatedSprite lily = new(lilyIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(lily, position, DialogueFactory.Instance.CreateLilyDialogue());
            }
            else if (type.Equals("Lily", StringComparison.OrdinalIgnoreCase))
            {
                Animation joeIdle = new(teamSheet, TimeSpan.FromMilliseconds(100), new Vector2(128, 128), new Vector2(0, 384), teamSheet.Width);
                AnimatedSprite joe = new(joeIdle, teamSheet) { Scale = new Vector2(0.95f) };
                return new NPC(joe, position, DialogueFactory.Instance.CreateJoeDialogue());
            }          

            return null;
        }

        public IItem CreateItem(string type)
        {
            // safety check in case you try to load an item whose texture isn't in the itemTextures dictionary
            if (itemSheet == null)
            {
                System.Diagnostics.Debug.WriteLine($"[Warning] Missing texture for item type: '{type}'");
                return null; 
            }

            return type switch
            {
                "Berry" => new BerryItem(new Sprite(itemSheet, itemTextureIInfo["Berry"])),
                "Explosive" => new ExplosiveItem(new Sprite(itemSheet, itemTextureIInfo["Explosive"])),
                "Log" => new LogItem(new Sprite(itemSheet, itemTextureIInfo["Log"])),
                "Key" => new KeyItem(new Sprite(itemSheet, itemTextureIInfo["Key"])),
                "Pebble" => new PebbleItem(new Sprite(itemSheet, itemTextureIInfo["Pebble"])),
                "SpeedShoes" => new SpeedItem(new Sprite(itemSheet, itemTextureIInfo["SpeedShoes"])),
                _ => null // Unknown type returns null
            };
        }
    }
}
