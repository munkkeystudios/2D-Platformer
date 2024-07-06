using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_moveable
{
    bool facing_right { get; set; }

    void MoveEnemy(Vector2 velocity);
    void CheckFaceDirection(Vector2 velocity);

}
