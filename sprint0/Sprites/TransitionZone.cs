using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using sprint0.Managers;
using System;

namespace sprint0.Sprites
{
    public class TransitionZone : ICollidable
    {
        private Rectangle bounds;
        private TransitionDirection direction;
        private int targetRoomId;
        private bool isLocked;
        private RoomManager roomManager;
        private string contentRoot;
        
        public TransitionZone(Rectangle bounds, TransitionDirection direction, int targetRoomId, RoomManager manager, string content)
        {
            this.bounds = bounds;
            this.direction = direction;
            this.targetRoomId = targetRoomId;
            this.isLocked = false;
            this.roomManager = manager;
            this.contentRoot = content;
        }
        
        public Rectangle GetBounds()
        {
            return bounds;
        }
        
        public bool IsSolid()
        {
            return false;
        }
        
        public Vector2 GetPosition()
        {
            return new Vector2(bounds.X, bounds.Y);
        }
        
        public void OnCollision(ICollidable other, CollisionDirection collisionDirection)
        {
            if (other is Link && !isLocked)
            {
                roomManager.TransitionToRoom(targetRoomId, direction, contentRoot);
            }
        }
        
        public void SetLocked(bool locked)
        {
            isLocked = locked;
        }
    }
}