using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public static class KeyboardWeaponInput
{
    public static bool PrimaryIsFiring()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButton(0);
    }
    public static bool PrimaryIsFiringReleased()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButtonUp(0);
    }
    public static bool SecondIsFiring()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButton(1);
    }
    public static bool SecondIsFiringReleased()
    {
        if(IsPointerOverUI()) return false;
        return Input.GetMouseButtonUp(1);
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
        if (EventSystem.current == null) return false;
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Any(result => result.gameObject.layer == LayerMask.NameToLayer("UI"));
    }
}