using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;

namespace _2D_Game
{
    public class GameInitializer
{
        private Game1 myGame;
        public GameInitializer(Game1 game1) 
        { 
            myGame = game1;
        }
        public void Initialize()
        {
            myGame.currentGameTime = new GameTime();
            RoomCollisionHandler.Instance.Initialize(myGame);
            WeaponInventory.Instance.Initialize(myGame);
            myGame.Enemies = new EnemyManager();
            myGame.boundaries = new List<IObstacle>();

            myGame.PauseGameOn = false;
            myGame.CurrentDialogue = new NoDialogue();
            myGame.DialogueMode = false;

            myGame.controllerNames = new String[] { "keyboard", "gamepad", "mouse" };
            myGame.controllers = new Dictionary<string, IController>();
            myGame.State = GameState.Start;
            myGame.LogsInFire = 0;
            myGame.controllers.Add(myGame.controllerNames[0], new KeyboardController(myGame, ControllerMode.StartMode));
            myGame.controllers.Add(myGame.controllerNames[1], new GamePadController(PlayerIndex.One, myGame, ControllerMode.StartMode));
            myGame.controllers.Add(myGame.controllerNames[2], new MouseController(myGame));

            myGame.roomManager = new RoomManager();
            myGame.deathHandler = new DeathHandler(myGame);
            
        }
}
}
