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
        private Animation animationPlayerUp;
        private Animation animationPlayerDown;
        private Animation animationPlayerLeft;
        private Animation animationPlayerRight;
        private AnimatedSprite playerDownSpr;
        private AnimatedSprite playerUpSpr;
        private AnimatedSprite playerLeftSpr;
        private AnimatedSprite playerRightSpr;
        private Queue<Sprite> blockSpriteSet;
        private Texture2D blockSpriteSheet;
        private IController controller;
        public ItemInventory Inventory { get; private set; }
        public EnemyManager Enemies { get; private set; }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            controller = new KeyboardController();
            controller.RegisterCommand(Keys.W, new UpCommand(this));
            controller.RegisterCommand(Keys.A, new LeftCommand(this));
            controller.RegisterCommand(Keys.D, new RightCommand(this));
            controller.RegisterCommand(Keys.S, new DownCommand(this));
            controller.RegisterCommand(Keys.Q, new QuitCommand(this));
            controller.RegisterCommand(Keys.R, new ResetCommand(this));
            controller.RegisterCommand(Keys.Z, new AttackCommand(this));
            controller.RegisterCommand(Keys.N, new AttackCommand(this));
            //TODO UseItemCommands, finish implementing the commands
            controller.RegisterCommand(Keys.E, new DamageCommand(this));
            controller.RegisterCommand(Keys.T, new PreviousBlockCommand(this));
            controller.RegisterCommand(Keys.Y, new NextBlockCommand(this));
            controller.RegisterCommand(Keys.U, new PreviousItemCommand(this));
            controller.RegisterCommand(Keys.I, new NextItemCommand(this));
            controller.RegisterCommand(Keys.O, new PreviousCharacterCommand(this));
            controller.RegisterCommand(Keys.P, new NextCharacterCommand(this));
            blockSpriteSet = new Queue<Sprite>();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            middleOfScreen = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width / 2, _spriteBatch.GraphicsDevice.Viewport.Height / 2);

            spriteSheet = Content.Load<Texture2D>("Sprites/testspritesheet");

            //move these to player state machine?
            animationPlayerDown = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(25, 25), Vector2.Zero, spriteSheet.Width, Animation.Style.Walking);
            animationPlayerUp = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(25, 25), new Vector2(0, 25), spriteSheet.Width, Animation.Style.Walking);
            animationPlayerLeft = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(25, 25), new Vector2(0, 50), spriteSheet.Width, Animation.Style.Walking);
            animationPlayerRight = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(25, 25), new Vector2(0, 75), spriteSheet.Width, Animation.Style.Walking);

            playerDownSpr = new AnimatedSprite(animationPlayerDown, spriteSheet);
            playerDownSpr.Scale = new Vector2(4.0f);

            playerUpSpr = new AnimatedSprite(animationPlayerUp, spriteSheet);
            playerUpSpr.Scale = new Vector2(4.0f);

            playerLeftSpr = new AnimatedSprite(animationPlayerLeft, spriteSheet);
            playerLeftSpr.Scale = new Vector2(4.0f);

            playerRightSpr = new AnimatedSprite(animationPlayerRight, spriteSheet);
            playerRightSpr.Scale = new Vector2(4.0f);
            //
           
            player = new DefaultPlayer(spriteSheet);


            Inventory.LoadContent(Content);
            Enemies.LoadContent(Content);

            blockSpriteSheet = Content.Load<Texture2D>("Sprites/testblocks");

            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(0, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(25, 0, 25, 25)));
            blockSpriteSet.Enqueue(new Sprite(blockSpriteSheet, new Rectangle(50, 0, 25, 25)));
          

            block = new Obstacle(new Rectangle(0, 0, 25, 25), middleOfScreen, blockSpriteSet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            controller.Update();
            Inventory.Update(gameTime);
            Enemies.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);

            Inventory.Draw(_spriteBatch);
            block.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);

            Vector2 middleOfScreen = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width/2 , _spriteBatch.GraphicsDevice.Viewport.Height/2);
            //playerDownSpr.Draw(_spriteBatch, middleOfScreen);
            //_spriteBatch.Draw(playerDownSpr.Content, middleOfScreen, new Rectangle(0,0,25,25), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetGame()
        {
            Initialize();
        }
    }
}
