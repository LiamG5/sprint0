using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace sprint0.Collisions
{
    public class CollisionDetection
    {
       
        //Making a colistion object could make this cleaner, puting pos and rec in the same object
        public CollisionDirection GetCollision(Rectangle rec1, Rectangle rec2)
        {
            
            if (!rec1.Intersects(rec2))
                return CollisionDirection.None;

            
            int leftOverlap = rec2.Right - rec1.Left;
            int rightOverlap = rec1.Right - rec2.Left;
            int topOverlap = rec2.Bottom - rec1.Top;
            int bottomOverlap = rec1.Bottom - rec2.Top;

            int minOverlap = leftOverlap;
            CollisionDirection direction = CollisionDirection.Left;

            if (rightOverlap < minOverlap)
            {
                minOverlap = rightOverlap;
                direction = CollisionDirection.Right;
            }

            if (topOverlap < minOverlap)
            {
                minOverlap = topOverlap;
                direction = CollisionDirection.Up;
            }

            if (bottomOverlap < minOverlap)
            {
                minOverlap = bottomOverlap;
                direction = CollisionDirection.Down;
            }

            return direction;
        }
    }
}