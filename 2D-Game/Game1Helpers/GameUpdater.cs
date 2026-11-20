using Microsoft.Xna.Framework;
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
    public class GameUpdater
{
        private Game1 myGame;
        private GameStateManager myStateManager;
        public GameUpdater(Game1 game1, GameStateManager stateManager) 
        {
            myGame = game1;
            myStateManager = stateManager;
        }
        private void UpdateControllers()
        {
            foreach (ControllerName controllerType in myStateManager.controllerNames)
            {
                if (myStateManager.controllers[controllerType].Update()) break; //only use one controller type at a time.

            }
        }
        public void UpdatePlay(GameTime gameTime)
        {

            RoomCollisionHandler.Instance.HandleCollision();

            //Everything we are updating
            //To make it more "fair", everything
            //The player does should come first
            //~Player Actions~
            UpdateControllers();
            if (myGame.IsActive && !myGame.bog.InBog && myGame.minimap.TryGetClickedRoomCenter(out Vector2 roomCenter))
            {
                myGame.player.Teleport(roomCenter);
                myGame.Camera.Follow(myGame.player.Position);
            }

            myGame.player.Update(gameTime);
            myGame.bog.Update(gameTime);
            myGame.roomManager.Update(gameTime, myGame.player.Position);
            PlayerProjectileHandler.Instance.Update(gameTime);
            WeaponInventory.Instance.Update(gameTime);
            ItemInventory.Instance.Update(gameTime, myGame.roomManager.CurrentRoom);
            myGame.weaponHotbar.Update(gameTime);
            myGame.itemHotbar.Update(gameTime);
            myGame.deathHandler.Update(gameTime);
            myGame.CurTime.Update(gameTime);
            myGame._statusDisplay.Update();

            //~Whatever Else Actions~
            myGame.Enemies.Update(gameTime, myGame.player.Position);
            EnemyProjectileHandler.Instance.Update(gameTime);


            RoomCollisionHandler.Instance.Update(gameTime);
            myGame.Camera.Follow(myGame.player.Position);
            myGame.hitboxMode.Update(gameTime);
        }

        public void UpdatePause(GameTime gameTime) 
        {
            UpdateControllers();
            myGame.pauseMenu.Update(gameTime);            
        }
        public void UpdateStart(GameTime gameTime)
        {
            UpdateControllers();
        }
        public void UpdateEnd(GameTime gameTime)
        {
            UpdateControllers();
        }
        public void UpdateDialogue(GameTime gameTime)
        {
            UpdateControllers();
            myGame.CurrentDialogue.Update(gameTime);
            myGame.player.Update(gameTime);//idle animation
        }
    }
}
