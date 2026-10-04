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
        public GamePadController(PlayerIndex index)
        {
            buttonMappings = new Dictionary<Buttons, ICommand>();
            CurIndex = index;
        }
        public void RegisterCommand(Keys key, ICommand command) { }
        /*public void RegisterCommand(Buttons button, ICommand command)
        {
            buttonMappings.Add(button, command);
        }*/

        /*
         * This implementation DOES work, but I dont want to
         * include it yet since it modifies IController in
         * an undesireable way, so maybe we workshop this
         * and double back?
         */

        public void Update()
        {
            
              //Button Control//
              foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
                {
                    if (buttonMappings.ContainsKey(btn))
                    {
                        if(GamePad.GetState(CurIndex).IsButtonDown(btn))
                        buttonMappings[btn].Execute();
                    }
                }


             
        }
    }
}
