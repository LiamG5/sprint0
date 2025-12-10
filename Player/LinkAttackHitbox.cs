using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;



namespace sprint0.Classes
{
	public class LinkAttackHitbox : IAttack
	{
		public Vector2 position { get; set; } = new Vector2(400, 200);
        public bool active = false;
        private int damage = 1;
        private Link.Direction direction;

        public LinkAttackHitbox()
		{
		}

        public CollisionDirection GetKnockbackDirection()
        {
            switch (this.direction)
            {
                case Link.Direction.Up:
                    return CollisionDirection.Down;
                    break;
                case Link.Direction.Down:
                    return CollisionDirection.Up;
                    break;
                case Link.Direction.Left:
                    return CollisionDirection.Right;
                    break;
                case Link.Direction.Right:
                    return CollisionDirection.Left;
                    break;
                default:
                    return CollisionDirection.None;
                    break;
            }
        }

        public void Attack(Link.Direction direction, Vector2 linkPosition)
        {
            this.direction = direction;
            switch (direction)
            {
                case Link.Direction.Up:
                    this.position = new Vector2((int)linkPosition.X, (int)linkPosition.Y - 48);
                    break;
                case Link.Direction.Down:
                    this.position = new Vector2((int)linkPosition.X, (int)linkPosition.Y + 48);
                    break;
                case Link.Direction.Left:
                    this.position = new Vector2((int)linkPosition.X - 48, (int)linkPosition.Y);
                    break;
                case Link.Direction.Right:
                    this.position = new Vector2((int)linkPosition.X + 48, (int)linkPosition.Y);
                    break;
            }
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }
        public bool BlocksMovement()
        {
            return active;
        }
        public bool BlocksProjectiles()
        {
            return active;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            if (active) {
                switch (other)
                {
                    case IEnemy enemy:
                        enemy.TakeDamage(damage);
                        break;
                }
            }
        }
    }
}