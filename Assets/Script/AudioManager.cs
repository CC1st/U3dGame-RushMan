using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource player;

    void Start()
    {
        Instance = this;
        player = GetComponent<AudioSource>();
    }

    //播放音频
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        player.PlayOneShot(clip);
    }

}
