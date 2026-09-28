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
        public AnimatedSprite CurrPlayerSprite { get; private set; }

        private AnimatedSprite PlayerUseUpSpr;
        private AnimatedSprite PlayerUseDownSpr;
        private AnimatedSprite PlayerUseLeftSpr;
        private AnimatedSprite PlayerUseRightSpr;
        private Animation animationPlayerUp;
        private Animation animationPlayerDown;
        private Animation animationPlayerLeft;
        private Animation animationPlayerRight;
        private Animation animationAxePlayerRightSpr;
        private Animation animationAxePlayerLeftSpr;
        private Animation animationAxePlayerUpSpr;
        private Animation animationAxePlayerDownSpr;
        private Animation animationKnifePlayerRightSpr;
        private Animation animationKnifePlayerLeftSpr;
        private Animation animationKnifePlayerUpSpr;
        private Animation animationKnifePlayerDownSpr;
     
        private AnimatedSprite playerDownSpr;
        private AnimatedSprite playerUpSpr;
        private AnimatedSprite playerLeftSpr;
        private AnimatedSprite playerRightSpr;
        private AnimatedSprite AxePlayerRightSpr;
        private AnimatedSprite AxePlayerLeftSpr;
        private AnimatedSprite AxePlayerUpSpr;
        private AnimatedSprite AxePlayerDownSpr;
        private AnimatedSprite KnifePlayerRightSpr;
        private AnimatedSprite KnifePlayerLeftSpr;
        private AnimatedSprite KnifePlayerUpSpr;
        private AnimatedSprite KnifePlayerDownSpr;

        public PlayerStateMachine(Texture2D playerSpriteSheet) { 
            this.spriteSheet = playerSpriteSheet;
            //add in others? or just update in Update.

            animationPlayerDown = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(0, 10), 34);
            animationPlayerUp = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(68, 10), 34);
            animationPlayerLeft = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(34, 10), 34);
            animationPlayerRight = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 17), new Vector2(34, 10), 34);

            animationAxePlayerDownSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(0, 47), 68);
            animationAxePlayerUpSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(0, 78), 68);
            animationAxePlayerLeftSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(0, 78), 68);
            animationAxePlayerRightSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(0, 97), 68);

            animationKnifePlayerDownSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(94, 47), 68);
            animationKnifePlayerUpSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(17, 28), new Vector2(84, 78), 68);
            animationKnifePlayerLeftSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(84, 78), 68);
            animationKnifePlayerRightSpr = new Animation(spriteSheet, TimeSpan.FromMilliseconds(125), new Vector2(28, 17), new Vector2(94, 97), 68);


            playerDownSpr = new AnimatedSprite(animationPlayerDown, spriteSheet);
            playerDownSpr.Scale = new Vector2(4.0f);
            playerUpSpr = new AnimatedSprite(animationPlayerUp, spriteSheet);
            playerUpSpr.Scale = new Vector2(4.0f);
            playerLeftSpr = new AnimatedSprite(animationPlayerLeft, spriteSheet, SpriteEffects.FlipHorizontally);
            playerLeftSpr.Scale = new Vector2(4.0f);
            playerRightSpr = new AnimatedSprite(animationPlayerRight, spriteSheet);
            playerRightSpr.Scale = new Vector2(4.0f);
            //PlayerHurtSpr for when or if its needed; 

            AxePlayerDownSpr = new AnimatedSprite(animationAxePlayerDownSpr, spriteSheet);
            AxePlayerUpSpr = new AnimatedSprite(animationAxePlayerUpSpr, spriteSheet);
            AxePlayerRightSpr = new AnimatedSprite(animationAxePlayerRightSpr, spriteSheet);
            AxePlayerLeftSpr = new AnimatedSprite(animationAxePlayerLeftSpr, spriteSheet, SpriteEffects.FlipHorizontally);

            KnifePlayerDownSpr = new AnimatedSprite(animationKnifePlayerDownSpr, spriteSheet);
            KnifePlayerUpSpr = new AnimatedSprite(animationKnifePlayerUpSpr, spriteSheet);
            KnifePlayerRightSpr = new AnimatedSprite(animationKnifePlayerRightSpr, spriteSheet);
            KnifePlayerLeftSpr = new AnimatedSprite(animationKnifePlayerLeftSpr, spriteSheet, SpriteEffects.FlipHorizontally);

            //These dont compile, something to do with the explicit cast
            //PlayerUseDownSpr = (AnimatedSprite) new Sprite(spriteSheet, new Rectangle(107, 10, 17, 17), new Vector2(4.0f));
            //PlayerUseUpSpr =(AnimatedSprite) tempUp;
            //PlayerUseLeftSpr = (AnimatedSprite) new Sprite(spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f), SpriteEffects.FlipHorizontally);
            //PlayerUseRightSpr = (AnimatedSprite) new Sprite(spriteSheet, new Rectangle(123, 10, 17, 17), new Vector2(4.0f));


            toolEquiped = new NoTool();
            CurrPlayerSprite = playerDownSpr;
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
            switch (direction)
            {
                case(Direction.Left):
                    if (toolEquiped.GetTool() != Tool.None)
                    {
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerSprite = KnifePlayerLeftSpr;
                                break;
                            case (Tool.Axe):
                                CurrPlayerSprite = AxePlayerLeftSpr;
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerSprite = PlayerUseLeftSpr;
                                break;
                        }
                        break;
                    }
                    else
                    {
                        CurrPlayerSprite = playerLeftSpr;
                    }
                    break;
                case (Direction.Right):
                    if (toolEquiped.GetTool() != Tool.None)
                    {
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerSprite = KnifePlayerRightSpr;
                                break;
                            case (Tool.Axe):
                                CurrPlayerSprite = AxePlayerRightSpr;
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerSprite = PlayerUseRightSpr;
                                break;
                        }
                        break;
                    }
                    else
                    {
                        CurrPlayerSprite = playerRightSpr;
                    }
                    break;
                case (Direction.Up):
                    if (toolEquiped.GetTool() != Tool.None)
                    {
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerSprite = KnifePlayerUpSpr;
                                break;
                            case (Tool.Axe):
                                CurrPlayerSprite = AxePlayerUpSpr;
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerSprite = PlayerUseUpSpr;
                                break;
                        }
                        break;
                    }
                    else
                    {
                        CurrPlayerSprite = playerUpSpr;
                    }
                    break;
                case (Direction.Down):
                    if (toolEquiped.GetTool() != Tool.None)
                    {
                        switch (toolUsed)
                        {
                            case (Tool.Knife):
                                CurrPlayerSprite = KnifePlayerDownSpr;
                                break;
                            case (Tool.Axe):
                                CurrPlayerSprite = AxePlayerDownSpr;
                                break;
                            case (Tool.Slingshot):
                                CurrPlayerSprite = PlayerUseDownSpr;
                                break;
                        }
                        break;
                    }
                    else
                    {
                        CurrPlayerSprite = playerDownSpr;
                    }
                    break;
            }
            CurrPlayerSprite.Update(gt);
        }


        // Then other functions for other states etc...
    }
}
