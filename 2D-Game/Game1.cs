using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace _2D_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public DefaultPlayer player;

        private Texture2D spr;

        private IController controller;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new DefaultPlayer();
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


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spr = Content.Load<Texture2D>("images/logo");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            controller.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(spr, player.Position, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetGame()
        {
            Initialize();
        }
    }
}
