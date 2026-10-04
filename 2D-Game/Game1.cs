using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class Game1 : Game
    {

        private Vector2 middleOfScreen;
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
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
            blockSpriteSet = new Queue<Sprite>();
            controllerNames = new String[]{ "keyboard", "gamepad", "mouse"};
            controllers = new Dictionary<string, IController>();
            controllers.Add(controllerNames[0], new KeyboardController(this));
            controllers.Add(controllerNames[1], new GamePadController(PlayerIndex.One, this));
            controllers.Add(controllerNames[2], new MouseController(this));



            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
            Enemies.LoadContent(Content);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Handy variable if you just want something to be centered and observable
            middleOfScreen = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width / 2, _spriteBatch.GraphicsDevice.Viewport.Height / 2);

            spriteSheet = Content.Load<Texture2D>("Sprites/LinkSheet");
            //spriteSheet = Content.Load<Texture2D>("Sprites/Player_Sheet");

            player = new DefaultPlayer(spriteSheet);


            Inventory.LoadContent(Content);
            Enemies.LoadContent(Content);

            blockSpriteSet = new Queue<Sprite>();
            blockSpriteSheet = Content.Load<Texture2D>("Sprites/testblocks");
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(25, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(50, 0, 25, 25)));

            projectileSpriteSheet = Content.Load<Texture2D>("Sprites/projectileSprites");
            projectileHandler = new PlayerProjectileHandler(ref player, projectileSpriteSheet);

            block = new Obstacle(new Rectangle(0, 0, 25, 25), middleOfScreen, blockSpriteSet);
        }

        protected override void Update(GameTime gameTime)
        {
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
            projectileHandler.Update(gameTime);
            Inventory.Update(gameTime);
            block.Update(gameTime);

            //~Whatever Else Actions~
            Enemies.Update(gameTime, player.Position);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentGameTime = gameTime;
            _spriteBatch.Begin();

            //Everything we are drawing
            player.Draw(_spriteBatch);
            projectileHandler.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            Inventory.Draw(_spriteBatch);
            block.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetGame()
        {
            Initialize();
        }
    }
}
