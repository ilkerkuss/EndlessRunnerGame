using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public Sound[] _sounds;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }else
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
         Init();
    }


    void Update()
    {
        
    }


    public void Init()
    {
        
        foreach (Sound s in _sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.loop = s.SoundLoop;
            s.AudioSource.pitch = s.Pitch;
           
            
        }

            PlaySound("BackgroundSound");
        
        
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in _sounds)
        {
            if (s.SoundName == name)
                s.AudioSource.Play();
        }
    }

    
}
