using UnityEngine;
using UnityEngine.UI;
using TopDown.Loading;

namespace TopDown.Menu
{
    [RequireComponent(typeof(Button))]
    public class MissionSelectButton : MonoBehaviour
    {
        [SerializeField] private GameObject lockedImage;
        [SerializeField] private int levelIndex;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            int lastLevel = PlayerPrefs.GetInt("LastLevel", (int)LoadingManager.Level.Level1);
            bool levelLocked = !(levelIndex <= lastLevel);
            lockedImage.SetActive(levelLocked);
            button.interactable = !levelLocked;
        }
    }
}