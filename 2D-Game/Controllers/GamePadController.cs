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
        private Dictionary<Buttons, ICommand> buttonMappings;
        public GamePadController()
        {
            buttonMappings = new Dictionary<Buttons, ICommand>();
        }
        public void RegisterCommand(Buttons button, ICommand command)
        {
            buttonMappings.Add(button, command);
        }
        public void Update()
        {
            
              //Button Control//
              foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
                {
                    if (buttonMappings.ContainsKey(btn))
                    {
                        if(GamePadState.Default.IsButtonDown(btn))
                        buttonMappings[btn].Execute();
                    }
                }


             
        }
    }
}
