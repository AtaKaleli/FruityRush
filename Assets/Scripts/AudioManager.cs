using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Start()
    {
        instance = this;
        bgm[0].Play();

    }


    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
            sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToStop)
    {
        sfx[sfxToStop].Stop();
    }

    public void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        bgm[bgmToPlay].Play();
    }
}
