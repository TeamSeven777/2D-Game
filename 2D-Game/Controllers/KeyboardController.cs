using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace _2D_Game
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;     

        public KeyboardController(Game1 myGame, ControllerMode controllerMode)
        {
            controllerMappings = new Dictionary<Keys, ICommand>();
            //all keyboard controls
            if (controllerMode == ControllerMode.MovementMode)
            {
                RegisterCommand(Keys.W, new UpCommand(myGame));
                RegisterCommand(Keys.A, new LeftCommand(myGame));
                RegisterCommand(Keys.D, new RightCommand(myGame));
                RegisterCommand(Keys.S, new DownCommand(myGame));
                RegisterCommand(Keys.Q, new QuitCommand(myGame));
                RegisterCommand(Keys.R, new ResetCommand(myGame));
                RegisterCommand(Keys.E, new DamageCommand(myGame));
                RegisterCommand(Keys.T, new PreviousBlockCommand(myGame));
                RegisterCommand(Keys.Y, new SwapRangedWeaponCommand(myGame));
                RegisterCommand(Keys.U, new PreviousItemCommand(myGame));
                RegisterCommand(Keys.I, new NextItemCommand(myGame));
                RegisterCommand(Keys.O, new PreviousCharacterCommand(myGame));
                RegisterCommand(Keys.P, new NextCharacterCommand(myGame));
                RegisterCommand(Keys.Z, new AttackCommand(myGame));
                RegisterCommand(Keys.D1, new UseAxeCommand(myGame));
                RegisterCommand(Keys.D2, new UseRangedWeaponCommand(myGame));
                RegisterCommand(Keys.D3, new UnEquipCommand(myGame));
                RegisterCommand(Keys.H, new HitboxModeCommand(myGame));
                RegisterCommand(Keys.Escape, new PauseMenuCommand(myGame));
                RegisterCommand(Keys.Space, new InteractCommand(myGame));
            }
            else if (controllerMode == ControllerMode.DialogueMode)
            {
                RegisterCommand(Keys.Space, new NextDialogueCommand(myGame));
            }else if (controllerMode == ControllerMode.PauseMode)
            {
                RegisterCommand(Keys.Escape, new PauseMenuCommand(myGame));
            }
        }
        public void RegisterCommand(Keys key, ICommand command)
        {
            controllerMappings.Add(key, command);
        }
        public void UnRegisterCommand(Keys key) 
        {
            if(controllerMappings.ContainsKey(key))controllerMappings.Remove(key);
        }
        public bool Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            bool keyboardUsed = false;
            foreach (Keys key in pressedKeys)
            {
                if (controllerMappings.ContainsKey(key))
                {
                    controllerMappings[key].Execute();
                    keyboardUsed = true;
                }
            }
            return keyboardUsed;
        }
    }
}
