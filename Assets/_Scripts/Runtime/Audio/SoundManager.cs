using UnityEngine;
using UnityEngine.Audio;

namespace TopDown.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("Sound Mixer")]
        [SerializeField] private AudioMixer masterMixer;

        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            SetSavedVolume();
        }

        private void SetSavedVolume()
        {
            SetMixerValue(PlayerPrefs.GetFloat("masterVolume", 1), "masterVolume");
            SetMixerValue(PlayerPrefs.GetFloat("musicVolume", 1), "musicVolume");
            SetMixerValue(PlayerPrefs.GetFloat("soundVolume", 1), "soundVolume");
        }
        public void SetMixerValue(float value, string variableName)
        {
            float mixerValue = Mathf.Log10(value) * 20;
            masterMixer.SetFloat(variableName, mixerValue);
        }

        public void PlayAudio(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}