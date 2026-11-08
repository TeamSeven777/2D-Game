using Microsoft.Xna.Framework;

namespace _2D_Game
{
    public class Camera
    {
        private readonly int viewWidth, viewHeight;
        private Rectangle bounds; // the area the camera is allowed to show

        // Top-left corner of the camera in world coordinates
        public Vector2 Position { get; private set; }

        // Pass this to SpriteBatch.Begin(transformMatrix: camera.Transform)
        public Matrix Transform { get; private set; } = Matrix.Identity;

        public Camera(int viewWidth, int viewHeight, int worldWidth, int worldHeight)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            bounds = new Rectangle(0, 0,
                worldWidth, worldHeight);
        }

        // A one-screen area leaves the camera nowhere to scroll, so it stays still
        public void SetBounds(Rectangle newBounds)
        {
            bounds = newBounds;
        }

        // Call once per frame (in Update) with the point to center on, e.g. the player's center
        public void Follow(Vector2 target)
        {
            float x = target.X - viewWidth / 2f;
            float y = target.Y - viewHeight / 2f;

            // Don't scroll past the world edges
            x = MathHelper.Clamp(x, bounds.Left, bounds.Right - viewWidth);
            y = MathHelper.Clamp(y, bounds.Top, bounds.Bottom - viewHeight);

            Position = new Vector2(x, y);
            Transform = Matrix.CreateTranslation(-x, -y, 0);
        }

        // Use for mouse input: screen pixels -> world coordinates
        public Vector2 ScreenToWorld(Vector2 screenPos)
        {
            return screenPos + Position;
        }

        // World coordinates -> screen pixels (for HUD markers, etc.)
        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            return worldPos - Position;
        }
    }
}