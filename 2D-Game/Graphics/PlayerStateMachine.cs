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
            CurrPlayerState = new IdlePlayerState(this);
        }
        public Direction direction, previousDirection;
        public ITool toolEquiped;
        public Tool toolUsed, previousToolUsed;
        public bool Damaged{ get; set; }
        private bool PreviousDamaged;
        public bool Moving {  get;  set; }
        private bool PreviousMoving;
        private bool ChangeOccurred = false;
        public bool CanMove { get; set; }
        private bool BoggusMode = false;
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
            previousToolUsed = toolUsed;
            toolUsed = toolEquiped.GetTool();
        }
        public void ToggleBoggle()
        {
            if (BoggusMode) BoggusMode = false;
            else BoggusMode = true;
            ChangeOccurred = true;
        }
        //
        public void Update(GameTime gt)
        {
            //TODO adjust Player Sprites in State classes to display more appropriately and have the appropriate origins.
            if (direction != previousDirection || Moving != PreviousMoving || toolUsed != previousToolUsed || Damaged != PreviousDamaged) ChangeOccurred = true;
            previousDirection = direction; PreviousMoving = Moving; previousToolUsed = toolUsed; PreviousDamaged = Damaged;
            if (toolUsed != Tool.None) CanMove = false;
            else CanMove = true;
            if (ChangeOccurred)
            {
                    switch (toolUsed)
                    {
                        case (Tool.Knife):
                            CurrPlayerState = new KnifePlayerState(this);
                            break;
                        case (Tool.Axe):
                            CurrPlayerState = new AxePlayerState(this);
                            break;
                        case (Tool.Slingshot):
                            CurrPlayerState = new SlingPlayerState(this);
                            break;
                        case (Tool.None):
                            if (Moving) CurrPlayerState = new MovePlayerState(this);
                            else CurrPlayerState = new IdlePlayerState(this);
                            break;
                    }
                 
                //CurrPlayerState
                if (Damaged) CurrPlayerState.PlayerSprite.Color = Color.Red;
                else CurrPlayerState.PlayerSprite.Color = Color.White;
                ChangeOccurred = false;
            }
            
            Boggus bog = new Boggus();
            if(BoggusMode) CurrPlayerState.PlayerSprite = bog.GetBoggusSprite();


            CurrPlayerState.Update(gt);
        }
        
    }
}
