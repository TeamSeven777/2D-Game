using _2D_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Channels;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class Game1 : Game
    {
        public Vector2 middleOfScreen { get; private set; }
        public int roomWidth { get; private set; }
        public int roomHeight { get; private set;  }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Stopwatch CurTime { get; private set; }
        public StatusDisplay _statusDisplay { get; private set;  }
        public DefaultPlayer player;
        public IObstacle fishingHole { get; private set; }
        public EnemyManager Enemies;
        public GameTime currentGameTime { get; private set; }
        public GameTime CurrentGameTime => currentGameTime;
        public HitboxMode hitboxMode;
        public List<IObstacle> boundaries;
        public int worldWidth { get; private set; }
        public int worldHeight { get; private set; }
        public RoomManager roomManager;
        public Camera Camera { get; private set; }
        public Minimap minimap { get; private set; }
        public IHotbar weaponHotbar, itemHotbar;
        public Texture2D startScreen { get; private set; }
        public Texture2D endScreen { get; private set; }
        public Texture2D background { get; private set; }
        private Texture2D blockSpriteSheet, fishingHoleSprite, inventorySlot, highlightSlot;
        private SpriteFont hudFont;
        public Bog bog { get; private set;  }
        public PauseMenu pauseMenu { get; private set; }
        public IDialogue CurrentDialogue { get; set; }
        public AudioManager GameAudioManager;
        public DeathHandler deathHandler;
        public GameStateManager StateManager { get; private set; }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 700;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            currentGameTime = new GameTime();
            RoomCollisionHandler.Instance.Initialize(this);
            WeaponInventory.Instance.Initialize(this);
            Enemies = new EnemyManager();
            boundaries = new List<IObstacle>();

            CurrentDialogue = new NoDialogue();
            StateManager = new GameStateManager(this); // handles state and controllers
            roomManager = new RoomManager();
            deathHandler = new DeathHandler(this);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameAudioManager = new AudioManager(Content);

            roomHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
            roomWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
            middleOfScreen = new Vector2(roomWidth / 2, roomHeight / 2);
            int CellWidth = 60;

            minimap = new Minimap(GraphicsDevice, new Point(10), CellWidth, roomWidth, roomHeight);
            worldWidth = roomWidth * 3;
            worldHeight = roomHeight * 3;
            Vector2 startRoomOrigin = new Vector2(0, worldHeight - roomHeight);

            background = Content.Load<Texture2D>("Images/FullMap2");
            startScreen = Content.Load<Texture2D>("Images/startScreen");
            endScreen = Content.Load<Texture2D>("Images/endScreen");
            hudFont = Content.Load<SpriteFont>("Fonts/pixelFont");
            CurTime = new Stopwatch(hudFont);
            Camera = new Camera(roomWidth, roomHeight, worldWidth, worldHeight);

            PlayerSpriteFactory.Instance.LoadTextures(Content);
            ProjectileSpriteFactory.Instance.LoadTextures(Content);
            ObstacleSpriteFactory.Instance.LoadTextures(Content);
            DialogueFactory.Instance.LoadFonts(Content);
            

            player = new DefaultPlayer(startRoomOrigin + middleOfScreen);
            _statusDisplay = new StatusDisplay(this, hudFont);
            Camera.Follow(player.Position);
           // gameGPS = new GameGPS(player.Position, roomWidth, roomHeight);


            ItemInventory.Instance.LoadContent(Content, roomManager.CurrentRoom);

            inventorySlot = Content.Load<Texture2D>("Images/InventoryBlock2");
            highlightSlot = Content.Load<Texture2D>("Images/HighlightUIBig");
            SpriteFont spriteFont = Content.Load<SpriteFont>("Fonts/pixelFont");

            weaponHotbar = new WeaponHotbar(GraphicsDevice,  new Point(GraphicsDevice.Viewport.Width / 2 - 480, 620), 48, 4, inventorySlot, highlightSlot, spriteFont);
            itemHotbar = new ItemHotbar(GraphicsDevice, new Point(GraphicsDevice.Viewport.Width/2 - 175, 620), 48, 7, inventorySlot, highlightSlot, spriteFont);
            pauseMenu = new PauseMenu(GraphicsDevice,this, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 200, 100), spriteFont);

            Enemies.LoadContent(Content, startRoomOrigin);

            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(0, worldHeight)));                      // left
            boundaries.Add(new Boundary(new Vector2(worldWidth, 0), new Vector2(worldWidth, worldHeight)));    // right
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(worldWidth, 0)));                       // up
            boundaries.Add(new Boundary(new Vector2(0, worldHeight), new Vector2(worldWidth, worldHeight)));   // down

            int pondWidth = 200; int pondHeight = 100; int pondX = roomWidth + 200; int pondY = roomHeight + 175;
            fishingHoleSprite = Content.Load<Texture2D>("Sprites/fishinghole");
            fishingHole = new FishZone(new Vector2(pondX, pondY), new Vector2(pondWidth, pondHeight),new Sprite(fishingHoleSprite, fishingHoleSprite.Bounds));//pond TODO add texture
                        

            PlayerProjectileHandler.Instance.Initialize(ref player);
            EnemyProjectileHandler.Instance.Initialize(ref Enemies, ref player);
          

            hitboxMode = new HitboxMode(this);

            //bog loading - needs to be initialized after Player and Camera (constructor uses them)
            bog = new Bog(this);
            bog.LoadContent(Content);

            // Load dungeon rooms and pass viewport dimensions

            roomManager.LoadDungeon("Content/all_rooms.xml", Content, blockSpriteSheet, roomWidth, roomHeight);
            roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.TitleTheme, true);

        }

        protected override void Update(GameTime gameTime)
        {
            currentGameTime = gameTime;
            StateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentGameTime = gameTime;
            StateManager.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
        public void ResetGame()
        {
            Initialize();
        }
    }
}