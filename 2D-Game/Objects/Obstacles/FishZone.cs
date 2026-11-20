using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class FishZone : IObstacle, IHitboxOwner, IInteractable
{
        public ISprite Sprite { get; set; }
        public Vector2 OriginPos {get; set;}
        public Vector2 destinationPos { get; set;}
        public IHitbox Hitbox { get; set; }
        public bool MarkedForRemoval { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        private Texture2D WaterTexture;

        /// <summary>
        /// Creates a block that spans an area determined by <paramref name="bottomRightCorner"/> and <paramref name="topLeftCorner"/>
        /// </summary>
        /// <param name="topLeftCorner"></param>
        /// <param name="bottomRightCorner"></param>
        public FishZone(Vector2 topLeftCorner, Vector2 bottomRightCorner)
        {
            OriginPos = topLeftCorner;
            destinationPos = bottomRightCorner;
            Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint(), destinationPos.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            //Temporary forced texture
            WaterTexture = new Texture2D(RoomCollisionHandler.Instance.myGame.GraphicsDevice, 1, 1);
            WaterTexture.SetData(new[] { Color.Aqua });
         
            RoomCollisionHandler.Instance.Add(this, false);
        }

        /// <summary>
        /// Creates a block that spans an area determined by <paramref name="bottomRightCorner"/> and <paramref name="topLeftCorner"/>
        /// with a <paramref name="texture"/>
        /// </summary>
        /// <param name="topLeftCorner"></param>
        /// <param name="bottomRightCorner"></param>
        /// <param name="texture"></param>
        public FishZone(Vector2 topLeftCorner, Vector2 bottomRightCorner, ISprite texture)
        {
            this.Sprite = texture;
            OriginPos = topLeftCorner;
            destinationPos = bottomRightCorner;
            Hitbox = new BlockHitBox(new Rectangle(OriginPos.ToPoint(), destinationPos.ToPoint()));
            CollisionHandler = new BlankCollisionHandler();
            RoomCollisionHandler.Instance.Add(this, false);
        }
        public void HandleInteraction()
        {
            if(ItemInventory.Instance.GetSelectedItem() is FishingRodItem fishingRod)
            {
                fishingRod.OverWater = true;
                fishingRod.Use();
            }
            else 
            {
                RoomCollisionHandler.Instance.myGame.CurrentDialogue = DialogueFactory.Instance.CreateFishDialogue(0);
                RoomCollisionHandler.Instance.myGame.StateManager.HandleDialogue();
            }
        }
        public void Update(GameTime gt)
        {

        }
        public void Draw(SpriteBatch sprbatch)
        {
            if(Sprite!=null)Sprite.Draw(sprbatch, Hitbox.BoundingBox.Center.ToVector2());
        }

    }
}
