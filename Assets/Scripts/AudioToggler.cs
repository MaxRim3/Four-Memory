using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Mute"))
        {
            toggleAudio();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleAudio()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            this.gameObject.GetComponent<AudioSource>().volume = 1;
        }
        else if (PlayerPrefs.GetInt("Mute") == 0)
        {
            this.gameObject.GetComponent<AudioSource>().volume = 0;
        }
    }
}
