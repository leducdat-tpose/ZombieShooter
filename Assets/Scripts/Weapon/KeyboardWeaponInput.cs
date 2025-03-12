using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyboardWeaponInput
{
    public static bool IsFiring() => Input.GetMouseButton(0);
    public static bool IsFiringReleased() => Input.GetMouseButtonUp(0);
    public static bool IsReloading() => Input.GetKeyDown(KeyCode.R);
    public static Vector3 GetMousePosition(Camera camera)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}