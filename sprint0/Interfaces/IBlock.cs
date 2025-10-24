using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.Interfaces
{
public interface IBlock
{   
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Vector2 position);
    Rectangle GetBounds();
    bool IsSolid();
    Vector2 GetPosition();
}
}