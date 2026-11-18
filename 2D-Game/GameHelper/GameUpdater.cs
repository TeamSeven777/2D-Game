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
        private DefaultPlayer player;
        public GameUpdater(Game1 game1) 
        { 
            myGame = game1;
            this.player = myGame.player;
        }
        public void Update(GameTime gameTime)
        {
            this.player = myGame.player;
            if (myGame.State != GameState.Playing)
            {
                myGame.currentGameTime = gameTime;
                foreach (String controllerType in myGame.controllerNames) myGame.controllers[controllerType].Update();
                return;
            }
            else
            {

                RoomCollisionHandler.Instance.HandleCollision();
                myGame.currentGameTime = gameTime;

                //Everything we are updating
                //To make it more "fair", everything
                //The player does should come first
                //~Player Actions~
                foreach (String controllerType in myGame.controllerNames)
                {
                    if (myGame.controllers[controllerType].Update()) break; //only use one controller type at a time.

                }
                if (!myGame.PauseGameOn && !myGame.DialogueMode) //disable certain updates on Pause. //TODO have another check to determine whether this is due to dialogue or a menu, and update accordingly
                {
                    if (myGame.IsActive && !myGame.bog.InBog && myGame.minimap.TryGetClickedRoomCenter(out Vector2 roomCenter))
                    {
                        player.Teleport(roomCenter);
                        myGame.Camera.Follow(player.Position);
                    }

                    player.Update(gameTime);
                    myGame.bog.Update(gameTime);
                    myGame.roomManager.Update(gameTime, player.Position);
                    PlayerProjectileHandler.Instance.Update(gameTime);
                    WeaponInventory.Instance.Update(gameTime);
                    ItemInventory.Instance.Update(gameTime, myGame.roomManager.CurrentRoom);
                    myGame.weaponHotbar.Update(gameTime);
                    myGame.itemHotbar.Update(gameTime);
                    myGame.deathHandler.Update(gameTime);
                    myGame.CurTime.Update(gameTime);
                    myGame._statusDisplay.Update();

                    //~Whatever Else Actions~
                    myGame.Enemies.Update(gameTime, player.Position);
                    EnemyProjectileHandler.Instance.Update(gameTime);


                    RoomCollisionHandler.Instance.Update(gameTime);
                    myGame.Camera.Follow(player.Position);
                    //gameGPS.Update(player.Position);
                    myGame.hitboxMode.Update(gameTime);
                }
                else if (myGame.DialogueMode)
                {
                    myGame.CurrentDialogue.Update(gameTime);
                    player.Update(gameTime);
                    //TODO give roommanager a way so that it can continue to animate just the NPC idles while talking
                }
                else
                {
                    //no updates aside from controller inputs
                    myGame.pauseMenu.Update(gameTime);
                }
            }
        }
}
}
