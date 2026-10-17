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
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            RoomCollisionHandler.Instance.Initialize();
            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
            blockSpriteSet = new Queue<Sprite>();
            boundaries = new List<Boundary>();


            controllerNames = new String[]{ "keyboard", "gamepad", "mouse"};
            controllers = new Dictionary<string, IController>();
            controllers.Add(controllerNames[0], new KeyboardController(this));
            controllers.Add(controllerNames[1], new GamePadController(PlayerIndex.One, this));
            controllers.Add(controllerNames[2], new MouseController(this));



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            //Handy variable if you just want something to be centered and observable
            roomHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
            roomWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
            middleOfScreen = new Vector2(roomWidth / 2, roomHeight / 2);
            

            //spriteSheet = Content.Load<Texture2D>("Sprites/LinkSheet");
            spriteSheet = Content.Load<Texture2D>("Sprites/Player_Sheet");
            PlayerSpriteFactory.Instance.LoadTextures(Content);

            player = new DefaultPlayer();


            Inventory.LoadContent(Content);
            Enemies.LoadContent(Content);

            blockSpriteSet = new Queue<Sprite>();
            blockSpriteSheet = Content.Load<Texture2D>("Sprites/testblocks");
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(25, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(50, 0, 25, 25)));
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(0, roomHeight)));//boundary test to left
            boundaries.Add(new Boundary(new Vector2(roomWidth, 0), new Vector2(roomWidth, roomHeight)));//boundary test to right
            boundaries.Add(new Boundary(new Vector2(0, 0), new Vector2(roomWidth, 0)));//boundary test to up
            boundaries.Add(new Boundary(new Vector2(0, roomHeight), new Vector2(roomWidth, roomHeight)));//boundary test to down

            projectileSpriteSheet = Content.Load<Texture2D>("Sprites/pebble");
            PlayerProjectileHandler.Instance.Initialize(ref player, projectileSpriteSheet);

            block = new Obstacle(new Vector2(25), middleOfScreen, blockSpriteSet);

            hitboxMode = new HitboxMode(this);

        }

        protected override void Update(GameTime gameTime)
        {
            RoomCollisionHandler.Instance.HandleCollision();
            //Do we still need this??
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
               Exit();
            //currentGameTime = gameTime;

            //Everything we are updating
            //To make it more "fair", everything
            //The player does should come first
            //~Player Actions~
            foreach (String controllerType in controllerNames)
            {
                if(controllers[controllerType].Update()) break; //only use one controller type at a time.

            }

            player.Update(gameTime);
            PlayerProjectileHandler.Instance.Update(gameTime);
            Inventory.Update(gameTime);
            block.Update(gameTime);

            //~Whatever Else Actions~
            Enemies.Update(gameTime, player.Position);
            

            base.Update(gameTime);
            RoomCollisionHandler.Instance.Update(gameTime);
            hitboxMode.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentGameTime = gameTime;
            _spriteBatch.Begin();

            //Everything we are drawing
            player.Draw(_spriteBatch);
            PlayerProjectileHandler.Instance.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            Inventory.Draw(_spriteBatch);
            block.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            foreach(Boundary boundary in boundaries) boundary.Draw(_spriteBatch);

            //for hitbox debugging
            hitboxMode.DrawHitboxes(_spriteBatch);
            //
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetGame()
        {
            Initialize();
        }
    }
}
