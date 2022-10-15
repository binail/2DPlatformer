using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inctance { get; private set; }
    private AudioSource source;

    private void Awake()
    {

        //Keep this object even when we go to new scene
        if (inctance == null)
        {
            inctance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (inctance != null && inctance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
