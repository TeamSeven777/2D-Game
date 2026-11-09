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

        private Vector2 middleOfScreen;
        private int roomWidth, roomHeight;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Obstacle block;
        public DefaultPlayer player;
        private Texture2D blockSpriteSheet;
        private Dictionary<String, IController> controllers;
        private String[] controllerNames;
        public EnemyManager Enemies;
        private GameTime currentGameTime;
        public GameTime CurrentGameTime => currentGameTime;
        public HitboxMode hitboxMode;
        public List<Boundary> boundaries;
        private Texture2D background;
        private int worldWidth, worldHeight;
        private RoomManager roomManager;
        public Camera Camera { get; private set; }
        private Minimap minimap;
        private IHotbar weaponHotbar, itemHotbar;
        private Texture2D inventorySlot, highlightSlot;

        public GameState State { get; private set; }
        private Texture2D startScreen;
        private Texture2D endScreen;
        private SpriteFont hudFont;
        private Texture2D bogBackground;
        private Rectangle bogArea;
        private Rectangle bogEntrance; //walking into this on the main map teleports the player
        public bool InBog { get; private set; }
        public const int LogsNeeded = 7;
        public int LogsInFire { get; private set; }

        public bool PauseGameOn { get; private set; }
        public bool DialogueMode { get; private set; }
        public IDialogue CurrentDialogue { get; set; }
        public AudioManager GameAudioManager;
        
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
            boundaries = new List<Boundary>();

            PauseGameOn = false;
            CurrentDialogue = new NoDialogue();
            DialogueMode = false;

            controllerNames = new String[]{ "keyboard", "gamepad", "mouse"};
            controllers = new Dictionary<string, IController>();
            State = GameState.Start;
            LogsInFire = 0;
            InBog = false;
            controllers.Add(controllerNames[0], new KeyboardController(this, ControllerMode.StartMode));
            controllers.Add(controllerNames[1], new GamePadController(PlayerIndex.One, this));
            controllers.Add(controllerNames[2], new MouseController(this));

            roomManager = new RoomManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameAudioManager = new AudioManager(Content);
            

            roomHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
            roomWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
            middleOfScreen = new Vector2(roomWidth / 2, roomHeight / 2);
            minimap = new Minimap(GraphicsDevice, new Point(10, 10), 60, roomWidth, roomHeight);
            
            worldWidth = roomWidth * 3;
            worldHeight = roomHeight * 3;
            Vector2 startRoomOrigin = new Vector2(0, worldHeight - roomHeight);
            background = Content.Load<Texture2D>("Images/FullMap2");
            startScreen = Content.Load<Texture2D>("Images/startScreen");
            endScreen = Content.Load<Texture2D>("Images/endScreen");
            hudFont = Content.Load<SpriteFont>("Fonts/pixelFont");
            bogBackground = Content.Load<Texture2D>("Images/bog");
            Camera = new Camera(roomWidth, roomHeight, worldWidth, worldHeight);

            PlayerSpriteFactory.Instance.LoadTextures(Content);
            ProjectileSpriteFactory.Instance.LoadTextures(Content);
            ObstacleSpriteFactory.Instance.LoadTextures(Content);
            DialogueFactory.Instance.LoadFonts(Content);
            

            player = new DefaultPlayer(startRoomOrigin + middleOfScreen);
            Camera.Follow(player.Position);
           // gameGPS = new GameGPS(player.Position, roomWidth, roomHeight);


            ItemInventory.Instance.LoadContent(Content, roomManager.CurrentRoom);

            inventorySlot = Content.Load<Texture2D>("Images/InventoryBlock2");
            highlightSlot = Content.Load<Texture2D>("Images/HighlightUIBig");
            SpriteFont spriteFont = Content.Load<SpriteFont>("Fonts/pixelFont");

            weaponHotbar = new WeaponHotbar(GraphicsDevice,  new Point(GraphicsDevice.Viewport.Width / 2 - 480, 620), 48, spriteFont);
            itemHotbar = new ItemHotbar(GraphicsDevice, new Point(GraphicsDevice.Viewport.Width/2 - 175, 620), 48, 7, inventorySlot, highlightSlot, spriteFont);

            Enemies.LoadContent(Content, startRoomOrigin);

            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(0, worldHeight)));                      // left
            boundaries.Add(new Boundary(new Vector2(worldWidth, 0), new Vector2(worldWidth, worldHeight)));    // right
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(worldWidth, 0)));                       // up
            boundaries.Add(new Boundary(new Vector2(0, worldHeight), new Vector2(worldWidth, worldHeight)));   // down
            // Room (4,4) is clear of the oversized right and bottom main map walls
            bogArea = new Rectangle(4 * roomWidth, 4 * roomHeight, roomWidth, roomHeight);
            bogEntrance = new Rectangle(worldWidth / 2 - 150, 0, 300, 100); //grass above the top center path

            int wall = 32; //the second Boundary argument is a width and height
            boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top - wall), new Vector2(bogArea.Width + wall * 2, wall)));   // up
            boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Bottom), new Vector2(bogArea.Width + wall * 2, wall)));       // down
            boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top), new Vector2(wall, bogArea.Height)));                    // left
            boundaries.Add(new Boundary(new Vector2(bogArea.Right, bogArea.Top), new Vector2(wall, bogArea.Height)));                          // right

            PlayerProjectileHandler.Instance.Initialize(ref player);
            EnemyProjectileHandler.Instance.Initialize(ref Enemies, ref player);
          

            hitboxMode = new HitboxMode(this);

            
            // Load dungeon rooms and pass viewport dimensions
            roomManager.LoadDungeon("Content/all_rooms.xml", Content, blockSpriteSheet, roomWidth, roomHeight);
            roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.TitleTheme, true);

        }

        protected override void Update(GameTime gameTime)
        {
            
            if (State != GameState.Playing)
            {
                currentGameTime = gameTime;
                controllers[controllerNames[0]].Update();
                base.Update(gameTime);
                return;
            }

            RoomCollisionHandler.Instance.HandleCollision();
            currentGameTime = gameTime;
            roomHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
            roomWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
            middleOfScreen = new Vector2(roomWidth / 2, roomHeight / 2);
            //Everything we are updating
            //To make it more "fair", everything
            //The player does should come first
            //~Player Actions~
            foreach (String controllerType in controllerNames)
            {
                if (controllers[controllerType].Update()) break; //only use one controller type at a time.

            }
            if (!PauseGameOn && !DialogueMode) //disable certain updates on Pause. //TODO have another check to determine whether this is due to dialogue or a menu, and update accordingly
            {
                if (IsActive && !InBog && minimap.TryGetClickedRoomCenter(out Vector2 roomCenter))
                {
                    player.Teleport(roomCenter);
                    Camera.Follow(player.Position);
                }

            player.Update(gameTime);
            if (InBog) CheckBogExits();
            else if (bogEntrance.Contains(player.Position.ToPoint())) EnterBog();
            roomManager.Update(gameTime, player.Position);
            PlayerProjectileHandler.Instance.Update(gameTime);
            WeaponInventory.Instance.Update(gameTime);
            ItemInventory.Instance.Update(gameTime, roomManager.CurrentRoom);
            weaponHotbar.Update(gameTime);
            itemHotbar.Update(gameTime);

                //~Whatever Else Actions~
                Enemies.Update(gameTime, player.Position);
                EnemyProjectileHandler.Instance.Update(gameTime);


                RoomCollisionHandler.Instance.Update(gameTime);
                Camera.Follow(player.Position);
                //gameGPS.Update(player.Position);
                hitboxMode.Update(gameTime);
            }
            else if (DialogueMode)
            {
                CurrentDialogue.Update(gameTime);
                player.Update(gameTime);
                //TODO give roommanager a way so that it can continue to animate just the NPC idles while talking
            }
            else
            {
                //no updates aside from controller inputs
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (State != GameState.Playing)
            {
                _spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
                Texture2D screen = State == GameState.Start ? startScreen : endScreen;
                _spriteBatch.Draw(screen, new Rectangle(0, 0, roomWidth, roomHeight), Color.White);
                _spriteBatch.End();
                base.Draw(gameTime);
                
                return;
            }

            currentGameTime = gameTime;
            
             // World: scrolls with the camera
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);
            _spriteBatch.Draw(background, new Rectangle(0, 0, worldWidth, worldHeight), Color.White);
            _spriteBatch.Draw(bogBackground, bogArea, Color.White);

            //Everything we are drawing
            
            roomManager.Draw(_spriteBatch);
            PlayerProjectileHandler.Instance.Draw(_spriteBatch);
            EnemyProjectileHandler.Instance.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            foreach(Boundary boundary in boundaries) boundary.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            //for hitbox debugging
            hitboxMode.DrawHitboxes(_spriteBatch);
            //

            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // no transform
            // HUD draws here
            string logCounter = $" ( {LogsInFire} / {LogsNeeded} ) ";
            Vector2 logCounterSize = hudFont.MeasureString(logCounter);
            Vector2 logCounterPos = new Vector2(roomWidth - logCounterSize.X - 16, roomHeight - logCounterSize.Y - 16);
            _spriteBatch.DrawString(hudFont, logCounter, logCounterPos + new Vector2(2, 2), Color.Black); //shadow so it reads on any background
            _spriteBatch.DrawString(hudFont, logCounter, logCounterPos, Color.White);
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // no transform
            // HUD draws here
            string healthCounter = $" ( {player.Health} / {player.MaxHealth} ) ";
            Vector2 healthCounterSize = hudFont.MeasureString(healthCounter);
            Vector2 healthCounterPos = new Vector2(roomWidth - healthCounterSize.X - 16, 16);
            _spriteBatch.DrawString(hudFont, healthCounter, healthCounterPos + new Vector2(1, 1), Color.White); //shadow so it reads on any background
            _spriteBatch.DrawString(hudFont, healthCounter, healthCounterPos, Color.OrangeRed);
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (!InBog) minimap.Draw(_spriteBatch, player.Position); //the minimap only covers the main 3x3 map
            weaponHotbar.Draw(_spriteBatch);
            itemHotbar.Draw(_spriteBatch);

            CurrentDialogue.Draw(_spriteBatch, new Vector2(middleOfScreen.X, middleOfScreen.Y+roomHeight/4)); //draw 3/4 down screen in middle
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void StartGame()
        {
            State = GameState.Playing;
            controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.MovementMode);
            roomManager.CurrentRoom.Activate();
        }

        public void EnterBog()
        {
            InBog = true;
            Camera.SetBounds(bogArea);
            player.Teleport(new Vector2(bogArea.Center.X, bogArea.Bottom - 140)); //arrive at the bottom center of the bog
            Camera.Follow(player.Position);
        }

        /// <summary>
        /// Right goes to the top, top goes to the left, bottom goes to the right, and left leaves the bog
        /// </summary>
        private void CheckBogExits()
        {
            const int sideEdge = 40; //how close to a wall counts as reaching that edge
            const int bottomEdge = 90; //larger because the hitbox extends below the player's position
            const int arrivalInset = 120; //far enough from each edge that arriving can't trigger another exit

            Vector2 position = player.Position;
            if (position.X >= bogArea.Right - sideEdge)
            {
                player.Teleport(new Vector2(bogArea.Center.X, bogArea.Top + arrivalInset));
            }
            else if (position.Y <= bogArea.Top + sideEdge)
            {
                player.Teleport(new Vector2(bogArea.Left + arrivalInset, bogArea.Center.Y));
            }
            else if (position.Y >= bogArea.Bottom - bottomEdge)
            {
                player.Teleport(new Vector2(bogArea.Right - arrivalInset, bogArea.Center.Y));
            }
            else if (position.X <= bogArea.Left + sideEdge)
            {
                LeaveBog();
            }
        }

        public void LeaveBog()
        {
            InBog = false;
            Camera.SetBounds(new Rectangle(0, 0, worldWidth, worldHeight));
            player.Teleport(new Vector2(worldWidth / 2, 290)); //on the top center path, below the bog entrance
            Camera.Follow(player.Position);
        }

        public void AddLogsToFire(int amount)
        {
            LogsInFire += amount;
            if (LogsInFire >= LogsNeeded) EndGame();
        }

        public void EndGame()
        {
            State = GameState.End;
            controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.EndMode);
            roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.EndTheme, false);
        }

        public void ResetGame()
        {
            Initialize();
            LoadContent();
        }
        public void TogglePause()
        {
            if (PauseGameOn)
            {
                PauseGameOn = false;
                controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.MovementMode);
            }
            else
            {
                PauseGameOn = true;
                controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.PauseMode);
            }
            
        }
        public void HandleDialogue()
        {
            if (!DialogueMode)
            {
                DialogueMode = true;
                controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.DialogueMode);
            }
            else
            {
                if (!CurrentDialogue.Next())
                {
                    CurrentDialogue = new NoDialogue();
                    DialogueMode = false;
                    controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.MovementMode);
                }
                else SoundEffectFactory.Instance.PlaySoundEffect(SoundName.Dialogue, true);
            }
        }
    }
}
