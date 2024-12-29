using UnityEditor;
using UnityEngine;

namespace TopDown.Utility
{
    public class DebugUtility : MonoBehaviour
    {
        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                EditorApplication.isPaused = !EditorApplication.isPaused;
        }
        #endif
    }
}