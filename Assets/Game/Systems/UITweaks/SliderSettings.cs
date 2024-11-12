using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour
{
    private Slider slider;
    public bool isSound = true;
    public void Start()
    {
        slider = GetComponent<Slider>();
        if(isSound) slider.value = AudioManager.inst.soundVolume;
        else slider.value = AudioManager.inst.musicVolume;
    }
    public void UpdateMusicVolume()
    {
        AudioManager.inst.SetMusicVolume(slider.value);
    }
    public void UpdateSoundVolume()
    {
        AudioManager.inst.SetSoundVolume(slider.value);
    }
}
