using System;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioClip clip;

    public void PlayClip()
    {
        try
        {
            if (!SceneLoader.IsLoading())
                SFXManager.instance.PlayClip(clip);
        }
        catch (NullReferenceException){
            
        }

    }
}