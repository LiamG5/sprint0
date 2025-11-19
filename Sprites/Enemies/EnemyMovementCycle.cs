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
        private int changeDirectionTimer = 0;
        private const int changeDirectionInterval = 80;

        public EnemyMovementCycle(Vector2 startPos)
        {
            this.position = startPos;
            direction = rand.Next(4);
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
