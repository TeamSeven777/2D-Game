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
        public GameInitializer Game1Initializer;
        public GameLoader Game1Loader;
        public GameUpdater Game1Updater;
        public GameDrawer Game1Drawer;
        //public GameMethods Game1Methods;
        public Vector2 middleOfScreen;
        public int roomWidth { get;  set; }
        public int roomHeight { get; set;  }
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Stopwatch CurTime;
        public StatusDisplay _statusDisplay;
        public DefaultPlayer player;
        public Texture2D blockSpriteSheet;
        public Texture2D fishingHoleSprite;
        public IObstacle fishingHole;
        public Dictionary<String, IController> controllers;
        public String[] controllerNames;
        public EnemyManager Enemies;
        public GameTime currentGameTime;
        public GameTime CurrentGameTime => currentGameTime;
        public HitboxMode hitboxMode;
        public List<IObstacle> boundaries;
        public Texture2D background;
        public int worldWidth { get;  set; }
        public int worldHeight { get;  set; }
        public RoomManager roomManager;
        public Camera Camera { get;  set; }
        public Minimap minimap;
        public IHotbar weaponHotbar, itemHotbar;
        public Texture2D inventorySlot, highlightSlot;
        public GameState State { get;  set; }
        public Texture2D startScreen;
        public Texture2D endScreen;
        public SpriteFont hudFont;
        public const int LogsNeeded = 7;
        public int LogsInFire { get;  set; }
        public Bog bog;
        public PauseMenu pauseMenu;
        public bool PauseGameOn { get;  set; }
        public bool DialogueMode { get; set; }
        public IDialogue CurrentDialogue { get; set; }
        public AudioManager GameAudioManager;
        public DeathHandler deathHandler;
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
            Game1Initializer = new GameInitializer(this);
            
            
            Game1Initializer.Initialize();
            Game1Loader = new GameLoader(this);
            Game1Updater = new GameUpdater(this);
            Game1Drawer = new GameDrawer(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Game1Loader.LoadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            
            Game1Updater.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            Game1Drawer.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void StartGame()
        {
            State = GameState.Playing;
            controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.MovementMode);
            controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.MovementMode);
            roomManager.CurrentRoom.Activate();
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
            controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.EndMode);
            roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.EndTheme, false);
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
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.MovementMode);
            }
            else
            {
                PauseGameOn = true;
                controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.PauseMode);
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.PauseMode);
            }
            
        }
        public void HandleDialogue()
        {
            if (!DialogueMode)
            {
                DialogueMode = true;
                controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.DialogueMode);
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.DialogueMode);
            }
            else
            {
                if (!CurrentDialogue.Next())
                {
                    CurrentDialogue = new NoDialogue();
                    DialogueMode = false;
                    controllers[controllerNames[0]] = new KeyboardController(this, ControllerMode.MovementMode);
                    controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, this, ControllerMode.MovementMode);
                }
                else SoundEffectFactory.Instance.PlaySoundEffect(SoundName.Dialogue, true);
            }
        }
    }
}
