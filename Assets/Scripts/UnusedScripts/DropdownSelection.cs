using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSelection : MonoBehaviour
{

    public SoundEffectPlayer soundEffectPlayer;
    // Start is called before the first frame update

    public void ConditionalPlayClip() {
        if (!CategoryCreator.isInitializing)
            soundEffectPlayer.PlayClip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
