using UnityEngine;

namespace TopDown.Utility
{
    public class PersistentObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}