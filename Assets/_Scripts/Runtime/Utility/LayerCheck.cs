using UnityEngine;

public static class LayerCheck
{
    public static bool IsInLayer(LayerMask layerMask, Collider2D checkCollider)
    {
        return (layerMask.value & (1 << checkCollider.transform.gameObject.layer)) > 0;
    }
}