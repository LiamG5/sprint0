using System.Collections.Generic;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace sprint0.Collisions
{
    public class CollisionHandler
    {
        private readonly CollisionDetection _collisionDetection;
       

        public CollisionHandler()
        {
            _collisionDetection = new CollisionDetection();
        }

        public List<CollisionDirection> CollisionDetect(List<Rectangle> rectangles)
        {
            var collisionDirections = new List<CollisionDirection>();
            
            if (rectangles == null || rectangles.Count < 2)
                return collisionDirections;

            Rectangle linkRec = rectangles[0];

            for (int i = 1; i < rectangles.Count; i++)
            {
                CollisionDirection direction = _collisionDetection.GetCollision(linkRec, rectangles[i]);
                collisionDirections.Add(direction);
            }

            return collisionDirections;
        }
    }
}