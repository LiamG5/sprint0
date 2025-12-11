using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using sprint0.Managers;

namespace sprint0.Sprites
{
    public class TransitionZone : ICollidable
    {
        private readonly Rectangle bounds;
        private readonly TransitionDirection direction;
        private readonly bool isBlocking;   
        private bool isLocked;
        private readonly RoomManager roomManager;
        private bool hasTriggered; // Prevent multiple triggers

        // Normal constructor (for regular door transitions)
        public TransitionZone(Rectangle bounds, TransitionDirection direction, RoomManager manager)
        {
            this.bounds = bounds;
            this.direction = direction;
            this.roomManager = manager;
            this.isLocked = false;
            this.isBlocking = false;
            this.hasTriggered = false;
        }

        public TransitionZone(Rectangle bounds, TransitionDirection direction, RoomManager manager, bool isBlocking)
        {
            this.bounds = bounds;
            this.direction = direction;
            this.roomManager = manager;
            this.isLocked = false;
            this.isBlocking = isBlocking;
            this.hasTriggered = false;
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }

        public bool IsSolid()
        {
            return isBlocking;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(bounds.X, bounds.Y);
        }

        public void OnCollision(ICollidable other, CollisionDirection collisionDirection)
        {
            // Skip if not Link, currently locked, or transition already triggered
            if (!(other is Link) || isLocked || hasTriggered)
                return;

            // Skip if a transition is already in progress
            if (roomManager.TransitionManager != null && roomManager.TransitionManager.IsTransitioning)
                return;

            // If this is a blocking zone, do not allow passage or transitions
            if (isBlocking)
            {
                System.Console.WriteLine($"[TransitionZone] Blocked passage at {direction} in Room {roomManager.CurrentRoomId}");
                return;
            }

            int currentRoom = roomManager.CurrentRoomId;
            
            if (roomManager.HasConnection(currentRoom, direction))
            {
                System.Console.WriteLine($"[TransitionZone] Triggering transition from Room {currentRoom} via {direction}");
                hasTriggered = true;
                roomManager.TransitionToRoom(direction);
            }
            else
            {
                System.Console.WriteLine($"[TransitionZone] No connection from Room {currentRoom} in direction {direction}");
            }
        }

        public void SetLocked(bool locked)
        {
            isLocked = locked;
        }
        
        public void ResetTrigger()
        {
            hasTriggered = false;
        }
        
        public bool BlocksProjectiles()
        {
            return true;
        }
        
        public bool BlocksMovement()
        {
            return isBlocking;
        }

        public TransitionDirection GetDirection()
        {
            return direction;
        }
    }
}
