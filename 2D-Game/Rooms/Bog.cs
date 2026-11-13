using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    /*
     * THE BOG: a confusing room that you musn't enter 
     * until you find the clues around the map that 
     * tell you how to get through it.
     */
    public class Bog
{
        private Game1 myGame;
        private DefaultPlayer player;
        private Camera Camera;
        private Texture2D bogBackground;
        private Rectangle bogArea;
        private Rectangle bogEntrance; //walking into this on the main map teleports the player
        private List<Direction> CorrectSequence;
        private List<Direction> PlayerSequence;
        private PlayerDetector LeftExit, RightExit, UpExit, DownExit;
        private bool BogSolved = false;
        private int sequenceCount = 0; //PlayerSequence count
        private int IncorrectCount = 0; private int IncorrectDamageThreshold = 5;
        public bool InBog { get; private set; }
        private int offsetY = -100;
        private int offsetX = 50;

        private WorldItem BogLog = null;
        public Bog(Game1 game1) {
            myGame = game1;
            player = myGame.player;
            Camera = myGame.Camera;
            InBog = false;
            CorrectSequence = new List<Direction>();
            CorrectSequence.Add(Direction.Up);
            CorrectSequence.Add(Direction.Down);
            CorrectSequence.Add(Direction.Down);
            CorrectSequence.Add(Direction.Right);
            PlayerSequence = new List<Direction>();
        }
        public void LoadContent(ContentManager content) {
            bogBackground = content.Load<Texture2D>("Images/bogBkg");

            // Room (4,4) is clear of the oversized right and bottom main map walls
            bogArea = new Rectangle(4 * myGame.roomWidth, 4 * myGame.roomHeight, myGame.roomWidth, myGame.roomHeight);
            bogEntrance = new Rectangle(myGame.worldWidth / 2 - 150, 0, 300, 100); //grass above the top center path

            int wall = 32; //bog room bounds
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top - wall), new Vector2(bogArea.Width + wall * 2, wall)));   // up
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Bottom), new Vector2(bogArea.Width + wall * 2, wall)));       // down
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top), new Vector2(wall, bogArea.Height)));                    // left
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Right, bogArea.Top), new Vector2(wall, bogArea.Height)));                          // right
            //water bounds
            int topLeftWaterHeight = 100; int topRightWaterHeight = 90; int topLeftWaterWidth = 430; int topRightWaterWidth = 300;
            int bottomLeftWaterHeight = 190; int bottomRightWaterHeight = 140; int bottomLeftWaterWidth = 375; int bottomRightWaterWidth = 400;
            myGame.boundaries.Add(new Boundary(bogArea.Location.ToVector2(), new Vector2(topLeftWaterWidth, topLeftWaterHeight)));//topleft water - about half curr height, 33% wider
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Right - topRightWaterWidth, bogArea.Top), new Vector2(topRightWaterWidth, topRightWaterHeight)));//topright water - about half height, *3 wide
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left, bogArea.Bottom-bottomLeftWaterHeight), new Vector2(bottomLeftWaterWidth, bottomLeftWaterHeight)));//bottomleft water -  -10 ht, + 1/4 wid
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Right-bottomRightWaterWidth, bogArea.Bottom - bottomRightWaterHeight), new Vector2(bottomRightWaterWidth, bottomRightWaterHeight)));//bottomright water - 2/3 ht, *4 wid
            //offset Y for left and Right: try -100; xfor top +100; xfor bottom +100 and fix y offset
            int detectorWidth = 120; int detectorLength = 40; int bottomEdge = 90;
            LeftExit = new PlayerDetector(new Rectangle(new Point(bogArea.Left, bogArea.Center.Y-detectorWidth/2+offsetY), new Point(detectorLength, detectorWidth)));
            RightExit = new PlayerDetector(new Rectangle(new Point(bogArea.Right-detectorLength, bogArea.Center.Y - detectorWidth / 2+offsetY), new Point(detectorLength, detectorWidth)));
            UpExit = new PlayerDetector(new Rectangle(new Point(bogArea.Center.X-detectorWidth/2+offsetX, bogArea.Top), new Point(detectorWidth, detectorLength)));
            DownExit = new PlayerDetector(new Rectangle(new Point(bogArea.Center.X - detectorWidth/2 + offsetX, bogArea.Bottom-bottomEdge/2), new Point(detectorWidth, bottomEdge)));
        }
        public void Update(GameTime gt) {
            if (InBog && BogSolved) { 
                CheckBogExits();
                if (BogLog != null && BogLog.IsCollected) BogLog = null;
            }
            else if (InBog)
            {
                CheckBogExits();
                //restart player sequence if they take a wrong turn.
                if (sequenceCount > 0 && sequenceCount < CorrectSequence.Count && PlayerSequence[sequenceCount - 1] != CorrectSequence[sequenceCount - 1])
                {
                    PlayerSequence = new List<Direction>();
                    IncorrectCount++;
                    if (IncorrectCount >= IncorrectDamageThreshold) player.TakeDamage(1); //start to take damage for each fail after threshold reached.
                }
                else if (sequenceCount == CorrectSequence.Count && PlayerSequence[sequenceCount - 1] == CorrectSequence[sequenceCount - 1])
                {
                    BogSolved = true; //player navigated the bog to the log, and can now exit
                    SoundEffectFactory.Instance.PlaySoundEffect(SoundName.PuzzleSolve1, false);
                    //create log in center
                    BogLog = new WorldItem(RoomContentFactory.Instance.CreateItem("Log"), new Vector2(bogArea.Left + bogArea.Width / 2, bogArea.Top + bogArea.Height / 2));
                }
            }
            else if (bogEntrance.Contains(player.Position.ToPoint())) EnterBog();
        }

        private void EnterBog()
        {
            InBog = true;
            Camera.SetBounds(bogArea);
            player.Teleport(new Vector2(bogArea.Center.X, bogArea.Bottom - 140)); //arrive at the bottom center of the bog
            Camera.Follow(player.Position);
        }

        /// <summary>
        /// Right goes to the top, top goes to the left, bottom goes to the right, and left leaves the bog
        /// </summary>
        private void CheckBogExits()
        {
            const int arrivalInset = 120; //far enough from each edge that arriving can't trigger another exit

            Vector2 position = player.Position;
            bool exitTaken = true;
            if (RightExit.PlayerDetected)//right
            {
                player.Teleport(new Vector2(bogArea.Center.X+offsetX, bogArea.Top + arrivalInset));
                PlayerSequence.Add(Direction.Right);
                RightExit.PlayerDetected = false; //if this is not done the player will be doomed forever.

            }
            else if (UpExit.PlayerDetected)//up
            {
                player.Teleport(new Vector2(bogArea.Left + arrivalInset, bogArea.Center.Y+offsetY));
                PlayerSequence.Add(Direction.Up);
                UpExit.PlayerDetected = false;
            }
            else if (DownExit.PlayerDetected)//down
            {
                player.Teleport(new Vector2(bogArea.Right - arrivalInset, bogArea.Center.Y+offsetY));
                PlayerSequence.Add(Direction.Down);
                DownExit.PlayerDetected = false;
            }
            else if (LeftExit.PlayerDetected)//left
            {
                player.Teleport(new Vector2(bogArea.Center.X+offsetX, bogArea.Bottom - arrivalInset));
                PlayerSequence.Add(Direction.Left);
                LeftExit.PlayerDetected = false;
            }
            else {
                exitTaken = false;
            }

            if (BogSolved && exitTaken) LeaveBog();//player can exit only after solving the bog puzzle
            sequenceCount = PlayerSequence.Count();
        }

        private void LeaveBog()
        {
            InBog = false;
            Camera.SetBounds(new Rectangle(0, 0, myGame.worldWidth, myGame.worldHeight));
            player.Teleport(new Vector2(myGame.worldWidth / 2, 290)); //on the top center path, below the bog entrance
            Camera.Follow(player.Position);
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bogBackground, bogArea, Color.White);
            if (BogLog != null && !BogLog.IsCollected) BogLog.Draw(spriteBatch); //draw when puzzle is complete until picked up.
        }
    }
}
