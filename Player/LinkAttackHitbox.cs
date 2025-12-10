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

        public LinkAttackHitbox()
		{
		
		}
        public void AttackUp(Vector2 position)
        {
            this.position = new Vector2((int)position.X, (int)position.Y - 48);
        }        
        public void AttackDown(Vector2 position)
        {
            this.position = new Vector2((int)position.X, (int)position.Y + 48);
        }
        public void AttackRight(Vector2 position)
        {
            this.position = new Vector2((int)position.X + 48, (int)position.Y);
        }
        public void AttackLeft(Vector2 position)
        {         
            this.position = new Vector2((int)position.X - 48, (int)position.Y);
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