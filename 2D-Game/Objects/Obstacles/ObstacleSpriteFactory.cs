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
        Texture2D TreeSprites;
        Texture2D BushSprites;
        private static ObstacleSpriteFactory instance = new ObstacleSpriteFactory();

        public static ObstacleSpriteFactory Instance
        { get { return instance; } }

        private ObstacleSpriteFactory() { }

        public void LoadTextures(ContentManager content)
        {
            TestBlockSpriteSheet = content.Load<Texture2D>("Sprites/testblocks");
            TreeSprites = content.Load<Texture2D>("Sprites/trees");
            BushSprites = content.Load<Texture2D>("Sprites/bushes");
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
                default:
                    sprite = new Sprite();
                    break;
            }
            return sprite;
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
    }
}
