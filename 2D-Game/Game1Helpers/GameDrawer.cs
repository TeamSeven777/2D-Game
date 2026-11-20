using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static _2D_Game.AnimatedSprite;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _2D_Game
{
    public class GameDrawer
{
        private Game1 myGame;
        public GameDrawer(Game1 game1)
        {
            myGame = game1;
        }
        public void DrawStartOrEnd(GameTime gameTime, SpriteBatch _spriteBatch)
        {
                _spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
                Texture2D screen = myGame.StateManager.CurrentState == GameState.Start ? myGame.startScreen : myGame.endScreen;
                _spriteBatch.Draw(screen, new Rectangle(0, 0, myGame.roomWidth, myGame.roomHeight), Color.White);
                _spriteBatch.End();
        }
        public void DrawGame(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            


            // World: scrolls with the camera
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: myGame.Camera.Transform);
            _spriteBatch.Draw(myGame.background, new Rectangle(0, 0, myGame.worldWidth, myGame.worldHeight), Color.White);
            myGame.bog.Draw(_spriteBatch);

            //Everything we are drawing
            myGame.fishingHole.Draw(_spriteBatch);
            myGame.roomManager.Draw(_spriteBatch);
            foreach (IObstacle bound in myGame.boundaries) bound.Draw(_spriteBatch);
            PlayerProjectileHandler.Instance.Draw(_spriteBatch);
            EnemyProjectileHandler.Instance.Draw(_spriteBatch);
            myGame.Enemies.Draw(_spriteBatch);
            //foreach(Boundary boundary in boundaries) boundary.Draw(_spriteBatch);

            myGame.player.Draw(_spriteBatch);
            //for hitbox debugging
            myGame.hitboxMode.DrawHitboxes(_spriteBatch);
            //

            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // no transform
            myGame._statusDisplay.Draw(_spriteBatch);
            myGame.CurTime.Draw(_spriteBatch);
            if (!myGame.bog.InBog) myGame.minimap.Draw(_spriteBatch, myGame.player.Position); //the minimap only covers the main 3x3 map
            myGame.weaponHotbar.Draw(_spriteBatch);
            myGame.itemHotbar.Draw(_spriteBatch);
            if (myGame.StateManager.CurrentState == GameState.Pause) myGame.pauseMenu.Draw(_spriteBatch);
            myGame.CurrentDialogue.Draw(_spriteBatch, new Vector2(myGame.middleOfScreen.X, myGame.middleOfScreen.Y + myGame.roomHeight / 4)); //draw 3/4 down screen in middle
            myGame.deathHandler.Draw(_spriteBatch);
            _spriteBatch.End();
        }
}
}
