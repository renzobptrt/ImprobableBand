using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        AudioClip currentBackground = Resources.Load<AudioClip>("Audio/Themes/"+nameMusic);
        if (currentBackground != null)
        {
            if(backgroundMusicASource.clip == null)
            {
                backgroundMusicASource.clip = currentBackground;
                backgroundMusicASource.Play();
            }
            else
            {
                backgroundMusicASource.DOFade(0, 0.2f).OnComplete(() =>
                {
                    backgroundMusicASource.clip = currentBackground;
                    backgroundMusicASource.Play();
                    backgroundMusicASource.DOFade(1, 0.2f);
                });
            }
        }
    }

    public  void PlaySfxMusic(string nameSfx)
    {
        AudioClip currentSfxMusic = Resources.Load<AudioClip>("Audio/" + nameSfx);
        if (currentSfxMusic != null)
            sfxMusicASource.PlayOneShot(currentSfxMusic);
    }

    private static SoundController instance = null;
}
