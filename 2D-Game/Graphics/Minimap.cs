using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_Game
{
    public class Minimap
    {
        private const int Cols = 3, Rows = 3;
        private readonly Rectangle bounds;      // screen space
        private readonly int cellWidth, cellHeight;
        private readonly int roomWidth, roomHeight;
        private readonly Texture2D pixel;
        private MouseState previousMouse;

        public Minimap(GraphicsDevice device, Point topLeft, int cellWidth, int roomWidth, int roomHeight)
        {
            this.roomWidth = roomWidth;
            this.roomHeight = roomHeight;
            this.cellWidth = cellWidth;
            cellHeight = cellWidth * roomHeight / roomWidth; 
            bounds = new Rectangle(topLeft.X, topLeft.Y, cellWidth * Cols, cellHeight * Rows);

            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public bool TryGetClickedRoomCenter(out Vector2 roomCenter)
        {
            roomCenter = Vector2.Zero;
            MouseState mouse = Mouse.GetState();
            bool justClicked = mouse.LeftButton == ButtonState.Pressed
                            && previousMouse.LeftButton == ButtonState.Released;
            previousMouse = mouse;

            if (!justClicked || !bounds.Contains(mouse.Position)) return false;

            int col = (mouse.X - bounds.X) / cellWidth;
            int row = (mouse.Y - bounds.Y) / cellHeight;
            col = MathHelper.Clamp(col, 0, Cols - 1);
            row = MathHelper.Clamp(row, 0, Rows - 1);

            roomCenter = new Vector2(col * roomWidth + roomWidth / 2f, row * roomHeight + roomHeight / 2f);
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerWorldPos)
        {
            // Background
            spriteBatch.Draw(pixel, bounds, Color.Black * 0.6f);

            int playerCol = (int)(playerWorldPos.X / roomWidth);
            int playerRow = (int)(playerWorldPos.Y / roomHeight);

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Rectangle cell = new Rectangle(bounds.X + col * cellWidth, bounds.Y + row * cellHeight, cellWidth, cellHeight);
                    Rectangle inner = new Rectangle(cell.X + 2, cell.Y + 2, cell.Width - 4, cell.Height - 4); // 2px gap = grid lines
                    Color fill = (col == playerCol && row == playerRow) ? Color.Gold : Color.Gray;
                    spriteBatch.Draw(pixel, inner, fill * 0.8f);
                }
            }

            // Player dot
            float dotX = bounds.X + playerWorldPos.X / (roomWidth * Cols) * bounds.Width;
            float dotY = bounds.Y + playerWorldPos.Y / (roomHeight * Rows) * bounds.Height;
            spriteBatch.Draw(pixel, new Rectangle((int)dotX - 2, (int)dotY - 2, 4, 4), Color.Red);
        }
    }
}