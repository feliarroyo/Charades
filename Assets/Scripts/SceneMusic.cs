using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public MusicPlayer bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Music").GetComponent<AudioSource>().clip.Equals(bgMusic.clip))
            bgMusic.PlayTrack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
