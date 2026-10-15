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
    /*
     * Create a list of hitboxes to display visually, for debugging purposes.
     */
    public class HitboxMode
{
        private Game1 myGame;
        private List<IHitbox> boxes;
        private List<Rectangle> rects; //any 
        private Texture2D hitboxTexture;
        private Timer toggleCooldown = new Timer(0.2);
        private bool HitboxesOn = false;
        private Color hitboxColor;
        
        public HitboxMode(Game1 game) {
            hitboxTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            hitboxTexture.SetData<Color>(new Color[] { Color.White });
            myGame = game;
            boxes = RoomCollisionHandler.Instance.GetHitboxes();
            rects = RoomCollisionHandler.Instance.GetRectangles();

        }
        //update hitbox locations and timer
        public void Update(GameTime gt) { 
            toggleCooldown.Update(gt);
            boxes = RoomCollisionHandler.Instance.GetHitboxes();
            rects = RoomCollisionHandler.Instance.GetRectangles();

        }
        //Draw each hitbox in the game for better debugging
        public void DrawHitboxes(SpriteBatch spriteBatch) {

            //myGame.player.HitBox.BoundingBox
            // myGame.block.BoundingBox.BoundingBox
            //myGame.
            if (HitboxesOn)
            {
                foreach (IHitbox box in boxes)
                {
                    switch (box.Type)
                    {
                        case HitBoxType.Player:
                            hitboxColor = Color.Green;
                            break;
                        case HitBoxType.Enemy:
                            hitboxColor = Color.HotPink;
                            break;
                        case HitBoxType.HurtBlock:
                            hitboxColor = Color.HotPink;
                            break;
                        case HitBoxType.PlayerAttack:
                            hitboxColor = Color.HotPink;
                            break;
                        case HitBoxType.EnemyAttack:
                            hitboxColor = Color.HotPink;
                            break;
                        case HitBoxType.Block:
                            hitboxColor = Color.White;
                            break;
                        default:
                            hitboxColor = Color.White;
                            break;
                    }
                    spriteBatch.Draw(hitboxTexture, box.BoundingBox, hitboxColor);
                }
                foreach (Rectangle rect in rects) {
                    spriteBatch.Draw(hitboxTexture, rect, Color.Orange);
                }
            }
        }
        public void ToggleHitboxes()
        {
            if (toggleCooldown.Ringing)
            {
                if (HitboxesOn) HitboxesOn = false;
                else HitboxesOn = true;
                toggleCooldown.Reset();
            }
        }
}
}
