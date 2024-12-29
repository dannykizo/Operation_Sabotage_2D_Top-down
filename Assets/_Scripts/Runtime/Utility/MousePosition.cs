using UnityEngine;

public static class MousePosition
{
    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}