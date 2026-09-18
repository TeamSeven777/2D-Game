using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Fourest_Seven.lib.Objects;
using The_Fourest_Seven.lib.Graphics;

namespace The_Fourest_Seven
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprite treeSprite;
        private Texture2D testTreeSprite;
        private IObstacle tree;
        private Texture2D testSpriteSheet;
        private AnimatedSprite lilGuy;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            tree = new Obstacle(new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2, 100, 100), new Vector2( 15, 15), treeSprite );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            testTreeSprite = Content.Load<Texture2D>("Sprites/testsprite");
            treeSprite = new Sprite(testTreeSprite);
            tree.sprite = treeSprite;
            testSpriteSheet = Content.Load<Texture2D>("Sprites/testspritesheet");
            Animation lilGuyAnimation = new Animation(testSpriteSheet, System.TimeSpan.FromMilliseconds(500), new Vector2(25, 25), Vector2.Zero);
            lilGuy = new AnimatedSprite(lilGuyAnimation, testSpriteSheet);
            tree.sprite.scale = new Vector2(4.0f);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            tree.Update(gameTime);
            lilGuy.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            Vector2 middleOfScreen = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            //_spriteBatch.Draw(testTreeSprite, middleOfScreen, Color.White);
            //treeSprite.Draw(_spriteBatch, middleOfScreen);
            lilGuy.Draw(_spriteBatch, middleOfScreen);
            //_spriteBatch.Draw(lilGuy.content, middleOfScreen, new Rectangle(0,0,25,25), Color.White);
            //_spriteBatch.Draw(testSpriteSheet,middleOfScreen, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
