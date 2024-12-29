using UnityEngine;
using TopDown.Audio;
using UnityEngine.UI;

namespace TopDown.UI
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlaySound);
        }
        private void PlaySound()
        {
            SoundManager.Instance?.PlayAudio(audioClip);
        }
    }
}
