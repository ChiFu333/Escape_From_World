using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager inst { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private AudioSource windSource;
    [SerializeField] private AudioClip musicClip;

    public float musicVolume = 1;
    public float soundVolume = 1;
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(gameObject);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic(musicClip);
        }
    }
    public void SetMusicVolume(float value) {
        musicVolume = value;
        musicSource.volume = musicVolume;
    }

    public void SetSoundVolume(float value) {
        soundVolume = value;
        soundsSource.volume = soundVolume;
        windSource.volume = windSounds[SceneManager.GetActiveScene().name] * soundVolume;
    }

    public void Play(AudioClip clip) {
        if (clip == null) return;
        soundsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip) {
        if (musicSource.clip == clip || clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void StopMusic(bool toStop) {
        if(toStop)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }
    public static float loud = 0.5f;
    public static float silent = 0.15f;
    private Dictionary<string, float> windSounds = new Dictionary<string, float>()
    {
        { "MainMenu", loud },
        { "EndMenu", loud},
        { "Griz", 0},
        { "Ending1", silent},
        { "Ending2", 1},

        { "Home", silent},
        { "Shop", silent},
        { "Library", silent},
        { "Factory", silent},
        { "PostOffice", silent},
        { "Admin", silent},
        { "Church", silent},

        { "OutsideHome", loud},
        { "OutsideShop", loud},
        { "OutsidePost", loud},
        { "OutsideAdmin", loud},
        { "OutsideChurch", loud},
        { "Escape", loud}        
    };
    private string[] snowLocation = new string[] 
    { 
        "OutsideHome",
        "OutsideShop",
        "OutsidePost",
        "OutsideAdmin",
        "OutsideChurch",
    };
    public void UpdateSounds()
    {
        windSource.volume = windSounds[SceneManager.GetActiveScene().name] * soundVolume;
        Player p = FindFirstObjectByType<Player>();
        if(p != null) p.insideHouse = !snowLocation.Contains(SceneManager.GetActiveScene().name);
    }
}
