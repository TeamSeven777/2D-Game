using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game 
{
    public class Stick : ITool, IHitboxOwner
    {
        private PlayerStateMachine stateMachine;
        private RotatingSprite stickSprite;
        public IHitbox Hitbox { get; set; }

        private Vector2 playerOffset;
        private int offsetValueX = 20;
        private int offsetValueHY = 32;

        private int hbWidth = 128;
        private int hbLength = 64;
        private Vector2 Position;

        public Tool GetTool()
        {
            return Tool.Stick;
        }
        public Stick(PlayerStateMachine playerStateMachine)
        {
            stateMachine = playerStateMachine;
            Position = stateMachine.Position;
            stickSprite = (RotatingSprite)PlayerSpriteFactory.Instance.CreateToolAttack(GetTool(), stateMachine.direction);
            switch (stateMachine.direction)
            {
                case Direction.Left:
                    playerOffset = new Vector2(-offsetValueX, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X -offsetValueX - hbLength, (int)(Position.Y + offsetValueHY - hbWidth / 2), hbLength, hbWidth), this, stateMachine.direction);
                    break;
                case Direction.Right:
                    playerOffset = new Vector2(offsetValueX, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X + offsetValueX, (int)(Position.Y - hbWidth / 2), hbLength, hbWidth), this, stateMachine.direction);
                    break;
                case Direction.Up:
                    playerOffset = new Vector2(0, -offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X - hbWidth / 2, (int)(Position.Y -offsetValueHY - hbLength), hbWidth, hbLength), this, stateMachine.direction);
                    break;
                default: //down
                    playerOffset = new Vector2(0, offsetValueHY);
                    Hitbox = new PlayerAttackHitBox(new Rectangle((int)Position.X - hbWidth / 2, (int)(Position.Y + offsetValueHY), hbWidth, hbLength), this, stateMachine.direction);
                    break;
            }
            RoomCollisionHandler.Instance.Add(this, true);

        }
        public void Use()
        {
            //TODO add proper function
        }
        //Update and draw methods to interface for separate draw
        public void Update(GameTime gt)
        {
            stickSprite.Update(gt);
            Position = stateMachine.Position;
            if(stickSprite.Paused) RoomCollisionHandler.Instance.Remove(this);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position) 
        {
            stickSprite.Draw(spriteBatch, position + playerOffset);
        }
    }
}
