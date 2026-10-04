using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class GamePadController : IController
{
        private PlayerIndex CurIndex;
        private Dictionary<Buttons, ICommand> buttonMappings;
        public GamePadController(PlayerIndex index, Game1 myGame)
        {
            buttonMappings = new Dictionary<Buttons, ICommand>();
            CurIndex = index;
            //gamepad controls
            RegisterCommand(Buttons.DPadDown, new DownCommand(myGame));
            RegisterCommand(Buttons.DPadUp, new UpCommand(myGame));
            RegisterCommand(Buttons.DPadRight, new RightCommand(myGame));
            RegisterCommand(Buttons.DPadLeft, new LeftCommand(myGame));
            RegisterCommand(Buttons.X, new AttackCommand(myGame));
            RegisterCommand(Buttons.Y, new UseAxeCommand(myGame));
            RegisterCommand(Buttons.RightShoulder, new UseSlingshotCommand(myGame));
        }
        public void RegisterCommand(Buttons button, ICommand command)
        {
            buttonMappings.Add(button, command);
        }

        public bool Update()
        {
            bool gamepadUsed = false;
            //Button Control//
            foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
            {
                if (buttonMappings.ContainsKey(btn))
                {
                    if (GamePad.GetState(CurIndex).IsButtonDown(btn))
                    {
                        buttonMappings[btn].Execute();
                        gamepadUsed = true;
                    }
                }
            }
            return gamepadUsed;

             
        }
    }
}
