using System.Collections;
using UnityEngine;

public interface ISpriteUpdate
{
    void UpdateSprite(Vector3 destination);
    public void BlinkAnimation();
}