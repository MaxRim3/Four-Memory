using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource;
    public AudioSource audioFXSource;

    public AudioClip backgroundMusicOne;
    public AudioClip backgroundMusicTwo;

    public GameObject audioPosition;

    public AudioClip[] hit;
    public AudioClip moleAppear;
    public AudioClip moleDisappear;

    public AudioClip memoriseBanner;
    public AudioClip rememberBanner;

    public AudioClip[] correctSound;
    public AudioClip wrongSound;

    public AudioClip counterSound;

    public AudioClip buttonSound;

    // Start is called before the first frame update
   
    public void playHit()
    {
        //if (hit)
        {
            //audioFXSource.volume = 0.4f;
            //audioFXSource.PlayOneShot(hit[Random.Range(0,3)]);
            AudioSource.PlayClipAtPoint(hit[Random.Range(0, 3)], audioPosition.transform.position, 0.5f);
        }
    }

    public void playMoleAppear()
    {

        if (moleAppear)
        {
            if (audioFXSource.clip != moleAppear)
            {
                //if (!audioFXSource.isPlaying)
                {
                    audioFXSource.clip = moleAppear;
                    audioFXSource.Play();
                }
            }
            else
            {

                audioFXSource.clip = moleAppear;
                audioFXSource.Play();
            }

        }
    }

    public void playMoleDisappear()
    {
        if (moleDisappear)
        {
            //audioFXSource.PlayOneShot(moleDisappear);
            //audioFXSource.clip = moleDisappear;
            AudioSource.PlayClipAtPoint(moleDisappear, audioPosition.transform.position, 0.2f);
        }
    }

    public void playMemoriseBanner()
    {
        if (memoriseBanner)
        {
            audioFXSource.PlayOneShot(memoriseBanner);
        }
    }

    public void playRememberBanner()
    {
        if (rememberBanner)
        {
            audioFXSource.PlayOneShot(rememberBanner);
        }
    }

    public void playCorrectSound()
    {
        //if (correctSound)
        {
            audioFXSource.PlayOneShot(correctSound[Random.Range(0,3)]);
        }
    }

    public void playWrongSound()
    {
        if (wrongSound)
        {
            audioFXSource.PlayOneShot(wrongSound);
        }
    }

    public void playButtonSound()
    {
        if (buttonSound)
        {
            audioFXSource.PlayOneShot(buttonSound);
        }
    }

    public void playCounterSound()
    {
        if (counterSound != null)
        {
            audioFXSource.PlayOneShot(counterSound);
        }
    }

    public void switchBackgroundMusic()
    {
        if (backgroundMusicSource.clip == backgroundMusicOne)
        {
            backgroundMusicSource.clip = backgroundMusicTwo;
        }
        else
        {
            backgroundMusicSource.clip = backgroundMusicOne;
        }
    }

    public void playBackgroundMusicOne()
    {
        if (backgroundMusicSource.clip != backgroundMusicOne)
        {
            backgroundMusicSource.clip = backgroundMusicOne;
            backgroundMusicSource.Play();
        }
    }

    public void playBackgroundMusicTwo()
    {
        if (backgroundMusicSource.clip != backgroundMusicTwo)
        {
            backgroundMusicSource.clip = backgroundMusicTwo;
            backgroundMusicSource.Play();
        }
    }
}
