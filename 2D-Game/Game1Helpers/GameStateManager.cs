using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;

namespace _2D_Game
{
    public class GameStateManager //handles game state changes, and updates and draws based on states.
{
        private Game1 myGame;
        public Dictionary<ControllerName, IController> controllers { get; private set; }
        public ControllerName[] controllerNames { get; private set; }
        public GameState CurrentState { get; private set; }
        public const int LogsNeeded = 7;
        public int LogsInFire { get; private set; }
        private GameUpdater myGameUpdater;
        private GameDrawer myGameDrawer;
        public GameStateManager(Game1 game1)
        {
            myGame = game1;
            CurrentState = GameState.Start;
            LogsInFire = 0;
            controllers = new Dictionary<ControllerName, IController>();

            controllerNames = new ControllerName[] { ControllerName.Keyboard, ControllerName.GamePad, ControllerName.Mouse };
            controllers.Add(ControllerName.Keyboard, new KeyboardController(myGame, ControllerMode.StartMode));
            controllers.Add(ControllerName.GamePad, new GamePadController(PlayerIndex.One, myGame, ControllerMode.StartMode));
            controllers.Add(ControllerName.Mouse, new MouseController(myGame));
            myGameUpdater = new GameUpdater(myGame, this);
            myGameDrawer = new GameDrawer(myGame);
        }
        public void StartGame()
        {
            CurrentState = GameState.Playing;
            controllers[ControllerName.Keyboard] = new KeyboardController(myGame, ControllerMode.MovementMode);
            controllers[ControllerName.GamePad] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.MovementMode);
            myGame.roomManager.CurrentRoom.Activate();
        }
        public void AddLogsToFire(int amount)
        {
            LogsInFire += amount;
            if (LogsInFire >= LogsNeeded) EndGame();
        }

        public void EndGame()
        {
            CurrentState = GameState.End;
            controllers[ControllerName.Keyboard] = new KeyboardController(myGame, ControllerMode.EndMode);
            controllers[ControllerName.GamePad] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.EndMode);
            myGame.roomManager.CurrentRoom.Deactivate();
            Playlist.Instance.ChangeSong(SongName.EndTheme, false);
        }      
        public void TogglePause()
        {
            if (CurrentState == GameState.Pause)
            {
                CurrentState = GameState.Playing;
                controllers[controllerNames[0]] = new KeyboardController(myGame, ControllerMode.MovementMode);
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.MovementMode);
            }
            else
            {
                CurrentState = GameState.Pause;
                controllers[controllerNames[0]] = new KeyboardController(myGame, ControllerMode.PauseMode);
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.PauseMode);
            }

        }
        public void HandleDialogue()
        {
            if (CurrentState !=GameState.Dialogue)
            {
                CurrentState = GameState.Dialogue;
                controllers[controllerNames[0]] = new KeyboardController(myGame, ControllerMode.DialogueMode);
                controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.DialogueMode);
            }
            else
            {
                if (!myGame.CurrentDialogue.Next())
                {
                    myGame.CurrentDialogue = new NoDialogue();
                    CurrentState = GameState.Playing;
                    controllers[controllerNames[0]] = new KeyboardController(myGame, ControllerMode.MovementMode);
                    controllers[controllerNames[1]] = new GamePadController(PlayerIndex.One, myGame, ControllerMode.MovementMode);
                }
                else SoundEffectFactory.Instance.PlaySoundEffect(SoundName.Dialogue, true);
            }
        }

        public void Update(GameTime gameTime) 
        {
            switch (CurrentState)
            {           
                case GameState.Dialogue:
                    myGameUpdater.UpdateDialogue(gameTime); break;
                case GameState.Pause:
                    myGameUpdater.UpdatePause(gameTime); break;
                case GameState.Start:
                    myGameUpdater.UpdateStart(gameTime); break;
                case GameState.End:
                    myGameUpdater.UpdateEnd(gameTime); break;
                default: //playing
                    myGameUpdater.UpdatePlay(gameTime); break;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(CurrentState == GameState.Start || CurrentState == GameState.End) myGameDrawer.DrawStartOrEnd(gameTime, spriteBatch);
            else myGameDrawer.DrawGame(gameTime, spriteBatch);
        }
    }
}
