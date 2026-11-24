using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Class
{
    public class ObstacleSpriteFactory
{
        Texture2D TestBlockSpriteSheet;
        Texture2D GravestoneSprite;
        Texture2D TreeSprites;
        Texture2D BushSprites;
        Texture2D FirepitSprite;
        Texture2D DoorSprites;
        Texture2D BoulderSprites;
        Texture2D FenceSprites;
        Texture2D FenceLongSprite;
        Texture2D BerryBushSprite;
        private static ObstacleSpriteFactory instance = new ObstacleSpriteFactory();

        public static ObstacleSpriteFactory Instance
        { get { return instance; } }

        private ObstacleSpriteFactory() { }

        public void LoadTextures(ContentManager content)
        {
            TestBlockSpriteSheet = content.Load<Texture2D>("Sprites/testblocks");
            GravestoneSprite = content.Load<Texture2D>("Sprites/Gravestone(short)");
            TreeSprites = content.Load<Texture2D>("Sprites/trees");
            BushSprites = content.Load<Texture2D>("Sprites/bushes");
            FirepitSprite = content.Load<Texture2D>("Sprites/FirePit");
            DoorSprites = content.Load<Texture2D>("Sprites/door");
            BoulderSprites = content.Load<Texture2D>("Sprites/boulders");
            FenceSprites = content.Load<Texture2D>("Sprites/fences");
            FenceLongSprite = content.Load<Texture2D>("Sprites/fencelong");
            BerryBushSprite = content.Load<Texture2D>("Sprites/BerryBush2");
        }

        /// <summary>
        /// Creates a fire pit sprite
        /// from <see cref="FirepitSprite"/>
        /// </summary>
        /// <returns></returns>
        public ISprite CreateFirepitSprite()
        {
            ISprite sprite = new Sprite(FirepitSprite,new Rectangle(0,0,64,64));
            sprite.Scale = new Vector2(2,2);
            return sprite;
        }

        /// <summary>
        /// Creates a tree sprite, where <paramref name="index"/> determines which one
        /// from <see cref="TreeSprites"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ISprite CreateTreeSprite(int index)
        {
            ISprite sprite;
            switch (index)
            {
                case (1):
                    sprite = new Sprite(TreeSprites,new Rectangle(0,80,48,64));
                    break;
                case (2):
                    sprite = new Sprite(TreeSprites, new Rectangle(48, 80, 64, 64));
                    break;
                default:
                    sprite = new Sprite();
                    break;
            }
            sprite.Scale = new Vector2(2,2);
            return sprite;
        }

        /// <summary>
        /// Creates a bush sprite, where <paramref name="index"/> determines which one
        /// from the <see cref="BushSprites"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ISprite CreateBushSprite(int index)
        {
            ISprite sprite;
            switch (index)
            {
                case (1):
                    sprite = new Sprite(BushSprites, new Rectangle(0, 48, 32, 32));
                    break;
                case (2):
                    sprite = new Sprite(BushSprites, new Rectangle(32, 48, 32, 32));
                    break;
                case (3):
                    sprite = new Sprite(BerryBushSprite, new Rectangle(0, 0, 64, 64));
                    break;
                default:
                    sprite = new Sprite();
                    break;
            }
            sprite.Scale = new Vector2(2,2);
            return sprite;
        }

        public ISprite CreateDoorSprite()
        {
            
            return new Sprite(DoorSprites, new Rectangle(0, 0, 128, 128));
        }

        public ISprite CreateMoveableBlockSprite()
        {
            return new Sprite(GravestoneSprite, new Rectangle(0, 0, 64, 64));
        }

        public ISprite CreateGraveStoneSprite()
        {
            return new Sprite(GravestoneSprite, new Rectangle(0,0, 64, 64));
        }

        /// <summary>
        /// Creates an unspecified object sprite, where <paramref name="index"/> determines which one
        /// from the <see cref="TestBlockSpriteSheet"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ISprite CreateOtherSprite(int index)
        {
            ISprite sprite;
            switch (index)
            {
                case (1):
                    sprite = new Sprite(TestBlockSpriteSheet, new Rectangle(0, 0, 25, 25));
                    break;
                case (2):
                    sprite = new Sprite(TestBlockSpriteSheet, new Rectangle(25, 0, 25, 25));
                    break;
                case (3):
                    sprite = new Sprite(TestBlockSpriteSheet, new Rectangle(50, 0, 25, 25));
                    break;
                default : 
                    sprite = new Sprite(); 
                    break;
            }
            return sprite;
        }

        public ISprite CreateBoulderSprite(int index = 0)
        {
            ISprite sprite;
            switch (index)
            {
                case (0):
                    sprite = new Sprite(BoulderSprites, new Rectangle(0, 0, 48, 72));
                    break;
                case (1):
                    sprite = new Sprite(BoulderSprites, new Rectangle(48, 0, 48, 72));
                    break;
                case (2):
                    sprite = new Sprite(BoulderSprites, new Rectangle(96, 0, 48, 72));
                    break;
                case (3):
                    sprite = new Sprite(BoulderSprites, new Rectangle(144, 0, 48, 72));
                    break;
                default:
                    sprite = new Sprite();
                    break;
            }
            sprite.Scale = new Vector2(2f);
            return sprite;
        }
        public ISprite CreateFenceSprite(int index = 0)
        {
            ISprite sprite;
            switch (index)
            {
                //N/S facing case
                case (0):
                    sprite = new Sprite(FenceSprites, new Rectangle(20, 0, 20, 72));
                    break;
                //N/S Long facing case
                case (1):
                    sprite = new Sprite(FenceLongSprite, FenceLongSprite.Bounds);
                    break;
                //E/W facing case
                case (2):
                    sprite = new Sprite(FenceSprites, new Rectangle(40, 0, 20, 72));
                    break;
                //corner case
                case (4):
                    sprite = new Sprite(FenceSprites, new Rectangle(0, 0, 20, 72));
                    break;
                default:
                    sprite = new Sprite(FenceSprites, new Rectangle(20, 0, 20, 72));
                    break;
            }
            sprite.Scale = new Vector2(2.0f);
            return sprite;
        }
    }
}
