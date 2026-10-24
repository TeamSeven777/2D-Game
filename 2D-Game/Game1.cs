using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Texture2D playerSpriteSheet;
        private Queue<Sprite> blockSpriteSet;
        private Texture2D blockSpriteSheet;
        private Texture2D playerProjectileSpriteSheet;
        private Texture2D enemyProjectileSpriteSheet;
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
        public bool PauseGameOn { get; private set; }
        public bool DialogueMode { get; private set; }
        public IDialogue CurrentDialogue { get; set; }
        
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
            blockSpriteSet = new Queue<Sprite>();
            boundaries = new List<Boundary>();

            PauseGameOn = false;
            CurrentDialogue = new NoDialogue();
            DialogueMode = false;

            controllerNames = new String[]{ "keyboard", "gamepad", "mouse"};
            controllers = new Dictionary<string, IController>();
            controllers.Add(controllerNames[0], new KeyboardController(this, ControllerMode.MovementMode));
            controllers.Add(controllerNames[1], new GamePadController(PlayerIndex.One, this));
            controllers.Add(controllerNames[2], new MouseController(this));

            roomManager = new RoomManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            roomHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
            roomWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
            middleOfScreen = new Vector2(roomWidth / 2, roomHeight / 2);
            minimap = new Minimap(GraphicsDevice, new Point(10, 10), 60, roomWidth, roomHeight);
            
            worldWidth = roomWidth * 3;
            worldHeight = roomHeight * 3;
            Vector2 startRoomOrigin = new Vector2(0, worldHeight - roomHeight);
            background = Content.Load<Texture2D>("Images/FullMap");
            Camera = new Camera(roomWidth, roomHeight, worldWidth, worldHeight);

            //spriteSheet = Content.Load<Texture2D>("Sprites/LinkSheet");
            playerSpriteSheet = Content.Load<Texture2D>("Sprites/Player_Sheet");
            PlayerSpriteFactory.Instance.LoadTextures(Content);
            DialogueFactory.Instance.LoadFonts(Content);

            player = new DefaultPlayer(startRoomOrigin + middleOfScreen);
            Camera.Follow(player.Position);


            ItemInventory.Instance.LoadContent(Content, roomManager.CurrentRoom);
            
            Enemies.LoadContent(Content, startRoomOrigin);

            blockSpriteSet = new Queue<Sprite>();
            blockSpriteSheet = Content.Load<Texture2D>("Sprites/testblocks");


            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(25, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(50, 0, 25, 25)));
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(0, worldHeight)));                      // left
            boundaries.Add(new Boundary(new Vector2(worldWidth, 0), new Vector2(worldWidth, worldHeight)));    // right
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(worldWidth, 0)));                       // up
            boundaries.Add(new Boundary(new Vector2(0, worldHeight), new Vector2(worldWidth, worldHeight)));   // down
            playerProjectileSpriteSheet = Content.Load<Texture2D>("Sprites/playerprojectiles");
            enemyProjectileSpriteSheet = Content.Load<Texture2D>("Sprites/enemy-projectiles");
            PlayerProjectileHandler.Instance.Initialize(ref player, playerProjectileSpriteSheet);
            EnemyProjectileHandler.Instance.Initialize(ref Enemies, ref player, enemyProjectileSpriteSheet);

            block = new Obstacle(new Vector2(25), startRoomOrigin + middleOfScreen + new Vector2(150, 150), blockSpriteSet);

            hitboxMode = new HitboxMode(this);

            
            // Load dungeon rooms and pass viewport dimensions
            roomManager.LoadDungeon("Content/all_rooms.xml", Content, blockSpriteSheet, roomWidth, roomHeight);

        }

        protected override void Update(GameTime gameTime)
        {
            
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
                if (IsActive && minimap.TryGetClickedRoomCenter(out Vector2 roomCenter))
                {
                    player.Teleport(roomCenter);
                    Camera.Follow(player.Position);
                }

            player.Update(gameTime);
            roomManager.Update(gameTime, player.Position);
            PlayerProjectileHandler.Instance.Update(gameTime);
            WeaponInventory.Instance.Update(gameTime);
            ItemInventory.Instance.Update(gameTime, roomManager.CurrentRoom);
            block.Update(gameTime);

                //~Whatever Else Actions~
                Enemies.Update(gameTime, player.Position);
                EnemyProjectileHandler.Instance.Update(gameTime);


                RoomCollisionHandler.Instance.Update(gameTime);
                Camera.Follow(player.Position);

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
            currentGameTime = gameTime;
            
             // World: scrolls with the camera
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);
            _spriteBatch.Draw(background, new Rectangle(0, 0, worldWidth, worldHeight), Color.White);

            //Everything we are drawing
            player.Draw(_spriteBatch);
            roomManager.Draw(_spriteBatch);
            PlayerProjectileHandler.Instance.Draw(_spriteBatch);
            EnemyProjectileHandler.Instance.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            block.Draw(_spriteBatch);
            foreach(Boundary boundary in boundaries) boundary.Draw(_spriteBatch);

            //for hitbox debugging
            hitboxMode.DrawHitboxes(_spriteBatch);
            //

            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // no transform
            // HUD draws here
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            minimap.Draw(_spriteBatch, player.Position);
            CurrentDialogue.Draw(_spriteBatch, new Vector2(middleOfScreen.X, middleOfScreen.Y+roomHeight/4)); //draw 3/4 down screen in middle
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetGame()
        {
            Initialize();
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
            }
        }
    }
}
