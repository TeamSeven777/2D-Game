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
        
        //private AnimatedSprite PlayerHurtSpr;
        private Queue<Sprite> blockSpriteSet;
        private Texture2D blockSpriteSheet;
        // private Dictionary<PlayerStateMachine.PlayerStates, AnimatedSprite> playerSpriteSet;
        private IController controller;
        public ItemInventory Inventory { get; private set; }
        public EnemyManager Enemies;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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
            //TODO UseItemCommands, finish implementing the commands
            controller.RegisterCommand(Keys.E, new DamageCommand(this));
            controller.RegisterCommand(Keys.T, new PreviousBlockCommand(this));
            controller.RegisterCommand(Keys.Y, new NextBlockCommand(this));
            controller.RegisterCommand(Keys.U, new PreviousItemCommand(this));
            controller.RegisterCommand(Keys.I, new NextItemCommand(this));
            controller.RegisterCommand(Keys.O, new PreviousCharacterCommand(this));
            controller.RegisterCommand(Keys.P, new NextCharacterCommand(this));
            controller.RegisterCommand(Keys.Z, new AttackCommand(this));
            controller.RegisterCommand(Keys.D1, new UseAxeCommand(this));
            controller.RegisterCommand(Keys.D2, new UseSlingshotCommand(this));
            controller.RegisterCommand(Keys.D3, new UnEquipCommand(this));
            blockSpriteSet = new Queue<Sprite>();


            Inventory = new ItemInventory();
            Enemies = new EnemyManager();
            Enemies.LoadContent(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            middleOfScreen = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width / 2, _spriteBatch.GraphicsDevice.Viewport.Height / 2);

            spriteSheet = Content.Load<Texture2D>("Sprites/LinkSheet");

            //playerSpriteSet = new Dictionary<PlayerStateMachine.PlayerStates, AnimatedSprite>();

            /*animationPlayerDown = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(0,10), 34);
            animationPlayerUp = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(68, 10), 34);
            animationPlayerLeft = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(34, 10), 34);
            animationPlayerRight = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(34, 10), 34);

            animationAxePlayerDownSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(0, 47), 68);
            animationAxePlayerUpSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(0, 78), 68);
            animationAxePlayerLeftSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(0, 78), 68);
            animationAxePlayerRightSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(0, 97), 68);

            animationKnifePlayerDownSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(94, 47), 68);
            animationKnifePlayerUpSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(84, 78), 68);
            animationKnifePlayerLeftSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(84, 78), 68);
            animationKnifePlayerRightSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(94, 97), 68);


            playerDownSpr = new AnimatedSprite(animationPlayerDown, spriteSheet);
            playerDownSpr.Scale = new Vector2(4.0f);
            playerUpSpr = new AnimatedSprite(animationPlayerUp, spriteSheet);
            playerUpSpr.Scale = new Vector2(4.0f);
            playerLeftSpr = new AnimatedSprite(animationPlayerLeft, spriteSheet);
            playerLeftSpr.Scale = new Vector2(4.0f);
            playerLeftSpr.Effects = SpriteEffects.FlipHorizontally;
            playerRightSpr = new AnimatedSprite(animationPlayerRight, spriteSheet);
            playerRightSpr.Scale = new Vector2(4.0f);
            //PlayerHurtSpr for when or if its needed; 

            AxePlayerDownSpr = new AnimatedSprite(animationAxePlayerDownSpr, spriteSheet);
            AxePlayerUpSpr = new AnimatedSprite(animationAxePlayerUpSpr, spriteSheet);
            AxePlayerRightSpr = new AnimatedSprite(animationAxePlayerRightSpr, spriteSheet);
            AxePlayerLeftSpr = new AnimatedSprite(animationAxePlayerLeftSpr, spriteSheet, SpriteEffects.FlipHorizontally);

            KnifePlayerDownSpr = new AnimatedSprite(animationKnifePlayerDownSpr, spriteSheet);
            KnifePlayerUpSpr = new AnimatedSprite(animationKnifePlayerUpSpr, spriteSheet);
            KnifePlayerRightSpr = new AnimatedSprite(animationKnifePlayerRightSpr, spriteSheet);
            KnifePlayerLeftSpr = new AnimatedSprite(animationKnifePlayerLeftSpr, spriteSheet, SpriteEffects.FlipHorizontally);

            PlayerUseDownspr = (AnimatedSprite)new Sprite(spriteSheet, new Rectangle(107, 10, 17, 17), new Vector2(4.0f));
            PlayerUseUpspr = (AnimatedSprite)new Sprite(spriteSheet, new Rectangle(140, 10, 17, 17), new Vector2(4.0f));
            PlayerUseLeftspr = (AnimatedSprite)new Sprite(spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f), SpriteEffects.FlipHorizontally);
            PlayerUseRightspr = (AnimatedSprite)new Sprite(spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f));
            */

            /*playerSpriteSet.Add(PlayerStateMachine.PlayerStates.WalkDown, playerDownSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.WalkUp, playerUpSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.WalkLeft, playerLeftSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.WalkRight, playerRightSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.UseUp, PlayerUseUpspr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.UseDown, PlayerUseDownspr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.UseLeft, PlayerUseLeftspr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.UseRight, PlayerUseRightspr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.AxeLeft, AxePlayerLeftSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.AxeRight, AxePlayerRightSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.AxeUp, AxePlayerUpSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.AxeDown, AxePlayerDownSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.KnifeRight, KnifePlayerRightSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.KnifeLeft, KnifePlayerLeftSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.KnifeUp, KnifePlayerUpSpr);
            playerSpriteSet.Add(PlayerStateMachine.PlayerStates.KnifeDown, KnifePlayerDownSpr);
            */

            player = new DefaultPlayer(spriteSheet);


            Inventory.LoadContent(Content);

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
            block.Update(gameTime);
            Enemies.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            Enemies.Draw(_spriteBatch);
            Inventory.Draw(_spriteBatch);
            block.Draw(_spriteBatch);

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
