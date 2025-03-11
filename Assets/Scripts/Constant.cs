using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public readonly static string MonsterTag = "Monster";
    public readonly static string PlayerTag = "Player";
    public readonly static string EnemyBulletTag = "EnemyBullet";
    public static Vector3 GetMousePosition(Camera camera)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
