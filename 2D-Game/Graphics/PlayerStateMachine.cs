using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _2D_Game
{
    public class PlayerStateMachine : IStateMachine
    {

        //Here for other animations of the player
        //private enum 
        public Texture2D spriteSheet { get; private set; }
        public IPlayerState CurrPlayerState { get; private set; }

        public PlayerStateMachine(Texture2D playerSpriteSheet) { 
            this.spriteSheet = playerSpriteSheet;

            direction = Direction.Down;
            toolEquiped = new NoTool();
            toolUsed = Tool.None;
            CurrPlayerState = new DownIdlePlayerState(spriteSheet);
        }
        public Direction direction, previousDirection;
        public ITool toolEquiped;
        public Tool toolUsed, previousToolUsed;
        public bool Damaged{ get; set; }
        private bool PreviousDamaged;
        public bool Moving {  get;  set; }
        private bool PreviousMoving;
        private bool ChangeOccurred = false;

        public void ChangeDirection(Direction dir) {
            direction = dir;            
        }

        public void UseTool(Tool tool)
        {
            switch (tool)
            {
                case Tool.Knife:
                    toolEquiped = new Knife();          
                    break;
                case Tool.Axe:
                    toolEquiped = new Axe();
                    break;
                case Tool.Slingshot:
                    toolEquiped = new Slingshot();
                    break;
                default:
                    toolEquiped = new NoTool();
                    break;
            }
            toolUsed = toolEquiped.GetTool();
        }

        //
        public void Update(GameTime gt)
        {
            //TODO make it so item uses prohibit change in state, and execute once per button press
            //TODO adjust Player Sprites in State classes to display more appropriately and have the appropriate origins.
            if (direction != previousDirection || Moving != PreviousMoving || toolUsed != previousToolUsed || Damaged != PreviousDamaged) ChangeOccurred = true;
            previousDirection = direction; PreviousMoving = Moving; previousToolUsed = toolUsed; PreviousDamaged = Damaged;
            if (ChangeOccurred)
            {
                switch (direction)
                {
                    case (Direction.Left):
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerState = new LeftKnifePlayerState(spriteSheet);
                                break;
                            case (Tool.Axe):
                                CurrPlayerState = new LeftAxePlayerState(spriteSheet);
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerState = new LeftSlingPlayerState(spriteSheet);
                                break;
                            case (Tool.None):
                                if (Moving) CurrPlayerState = new LeftMovePlayerState(spriteSheet);
                                else CurrPlayerState = new LeftIdlePlayerState(spriteSheet);
                                break;
                        }
                        break;
                    case (Direction.Right):

                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerState = new RightKnifePlayerState(spriteSheet);
                                break;
                            case (Tool.Axe):
                                CurrPlayerState = new RightAxePlayerState(spriteSheet);
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerState = new RightSlingPlayerState(spriteSheet);
                                break;
                            case (Tool.None):
                                if (Moving) CurrPlayerState = new RightMovePlayerState(spriteSheet);
                                else CurrPlayerState = new RightIdlePlayerState(spriteSheet);
                                break;
                        }
                        break;
                    case (Direction.Up):
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerState = new UpKnifePlayerState(spriteSheet); ;
                                break;
                            case (Tool.Axe):
                                CurrPlayerState = new UpAxePlayerState(spriteSheet);
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerState = new UpSlingPlayerState(spriteSheet);
                                break;
                            case (Tool.None):
                                if (Moving) CurrPlayerState = new UpMovePlayerState(spriteSheet);
                                else CurrPlayerState = new UpIdlePlayerState(spriteSheet);
                                break;
                        }
                        break;
                    case (Direction.Down):
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerState = new DownKnifePlayerState(spriteSheet); ;
                                break;
                            case (Tool.Axe):
                                CurrPlayerState = new DownAxePlayerState(spriteSheet);
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerState = new DownSlingPlayerState(spriteSheet);
                                break;
                            case (Tool.None):
                                if (Moving) CurrPlayerState = new DownMovePlayerState(spriteSheet);
                                else CurrPlayerState = new DownIdlePlayerState(spriteSheet);
                                break;
                        }
                        break;
                }
                //CurrPlayerState
                if (Damaged) CurrPlayerState.PlayerSprite.Color = Color.Red;
                else CurrPlayerState.PlayerSprite.Color = Color.White;
                ChangeOccurred = false;
            }
            CurrPlayerState.Update(gt);
        }
        
    }
}
