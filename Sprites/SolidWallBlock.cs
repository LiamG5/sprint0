using Microsoft.Xna.Framework;
using sprint0.Interfaces;
using sprint0.Collisions;

namespace sprint0.Sprites
{
    public class SolidWallBlock : ICollidable
    {
        private Rectangle bounds;
        private Vector2 position;

        public SolidWallBlock(Vector2 position, int width, int height)
        {
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }

        public bool IsSolid()
        {
            return true;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
        }
    }
}