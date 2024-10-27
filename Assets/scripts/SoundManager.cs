using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //Assign initial volumes
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        //Get base volume
        float baseVolume = 1;

        float currentVolume = PlayerPrefs.GetFloat("soundVolume", 1); //Load last save sound volume form the player prefs
        currentVolume += _change;


        //check if we reached the maxumum or minimum value
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicVolume(float _change)
    {
        float baseVolume = 0.3f;

        float currentVolume = PlayerPrefs.GetFloat("musicVolume"); //Load last save sound volume form the player prefs
        currentVolume += _change;


        //check if we reached the maxumum or minimum value
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        musicSource.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}

