using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _2D_Game
{
    public class Hotbar
    {
        private readonly Point TopLeftStartPoint;      // screen space
        private readonly int cellWidth, cellHeight;
        private List<IItem> ItemsToDraw;
        private readonly Texture2D pixel;

        public Hotbar(GraphicsDevice device, Point topLeft, int cellWidth)
        {
            this.cellWidth = cellWidth;
            cellHeight = cellWidth;
            TopLeftStartPoint = topLeft;
             ItemsToDraw = ItemInventory.Instance.GetKeys();
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
                Rectangle cell = new Rectangle(TopLeftStartPoint.X + row * cellWidth, TopLeftStartPoint.Y, cellWidth, cellHeight);
                Rectangle inner = new Rectangle(cell.X + 2, cell.Y + 2, cell.Width - 4, cell.Height - 4);
                spriteBatch.Draw(pixel, inner, Color.Black * 0.8f);
                if (row < ItemsToDraw.Count)
                {
                    ItemsToDraw[row].Icon.Draw(spriteBatch, new Vector2((TopLeftStartPoint.X + row * cellWidth) + (cellWidth / 2), TopLeftStartPoint.Y + (cellHeight / 2)));
                }
                
            }

           
        }

    }
}