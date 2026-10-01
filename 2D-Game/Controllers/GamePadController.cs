using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class GamePadController
{
        private Dictionary<Buttons, ICommand> controllerMappings;
        public GamePadController()
        {
            controllerMappings = new Dictionary<Buttons, ICommand>();
        }
        public void RegisterCommand(Buttons button, ICommand command)
        {
            controllerMappings.Add(button, command);
        }
        public void Update()
        {
            GamePadState pressedButtons = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);

            /*
             * foreach (Buttons in pressedButtons.IsButtonDown())
             *   {
             *       if (controllerMappings.ContainsKey(key))
             *       {
             *           controllerMappings[key].Execute();
             *       }
             *   }
             */
        }
    }
}
