using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using sprint0.Collisions;
using System;

namespace sprint0.Sprites.Enemies
{
    public class EnemyMovementCycle
    {
        private Vector2 position;
        private static Random rand = new Random();
        private Vector2 velocity;
        private int direction;
        private const float speed = 1.0f;

        // How often to re-randomize direction when NOT chasing
        private int changeDirectionTimer = 0;
        private const int changeDirectionInterval = 80;

        // --- NEW: target-based “chase” AI support ---
        private readonly Func<Vector2>? targetProvider;
        private const float minChaseDistanceSquared = 1.0f; // don’t jitter when very close

        // Old constructor still works (pure random movement)
        public EnemyMovementCycle(Vector2 startPos)
        {
            this.position = startPos;
            direction = rand.Next(4);
            ChangeDirection(); // ensure velocity is initialized
        }

        // NEW: constructor with target (for chasing Link)
        public EnemyMovementCycle(Vector2 startPos, Func<Vector2> targetProvider)
        {
            this.position = startPos;
            this.targetProvider = targetProvider;
            direction = rand.Next(4);
            ChangeDirection(); // default velocity in case targetProvider is null or not used
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 wallMasterSpawn()
        {
            position = new Vector2(position.X + 1, position.Y);
            return position;
        }

        public Vector2 Move()
        {
            if (sprint0.Classes.Inventory.AreEnemiesFrozen())
            {
                return position;
            }

            if (targetProvider != null)
            {
                // --- Chase AI: move toward target in cardinal directions ---
                Vector2 targetPos = targetProvider();
                Vector2 toTarget = targetPos - position;

                if (toTarget.LengthSquared() > minChaseDistanceSquared)
                {
                    // Choose dominant axis so we stay NESW (no diagonal sliding)
                    if (Math.Abs(toTarget.X) > Math.Abs(toTarget.Y))
                    {
                        velocity = new Vector2(Math.Sign(toTarget.X) * speed, 0f);
                    }
                    else
                    {
                        velocity = new Vector2(0f, Math.Sign(toTarget.Y) * speed);
                    }
                }

                position += velocity;
            }
            else
            {
                // Original random wandering behaviour
                position += velocity;
                changeDirectionTimer++;
                if (changeDirectionTimer >= changeDirectionInterval)
                {
                    changeDirectionTimer = 0;
                    ChangeDirection();
                }
            }

            return position;
        }

        public void ChangeDirection()
        {
            direction = rand.Next(4);
            switch (direction)
            {
                case 0:
                    velocity = new Vector2(0f, -speed);
                    break;
                case 1:
                    velocity = new Vector2(0f, speed);
                    break;
                case 2:
                    velocity = new Vector2(-speed, 0f);
                    break;
                case 3:
                    velocity = new Vector2(speed, 0f);
                    break;
            }
        }

        // Collision-response direction changes still work the same
        public void ChangeDirectionCol()
        {
            switch (direction)
            {
                case 0:
                    position = new Vector2(position.X, position.Y + speed);
                    velocity = new Vector2(-speed, 0f);
                    direction = 2;
                    break;
                case 1:
                    position = new Vector2(position.X, position.Y - speed);
                    velocity = new Vector2(speed, 0f);
                    direction = 3;
                    break;
                case 2:
                    position = new Vector2(position.X + speed, position.Y);
                    velocity = new Vector2(0f, speed);
                    direction = 1;
                    break;
                case 3:
                    position = new Vector2(position.X - speed, position.Y);
                    velocity = new Vector2(0f, -speed);
                    direction = 0;
                    break;
            }
        }
    }
}


