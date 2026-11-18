using _2D_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class GameLoader
{
        private Game1 myGame;
        private ContentManager Content;
        private GraphicsDevice GraphicsDevice;
        public GameLoader(Game1 game1) 
        { 
            myGame = game1;
            this.Content = myGame.Content;
            this.GraphicsDevice = myGame.GraphicsDevice;
            
        }
        public void LoadContent()
        {
            this.Content = myGame.Content;
            this.GraphicsDevice = myGame.GraphicsDevice;
            myGame._spriteBatch = new SpriteBatch(GraphicsDevice);

            myGame.GameAudioManager = new AudioManager(Content);


            myGame.roomHeight = myGame._spriteBatch.GraphicsDevice.Viewport.Height;
            myGame.roomWidth = myGame._spriteBatch.GraphicsDevice.Viewport.Width;
            myGame.middleOfScreen = new Vector2(myGame.roomWidth / 2, myGame.roomHeight / 2);
            int CellWidth = 60;

            myGame.minimap = new Minimap(GraphicsDevice, new Point(10), CellWidth, myGame.roomWidth, myGame.roomHeight);
            myGame.worldWidth = myGame.roomWidth * 3;
            myGame.worldHeight = myGame.roomHeight * 3;
            Vector2 startRoomOrigin = new Vector2(0, myGame.worldHeight - myGame.roomHeight);

            myGame.background = Content.Load<Texture2D>("Images/FullMap2");
            myGame.startScreen = Content.Load<Texture2D>("Images/startScreen");
            myGame.endScreen = Content.Load<Texture2D>("Images/endScreen");
            myGame.hudFont = Content.Load<SpriteFont>("Fonts/pixelFont");
            myGame.CurTime = new Stopwatch(myGame.hudFont);
            myGame.Camera = new Camera(myGame.roomWidth, myGame.roomHeight, myGame.worldWidth, myGame.worldHeight);

            PlayerSpriteFactory.Instance.LoadTextures(Content);
            ProjectileSpriteFactory.Instance.LoadTextures(Content);
            ObstacleSpriteFactory.Instance.LoadTextures(Content);
            DialogueFactory.Instance.LoadFonts(Content);


            myGame.player = new DefaultPlayer(startRoomOrigin + myGame.middleOfScreen);
            myGame._statusDisplay = new StatusDisplay(myGame, myGame.hudFont);
            myGame.Camera.Follow(myGame.player.Position);
            // gameGPS = new GameGPS(player.Position, roomWidth, roomHeight);


            ItemInventory.Instance.LoadContent(Content, myGame.roomManager.CurrentRoom);

            myGame.inventorySlot = Content.Load<Texture2D>("Images/InventoryBlock2");
            myGame.highlightSlot = Content.Load<Texture2D>("Images/HighlightUIBig");
            SpriteFont spriteFont = Content.Load<SpriteFont>("Fonts/pixelFont");

            myGame.weaponHotbar = new WeaponHotbar(GraphicsDevice, new Point(GraphicsDevice.Viewport.Width / 2 - 480, 620), 48, 4, myGame.inventorySlot, myGame.highlightSlot, spriteFont);
            myGame.itemHotbar = new ItemHotbar(GraphicsDevice, new Point(GraphicsDevice.Viewport.Width / 2 - 175, 620), 48, 7, myGame.inventorySlot, myGame.highlightSlot, spriteFont);
            myGame.pauseMenu = new PauseMenu(GraphicsDevice, myGame, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 200, 100), spriteFont);

            myGame.Enemies.LoadContent(Content, startRoomOrigin);

            myGame.boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(0, myGame.worldHeight)));                      // left
            myGame.boundaries.Add(new Boundary(new Vector2(myGame.worldWidth, 0), new Vector2(myGame.worldWidth, myGame.worldHeight)));    // right
            myGame.boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(myGame.worldWidth, 0)));                       // up
            myGame.boundaries.Add(new Boundary(new Vector2(0, myGame.worldHeight), new Vector2(myGame.worldWidth, myGame.worldHeight)));   // down

            int pondWidth = 200; int pondHeight = 100; int pondX = myGame.roomWidth + 200; int pondY = myGame.roomHeight + 175;
            myGame.fishingHoleSprite = Content.Load<Texture2D>("Sprites/fishinghole");
            myGame.fishingHole = new FishZone(new Vector2(pondX, pondY), new Vector2(pondWidth, pondHeight), new Sprite(myGame.fishingHoleSprite, myGame.fishingHoleSprite.Bounds));//pond TODO add texture


            PlayerProjectileHandler.Instance.Initialize(ref myGame.player);
            EnemyProjectileHandler.Instance.Initialize(ref myGame.Enemies, ref myGame.player);


            myGame.hitboxMode = new HitboxMode(myGame);

            //bog loading - needs to be initialized after Player and Camera (constructor uses them)
            myGame.bog = new Bog(myGame);
            myGame.bog.LoadContent(Content);

            // Load dungeon rooms and pass viewport dimensions

            myGame.roomManager.LoadDungeon("Content/all_rooms.xml", Content, myGame.blockSpriteSheet, myGame.roomWidth, myGame.roomHeight);
            myGame.roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.TitleTheme, true);

        }
}
}
