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
        private const float SPEED = 1.0f;
        private int changeDirectionTimer = 0;
        private const int changeDirectionInterval = 80;

        public EnemyMovementCycle()
        {
            this.position = new Vector2(400, 100);
        }

        public Vector2 GetPosition()
        {
            return position;
        }
        public Vector2 Move()
        {
            position += velocity;
            changeDirectionTimer++;
            if (changeDirectionTimer >= changeDirectionInterval)
            {
                changeDirectionTimer = 0;
                ChangeDirection();
            }
            return position;
        }
        public void ChangeDirection()
        {
            int direction = rand.Next(4);
            switch (direction)
            {
                case 0:
                    velocity = new Vector2(0f, -SPEED);
                    break;
                case 1:
                    velocity = new Vector2(0f, SPEED);
                    break;
                case 2:
                    velocity = new Vector2(-SPEED, 0f);
                    break;
                case 3:
                default:
                    velocity = new Vector2(SPEED, 0f);
                    break;
            }
        }

    }

}
