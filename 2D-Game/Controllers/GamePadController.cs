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
        public GamePadController(PlayerIndex index, Game1 myGame, ControllerMode controllerMode)
        {
            buttonMappings = new Dictionary<Buttons, ICommand>();
            CurIndex = index;
            if (controllerMode == ControllerMode.MovementMode)
            {
                RegisterCommand(Buttons.DPadDown, new DownCommand(myGame));
                RegisterCommand(Buttons.DPadUp, new UpCommand(myGame));
                RegisterCommand(Buttons.DPadRight, new RightCommand(myGame));
                RegisterCommand(Buttons.DPadLeft, new LeftCommand(myGame));
                RegisterCommand(Buttons.B, new AttackCommand(myGame));
                RegisterCommand(Buttons.X, new UseAxeCommand(myGame));
                RegisterCommand(Buttons.Y, new UseRangedWeaponCommand(myGame));
                RegisterCommand(Buttons.A, new InteractCommand(myGame));
                RegisterCommand(Buttons.Start, new PauseMenuCommand(myGame));
                RegisterCommand(Buttons.Back, new SwapRangedWeaponCommand(myGame));
                RegisterCommand(Buttons.LeftShoulder, new PreviousItemCommand(myGame));
                RegisterCommand(Buttons.RightShoulder, new NextItemCommand(myGame));
            }
            else if (controllerMode == ControllerMode.EndMode)
            {
                RegisterCommand(Buttons.Back, new QuitCommand(myGame));
                RegisterCommand(Buttons.RightShoulder, new ResetCommand(myGame));
                RegisterCommand(Buttons.Start, new ResetCommand(myGame));
            }
            else if (controllerMode == ControllerMode.StartMode)
            {
                RegisterCommand(Buttons.Start, new StartGameCommand(myGame));
                RegisterCommand(Buttons.Back, new QuitCommand(myGame));
            }
            else if (controllerMode == ControllerMode.DialogueMode)
            {
                RegisterCommand(Buttons.A, new NextDialogueCommand(myGame));
            }
            else if (controllerMode == ControllerMode.PauseMode)
            {
                RegisterCommand(Buttons.Start, new PauseMenuCommand(myGame));
                RegisterCommand(Buttons.Back, new QuitCommand(myGame));
                RegisterCommand(Buttons.RightShoulder, new ResetCommand(myGame));
            }
            //gamepad controls

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
