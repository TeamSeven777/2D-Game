using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class RoomContentFactory
    {
        // Assets
        private Texture2D ghostSheet, ghostAttackSheet, mosquitoSheet, spiderSheet;
        private Texture2D teamSheet;
        private readonly Dictionary<string, Texture2D> itemTextures = new(StringComparer.OrdinalIgnoreCase);
        private Texture2D blockSpriteSheet;

        public void LoadContent(ContentManager content, Texture2D blockSheet)
        {
            blockSpriteSheet = blockSheet;

            // Enemy sprite sheets
            ghostSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Idle-N");
            ghostAttackSheet = content.Load<Texture2D>("Sprites/Test-Ghost-Melee-Attack-N");
            mosquitoSheet = content.Load<Texture2D>("Sprites/mosquito");
            spiderSheet = content.Load<Texture2D>("Sprites/Spider");

            // NPC sprite sheets
            teamSheet = content.Load<Texture2D>("Sprites/TeamIdles-Sheet");

            // Item textures (lookup dictionary)
            itemTextures["Berry"] = content.Load<Texture2D>("Images/Berry2");
            itemTextures["Explosive"] = content.Load<Texture2D>("Images/Explosive2");
            itemTextures["Log"] = content.Load<Texture2D>("Images/Log2");
            itemTextures["Key"] = content.Load<Texture2D>("Images/Key2");
            itemTextures["Pebble"] = content.Load<Texture2D>("Images/Pebble2");
        }

        public Queue<Sprite> CreateBlockSpriteSet()
        {
            Queue<Sprite> blockSpriteSet = new();
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));
            return blockSpriteSet;
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
            if (!itemTextures.TryGetValue(type, out Texture2D texture) || texture == null)
            {
                System.Diagnostics.Debug.WriteLine($"[Warning] Missing texture for item type: '{type}'");
                return null; 
            }

            Sprite sprite = new(texture, new Rectangle(0, 0, texture.Width, texture.Height));

            return type switch
            {
                "Berry" => new BerryItem(sprite),
                "Explosive" => new ExplosiveItem(sprite),
                "Log" => new LogItem(sprite),
                "Key" => new KeyItem(sprite),
                "Pebble" => new PebbleItem(sprite),
                _ => null // Unknown type returns null
            };
        }
    }
}
