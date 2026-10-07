using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class MouseController : IController
    {
        private Dictionary<MouseButton, ICommand> controllerMappings;
        private MouseState CurrentState { get; set; }

        public MouseController(Game1 myGame)
        {
            controllerMappings = new Dictionary<MouseButton, ICommand>();
            //all mouse controls
            RegisterCommand(MouseButton.Right, new NextRoomCommand(myGame));
            RegisterCommand(MouseButton.Left, new PreviousRoomCommand(myGame));

        }
        public void RegisterCommand(MouseButton mouseButton, ICommand command)
        {
            controllerMappings.Add(mouseButton, command);
        }
        public bool Update()
        {
            bool mouseUsed = false;
            CurrentState = Mouse.GetState();
            foreach (MouseButton btn in Enum.GetValues(typeof(MouseButton)))
            {
                if (controllerMappings.ContainsKey(btn) && ButtonPressed(btn))
                {
                    controllerMappings[btn].Execute();
                    mouseUsed = true;
                }
            }
            return mouseUsed;
        }
        private bool ButtonPressed(MouseButton button)
        {
            if (button == MouseButton.Left)
                return CurrentState.LeftButton == ButtonState.Pressed;
            else if (button == MouseButton.Middle)
                return CurrentState.MiddleButton == ButtonState.Pressed;
            else if (button == MouseButton.Right)
                return CurrentState.RightButton == ButtonState.Pressed;
            else if (button == MouseButton.X1)
                return CurrentState.XButton1 == ButtonState.Pressed;
            else if (button == MouseButton.X2)
                return CurrentState.XButton2 == ButtonState.Pressed;
            return false;
        }
    }
}
