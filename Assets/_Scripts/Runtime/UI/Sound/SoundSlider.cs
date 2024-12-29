using UnityEngine;
using TopDown.Audio;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TopDown.UI
{
    public class SoundSlider : MonoBehaviour
    {
        [Header("Sound Mixer")]
        [SerializeField] private AudioMixer masterMixer;

        [Header("Variable Name")]
        [SerializeField] private string variableName;

        [Header("Slider")]
        [SerializeField] private Slider soundSlider;

        private void Awake()
        {
            DisplayVolume();
        }

        //Display volume on slider
        private void DisplayVolume()
        {
            float variableValue = PlayerPrefs.GetFloat(variableName, 1);
            soundSlider.value = variableValue;
        }

        //When slider value changed change mixer volume and save variable new value
        public void OnSliderValueChange()
        {
            SoundManager.Instance?.SetMixerValue(soundSlider.value, variableName);
            PlayerPrefs.SetFloat(variableName, soundSlider.value);
        }
    }
}