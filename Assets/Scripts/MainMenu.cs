using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MusicPlayer menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Music").GetComponent<AudioSource>().clip.Equals(menuMusic.clip))
            menuMusic.PlayTrack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
