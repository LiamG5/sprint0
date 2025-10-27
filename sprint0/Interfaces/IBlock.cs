using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Collisions;

namespace sprint0.Interfaces
{
public interface IBlock : ICollidable
{   
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Vector2 position);
}
}