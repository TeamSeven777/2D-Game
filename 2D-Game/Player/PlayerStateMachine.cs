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
        public IPlayerState CurrPlayerState { get; private set; }
        private DefaultPlayer player;
        public Vector2 Position { get; private set; }

        public PlayerStateMachine(DefaultPlayer myPlayer) {
            Position = myPlayer.Position;
            player = myPlayer;
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
        private bool isDead = false;
        private bool BoggusMode = false;
        public void ChangeDirection(Direction dir) {
            direction = dir;            
        }

        public void UseTool(Tool tool)
        {
            switch (tool)
            {
                case Tool.Fist:
                    toolEquiped = new Fist(this);          
                    break;
                case Tool.Stick:
                    toolEquiped = new Stick(this);
                    break;
                case Tool.Slingshot:
                    toolEquiped = new Slingshot();
                    break;
                default:
                    toolEquiped = new NoTool();
                    break;
            }
            //If tool isn't changed, use previous one
            previousToolUsed = toolUsed;
            toolUsed = toolEquiped.GetTool();
        }
        public void ToggleBoggle()
        {
            if (BoggusMode) BoggusMode = false;
            else BoggusMode = true;
            ChangeOccurred = true;
        }
        public void KillPlayer()
        {
            if(CurrPlayerState.GetType() != typeof(DeadPlayerState))CurrPlayerState = new DeadPlayerState(this);
            isDead = true;
            CurrPlayerState.PlayerSprite.Color = Color.Red;
            player.MoveDown();
        }
        //
        public void Update(GameTime gt)
        {
            Position = player.Position;
            //TODO make it so item uses prohibit change in state, and execute once per button press
            //TODO adjust Player Sprites in State classes to display more appropriately and have the appropriate origins.
            if (direction != previousDirection || Moving != PreviousMoving || toolUsed != previousToolUsed || Damaged != PreviousDamaged) ChangeOccurred = true;
            previousDirection = direction; PreviousMoving = Moving; previousToolUsed = toolUsed; PreviousDamaged = Damaged;
            if (toolUsed != Tool.None || Damaged) CanMove = false;
            else CanMove = true;
            if (ChangeOccurred && !isDead)
            {
                    switch (toolUsed)
                    {
                        case (Tool.Stick):
                            CurrPlayerState = new StickPlayerState(this);
                            break;
                        case (Tool.Fist):
                            CurrPlayerState = new FistPlayerState(this);
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
                if (Damaged) CurrPlayerState = new DamagedPlayerState(this);
                else CurrPlayerState.PlayerSprite.Color = Color.White;
                ChangeOccurred = false;
            }
            
            Boggus bog = new Boggus();
            if(BoggusMode) CurrPlayerState.PlayerSprite = bog.GetBoggusSprite();


            CurrPlayerState.Update(gt);
        }
        
    }
}
