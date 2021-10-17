using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public GameObject[] audioSource;
    public Sprite muted;
    public Sprite unmuted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleMute()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            PlayerPrefs.SetInt("Mute", 0);
            this.gameObject.GetComponent<Image>().sprite = muted;
        }
        else if (PlayerPrefs.GetInt("Mute") == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            this.gameObject.GetComponent<Image>().sprite = unmuted;
        }

        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].GetComponent<AudioToggler>().toggleAudio();
        }
    }
}
