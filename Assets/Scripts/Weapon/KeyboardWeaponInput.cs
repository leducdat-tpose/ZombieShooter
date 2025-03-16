using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class KeyboardWeaponInput
{
    public static bool IsFiring()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButton(0);
    }
    public static bool IsFiringReleased()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButtonUp(0);
    }
    public static bool IsReloading() => Input.GetKeyDown(KeyCode.R);
    public static Vector3 GetMousePosition(Camera camera)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
    public static bool IsPointerOverUI()
    {
        if(EventSystem.current != null) return EventSystem.current.IsPointerOverGameObject();
        return false;
    }
}