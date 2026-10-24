using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class InteractTool : ITool, IHitboxOwner
    {
        private PlayerStateMachine stateMachine;
        public IHitbox Hitbox { get; set; }
        public Direction direction { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public bool hasInteracted { get; set; }
        private Vector2 playerOffset;
        private int offsetValueX = 20;
        private int offsetValueHY = 32;
        private Timer punchDelay;
        private int hbWidthVertical = 64;
        private int hbWidthHorizontal = 96;
        private int hbLength = 32;
        private Vector2 Position;
        public InteractTool(PlayerStateMachine playerStateMachine)
        {          
            stateMachine = playerStateMachine;
            Position = stateMachine.Position;
            switch (stateMachine.direction)
            {
                case Direction.Left:
                    playerOffset = new Vector2(-offsetValueX, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X - offsetValueX - hbLength, (int)(Position.Y - hbWidthHorizontal / 2+offsetValueHY/2), hbLength, hbWidthHorizontal), this, stateMachine.direction);
                    break;
                case Direction.Right:
                    playerOffset = new Vector2(offsetValueX, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X + offsetValueX, (int)(Position.Y - hbWidthHorizontal / 2+offsetValueHY/2), hbLength, hbWidthHorizontal), this, stateMachine.direction);
                    break;
                case Direction.Up:
                    playerOffset = new Vector2(0, -offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X - hbWidthVertical / 2, (int)(Position.Y - offsetValueHY - hbLength), hbWidthVertical, hbLength), this, stateMachine.direction);
                    break;
                default: //down
                    playerOffset = new Vector2(0, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X - hbWidthVertical / 2, (int)(Position.Y + offsetValueHY * 2), hbWidthVertical, hbLength), this, stateMachine.direction);
                    break;
            }
            direction = stateMachine.direction;
            CollisionHandler = new InteractionCollisionHandler(this);//temp?
            //RoomCollisionHandler.Instance.Add(this, true);
            punchDelay = new Timer(.3);
            hasInteracted = false;

        }
        public Tool GetTool()
        {
            return Tool.Interact;
        }
        public void Update(GameTime gt)
        {
            Position = stateMachine.Position;
            punchDelay.Update(gt);
            if (stateMachine.CurrPlayerState.GetType() != typeof(StickPlayerState)) RoomCollisionHandler.Instance.Remove(this);
            else if (punchDelay.Ringing)
            {
                RoomCollisionHandler.Instance.Add(this, true);
                RoomCollisionHandler.Instance.HandleCollision();
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position) { }
    }
}
