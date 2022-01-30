using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource backgroundMusicASource;
    public AudioSource sfxMusicASource;

    public static SoundController Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<SoundController>();

                if (instance == null)

                {
                    GameObject go = new GameObject();
                    go.name = "Game Manager";
                    instance = go.AddComponent<SoundController>();
                }
            }
            return instance;
        }
    }

    //Public Methods
    public void PlayBackgroundMusic(string nameMusic)
    {
        AudioClip currentBackground = Resources.Load<AudioClip>(nameMusic);
        if (currentBackground != null)
        {
            backgroundMusicASource.clip = currentBackground;
            backgroundMusicASource.Play();
        }
    }

    public  void PlaySfxMusic(string nameSfx)
    {
        AudioClip currentSfxMusic = Resources.Load<AudioClip>(nameSfx);
        if (currentSfxMusic != null)
            sfxMusicASource.PlayOneShot(currentSfxMusic);
    }

    private static SoundController instance = null;
}
