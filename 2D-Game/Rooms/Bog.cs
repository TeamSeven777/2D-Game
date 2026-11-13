using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class Bog
{
        private Game1 myGame;
        private DefaultPlayer player;
        private Camera Camera;
        private Texture2D bogBackground;
        private Rectangle bogArea;
        private Rectangle bogEntrance; //walking into this on the main map teleports the player
        public bool InBog { get; private set; }
        public Bog(Game1 game1) {
            myGame = game1;
            player = myGame.player;
            Camera = myGame.Camera;
            InBog = false;
        }
        public void LoadContent(ContentManager content) {
            bogBackground = content.Load<Texture2D>("Images/bogBkg");

            // Room (4,4) is clear of the oversized right and bottom main map walls
            bogArea = new Rectangle(4 * myGame.roomWidth, 4 * myGame.roomHeight, myGame.roomWidth, myGame.roomHeight);
            bogEntrance = new Rectangle(myGame.worldWidth / 2 - 150, 0, 300, 100); //grass above the top center path

            int wall = 32; //the second Boundary argument is a width and height
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top - wall), new Vector2(bogArea.Width + wall * 2, wall)));   // up
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Bottom), new Vector2(bogArea.Width + wall * 2, wall)));       // down
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Left - wall, bogArea.Top), new Vector2(wall, bogArea.Height)));                    // left
            myGame.boundaries.Add(new Boundary(new Vector2(bogArea.Right, bogArea.Top), new Vector2(wall, bogArea.Height)));                          // right
        }
        public void Update(GameTime gt) {
            if (InBog) CheckBogExits();
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
            const int sideEdge = 40; //how close to a wall counts as reaching that edge
            const int bottomEdge = 90; //larger because the hitbox extends below the player's position
            const int arrivalInset = 120; //far enough from each edge that arriving can't trigger another exit

            Vector2 position = player.Position;
            if (position.X >= bogArea.Right - sideEdge)
            {
                player.Teleport(new Vector2(bogArea.Center.X, bogArea.Top + arrivalInset));
            }
            else if (position.Y <= bogArea.Top + sideEdge)
            {
                player.Teleport(new Vector2(bogArea.Left + arrivalInset, bogArea.Center.Y));
            }
            else if (position.Y >= bogArea.Bottom - bottomEdge)
            {
                player.Teleport(new Vector2(bogArea.Right - arrivalInset, bogArea.Center.Y));
            }
            else if (position.X <= bogArea.Left + sideEdge)
            {
                LeaveBog();
            }
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
        }
    }
}
