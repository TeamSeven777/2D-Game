using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class HitboxMode
{
        private Game1 myGame;
        private List<Rectangle> boxes;
        private Texture2D hitboxTexture;
        public HitboxMode(Game1 game) {
            hitboxTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            hitboxTexture.SetData<Color>(new Color[] { Color.White });
            myGame = game;
            boxes = new List<Rectangle>();
            boxes.Add(myGame.player.HitBox.BoundingBox);
            boxes.Add(myGame.block.BoundingBox.BoundingBox);
        }
        public void Update() { 
            boxes = new List<Rectangle>();
            boxes.Add(myGame.player.HitBox.BoundingBox);
            boxes.Add(myGame.block.BoundingBox.BoundingBox);
        }
        public void DrawHitboxes(SpriteBatch spriteBatch) {

            //myGame.player.HitBox.BoundingBox
            // myGame.block.BoundingBox.BoundingBox
            //myGame.
            foreach (Rectangle box in boxes) {
                spriteBatch.Draw(hitboxTexture, box, Color.White);
            }
        }
}
}
