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
        public Texture2D PlayerSpriteSheet { get; private set; }
        public AnimatedSprite PlayerSprite { get; private set; }

        public PlayerStateMachine(Texture2D playerSpriteSheet) { 
            PlayerSpriteSheet = playerSpriteSheet;
            //add in others? or just update in Update.
            Animation animationPlayerDown = new Animation(playerSpriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(25, 25), Vector2.Zero, playerSpriteSheet.Width, Animation.Style.Walking);
            

            PlayerSprite = new AnimatedSprite(animationPlayerDown, playerSpriteSheet);
            PlayerSprite.Scale = new Vector2(4.0f);
        }
        public Direction direction;
        public ITool toolEquiped;
        public Tool toolUsed;
        public bool Damaged = false;

        public void ChangeDirection(Direction dir) {
            direction = dir;
        }
        public void BeDamaged(bool dmg)
        {
            Damaged = dmg;
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
        public void Update(GameTime gt) { 
            //TODO here is where you update sprite based on direction, tool, damaged, etc.
        }

        // Then other functions for other states etc...
    }
}
