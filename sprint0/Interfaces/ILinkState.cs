using System;
using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{

public interface ILinkState
{
    void Update();
    void MoveLeft();
    void MoveRight();
    void MoveUp();
    void MoveDown();
    void AttackLeft();
    void AttackRight();
    void AttackUp();
    void AttackDown();
    void UseItem1Left();
    void UseItem1Right();
    void UseItem1Up();
    void UseItem1Down();
    void UseItem2Left();
    void UseItem2Right();
    void UseItem2Up();
    void UseItem2Down();
    void UseItem3Left();
    void UseItem3Right();
    void UseItem3Up();
    void UseItem3Down();
    void TakeDamage();
}
}