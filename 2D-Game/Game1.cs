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
        private Texture2D spriteSheet;
        private Queue<Sprite> blockSpriteSet;
        private Texture2D blockSpriteSheet;
        private Texture2D projectileSpriteSheet;
        public IProjectileHandler projectileHandler;
        private Dictionary<String, IController> controllers;
  
        private String[] controllerNames;
        public ItemInventory Inventory { get; private set; }
        public EnemyManager Enemies;
        private GameTime currentGameTime;
        public GameTime CurrentGameTime => currentGameTime;
        public HitboxMode hitboxMode;
        public List<Boundary> boundaries;
        private Texture2D background;
        private int worldWidth, worldHeight;
        public Camera Camera { get; private set; }
        private Minimap minimap;
        public bool PauseGameOn { get; private set; }
        
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
            RoomCollisionHandler.Instance.Initialize();
            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
            blockSpriteSet = new Queue<Sprite>();
            boundaries = new List<Boundary>();

            PauseGameOn = false;


            controllerNames = new String[]{ "keyboard", "gamepad", "mouse"};
            controllers = new Dictionary<string, IController>();
            controllers.Add(controllerNames[0], new KeyboardController(this, ControllerMode.MovementMode));
            controllers.Add(controllerNames[1], new GamePadController(PlayerIndex.One, this));
            controllers.Add(controllerNames[2], new MouseController(this));


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
            background = Content.Load<Texture2D>("Images/gameBackground");
            Camera = new Camera(roomWidth, roomHeight, worldWidth, worldHeight);

            //spriteSheet = Content.Load<Texture2D>("Sprites/LinkSheet");
            spriteSheet = Content.Load<Texture2D>("Sprites/Player_Sheet");
            PlayerSpriteFactory.Instance.LoadTextures(Content);

            player = new DefaultPlayer(startRoomOrigin + middleOfScreen);
            Camera.Follow(player.Position);


            Inventory.LoadContent(Content, startRoomOrigin);
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
            projectileSpriteSheet = Content.Load<Texture2D>("Sprites/pebble");
            PlayerProjectileHandler.Instance.Initialize(ref player, projectileSpriteSheet);

            block = new Obstacle(new Vector2(25), startRoomOrigin + middleOfScreen + new Vector2(150, 0), blockSpriteSet);

            hitboxMode = new HitboxMode(this);

        }

        protected override void Update(GameTime gameTime)
        {
            
            RoomCollisionHandler.Instance.HandleCollision();

            currentGameTime = gameTime;

            //Everything we are updating
            //To make it more "fair", everything
            //The player does should come first
            //~Player Actions~

            if (!PauseGameOn) //disable certain updates on Pause. //TODO have another check to determine whether this is due to dialogue or a menu, and update accordingly
            {
                foreach (String controllerType in controllerNames)
                {
                    if (controllers[controllerType].Update()) break; //only use one controller type at a time.
                }
                if (IsActive && minimap.TryGetClickedRoomCenter(out Vector2 roomCenter))
                {
                    player.Teleport(roomCenter);
                    Camera.Follow(player.Position);
                }

                player.Update(gameTime);
                PlayerProjectileHandler.Instance.Update(gameTime);
                Inventory.Update(gameTime);
                block.Update(gameTime);

                //~Whatever Else Actions~
                Enemies.Update(gameTime, player.Position);



                RoomCollisionHandler.Instance.Update(gameTime);
                Camera.Follow(player.Position);

                hitboxMode.Update(gameTime);
            }
            else
            {
                foreach (String controllerType in controllerNames)
                {
                    if (controllers[controllerType].Update()) break; //only use one controller type at a time.

                }
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
            PlayerProjectileHandler.Instance.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            Inventory.Draw(_spriteBatch);
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
    }
}
