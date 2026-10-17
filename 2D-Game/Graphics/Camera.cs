using Microsoft.Xna.Framework;

namespace _2D_Game
{
    public class Camera
    {
        private readonly int viewWidth, viewHeight;
        private readonly int worldWidth, worldHeight;

        // Top-left corner of the camera in world coordinates
        public Vector2 Position { get; private set; }

        // Pass this to SpriteBatch.Begin(transformMatrix: camera.Transform)
        public Matrix Transform { get; private set; } = Matrix.Identity;

        public Camera(int viewWidth, int viewHeight, int worldWidth, int worldHeight)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
        }

        // Call once per frame (in Update) with the point to center on, e.g. the player's center
        public void Follow(Vector2 target)
        {
            float x = target.X - viewWidth / 2f;
            float y = target.Y - viewHeight / 2f;

            // Don't scroll past the world edges
            x = MathHelper.Clamp(x, 0, worldWidth - viewWidth);
            y = MathHelper.Clamp(y, 0, worldHeight - viewHeight);

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