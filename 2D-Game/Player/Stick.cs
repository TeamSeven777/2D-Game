using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game 
{
    public class Stick : ITool
    {
        private PlayerStateMachine stateMachine;
        private ISprite stickSprite;

        private Vector2 playerOffset;
        private int offsetValueX = 20;
        private int offsetValueHY = 32;


        public Tool GetTool()
        {
            return Tool.Stick;
        }
        public Stick(PlayerStateMachine playerStateMachine)
        {
            stateMachine = playerStateMachine;
            stickSprite = PlayerSpriteFactory.Instance.CreateToolAttack(GetTool(), stateMachine.direction);
            switch (stateMachine.direction)
            {
                case Direction.Left:
                    playerOffset = new Vector2(-offsetValueX, offsetValueHY);
                    break;
                case Direction.Right:
                    playerOffset = new Vector2(offsetValueX, offsetValueHY);
                    break;
                case Direction.Up:
                    playerOffset = new Vector2(0, -offsetValueHY);
                    break;
                default: //down
                    playerOffset = new Vector2(0, offsetValueHY);
                    break;
            }
            
        }
        public void Use()
        {
            //TODO add proper function
        }
        //Update and draw methods to interface for separate draw
        public void Update(GameTime gt)
        {
            stickSprite.Update(gt);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position) 
        {
            stickSprite.Draw(spriteBatch, position + playerOffset);
        }
    }
}
