using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _2D_Game
{
    public class Hotbar
    {
        private readonly Point ItemTopLeftStartPoint;
        private readonly Point WeaponTopLeftStartPoint; // screen space
        private readonly int cellWidth, cellHeight;
        private List<IItem> ItemsToDraw;
        private List<Tuple<ISprite,Tool>> WeaponsToDraw;
        private readonly Texture2D pixel;

        public Hotbar(GraphicsDevice device, Point ItemTopLeft, Point WeaponTopLeft, int cellWidth)
        {
            this.cellWidth = cellWidth;
            cellHeight = cellWidth;
            ItemTopLeftStartPoint = ItemTopLeft;
            WeaponTopLeftStartPoint = WeaponTopLeft;
            ItemsToDraw = ItemInventory.Instance.GetKeys();
            WeaponsToDraw = new List<Tuple<ISprite, Tool>>();
            for(int i =0; i < 2; i++)
            {
                WeaponsToDraw.Add(new Tuple<ISprite, Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Slingshot), Tool.Slingshot));
                WeaponsToDraw.Add(new Tuple<ISprite,Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Boomerang),Tool.Boomerang));
                WeaponsToDraw.Add(new Tuple<ISprite, Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Dynamite), Tool.Dynamite));
            }
            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });
        }


        public void AddItem(IItem item)
        {
                ItemsToDraw.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            if (ItemsToDraw.Contains(item))
            {
                ItemsToDraw.Remove(item);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 playerWorldPos)
        {
            for (int row = 0; row < 7; row++)
            {
                //Rectangle to Draw
                Rectangle inner = new Rectangle(ItemTopLeftStartPoint.X + row * cellWidth + 2, ItemTopLeftStartPoint.Y, cellWidth - 4, cellHeight - 4);
                spriteBatch.Draw(pixel, inner, Color.Black * 0.8f);
                //Draws Items inside the rectangles
                if(row < WeaponsToDraw.Count && row < 3)
                {
                    Rectangle left = new Rectangle(WeaponTopLeftStartPoint.X + row * cellWidth + 2, WeaponTopLeftStartPoint.Y, cellWidth - 4, cellHeight - 4);
                    if (WeaponsToDraw[row].Item2 == WeaponInventory.Instance.GetTop())
                    {
                        spriteBatch.Draw(pixel, left, Color.White * 0.8f);
                    }
                    else
                    {
                        spriteBatch.Draw(pixel, left, Color.Black * 0.8f);
                    }
                    WeaponsToDraw[row].Item1.Draw(spriteBatch, new Vector2((WeaponTopLeftStartPoint.X + row * cellWidth) + (cellWidth / 2), WeaponTopLeftStartPoint.Y + (cellHeight / 2)));
                }
                if (row < ItemsToDraw.Count)
                {

                    //Draws Items
                    ItemsToDraw[row].Icon.Draw(spriteBatch, new Vector2((ItemTopLeftStartPoint.X + row * cellWidth) + (cellWidth / 2), ItemTopLeftStartPoint.Y + (cellHeight / 2)));
                }
                
            }

           
        }

    }
}