using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;



namespace sprint0.Classes
{
	public class LinkAttackHitbox : ICollidable
	{
		public Vector2 position { get; set; } = new Vector2(400, 200);
        public bool active = false;
        public LinkAttackHitbox()
		{
		
		}
        public void Update(Vector2 position)
		{
            this.position = position;
		}
        public void AttackUp()
        {
            
            this.position = new Vector2((int)position.X, (int)position.Y - 48);
        }
        public void AttackDown()
        {
            
            this.position = new Vector2((int)position.X, (int)position.Y + 48);
        }
        public void AttackRight()
        {
            
            this.position = new Vector2((int)position.X, (int)position.Y + 48);
        }
        public void AttackLeft()
        {
            
            this.position = new Vector2((int)position.X, (int)position.Y - 48);
        }
        
         public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }
        public bool IsSolid()
        {
            return active = true;;
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