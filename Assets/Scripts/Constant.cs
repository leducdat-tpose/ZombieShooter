using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public readonly static string MonsterTag = "Monster";
    public readonly static string PlayerTag = "Player";
    public readonly static string StaticObject = "StaticObject";
    public readonly static string EnemyBulletTag = "EnemyBullet";
    public readonly static string ObstacleLayer = "Obstacle";
    public readonly static int ObstacleLayerMaskValue = LayerMask.GetMask(ObstacleLayer);

}
