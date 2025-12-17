using Microsoft.Xna.Framework;
using sprint0.Collisions;
using System;

namespace sprint0.Sprites.Enemies
{
    public class EnemyStateMachine
    {
        public enum State { Normal, Knockback, Invulnerable }
        
        private State currentState = State.Normal;
        private float knockbackTimer = 0f;
        private float invulnerabilityTimer = 0f;
        private Vector2 knockbackVelocity = Vector2.Zero;
        
        public float KnockbackDuration { get; set; } = 250f;
        public float InvulnerabilityDuration { get; set; } = 500f;
        public float KnockbackSpeed { get; set; } = 5f;
        
        public State GetCurrentState()
        {
            return currentState;
        }

        public bool IsInvulnerable()
        {
            return currentState == State.Invulnerable || currentState == State.Knockback;
        }

        public float GetInvulnerabilityTimer()
        {
            return invulnerabilityTimer;
        }
        
        public Vector2 UpdateKnockback(float elapsedMs)
        {
            Vector2 movementDelta = Vector2.Zero;
            
            switch (currentState)
            {
                case State.Knockback:
                    if (knockbackVelocity.LengthSquared() > 0)
                    {
                        float knockbackDistance = KnockbackSpeed * (elapsedMs / 16.67f);
                        Vector2 knockbackDirection = Vector2.Normalize(knockbackVelocity);
                        movementDelta = knockbackDirection * knockbackDistance;
                    }
                    
                    knockbackTimer += elapsedMs;
                    if (knockbackTimer >= KnockbackDuration)
                    {
                        knockbackTimer = 0f;
                        knockbackVelocity = Vector2.Zero;
                        currentState = State.Invulnerable;
                        invulnerabilityTimer = 0f;
                    }
                    break;
                    
                case State.Invulnerable:
                    invulnerabilityTimer += elapsedMs;
                    if (invulnerabilityTimer >= InvulnerabilityDuration)
                    {
                        invulnerabilityTimer = 0f;
                        currentState = State.Normal;
                    }
                    break;
                    
                case State.Normal:
                default:
                    break;
            }
            
            return movementDelta;
        }
        
        public void StartKnockback(CollisionDirection direction)
        {
            if (currentState == State.Normal)
            {
                switch (direction)
                {
                    case CollisionDirection.Left:
                        knockbackVelocity = new Vector2(KnockbackSpeed, 0f);
                        break;
                    case CollisionDirection.Right:
                        knockbackVelocity = new Vector2(-KnockbackSpeed, 0f);
                        break;
                    case CollisionDirection.Up:
                        knockbackVelocity = new Vector2(0f, KnockbackSpeed);
                        break;
                    case CollisionDirection.Down:
                        knockbackVelocity = new Vector2(0f, -KnockbackSpeed);
                        break;
                    default:
                        knockbackVelocity = new Vector2(KnockbackSpeed, 0f);
                        break;
                }
                
                currentState = State.Knockback;
                knockbackTimer = 0f;
            }
        }
        
        public void SetInvulnerableState()
        {
            if (currentState == State.Normal)
            {
                currentState = State.Invulnerable;
                invulnerabilityTimer = 0f;
            }
        }
        
        public void CancelKnockback()
        {
            knockbackVelocity = Vector2.Zero;
        }
        
        public void Reset()
        {
            currentState = State.Normal;
            knockbackTimer = 0f;
            invulnerabilityTimer = 0f;
            knockbackVelocity = Vector2.Zero;
        }

        public Color GetDrawColor()
        {
            if (IsInvulnerable())
            {
                int flashFrame = (int)(invulnerabilityTimer / 50f) % 2;
                return flashFrame == 0 ? Color.Red : Color.White;
            }
            return Color.White;
        }
    }
}

