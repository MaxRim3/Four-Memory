using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour
{
    public GameObject blackPanel;
    public AudioSource audioSource;
    public AudioClip buttonSound;


    public void Start()
    {
        StartCoroutine(FaceBlackOutSquare(false, 2));
    }

    public void startLoadMainMenu()
    {
        StartCoroutine(loadMainMenuC());
    }

    public IEnumerator loadMainMenuC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }



    public IEnumerator FaceBlackOutSquare(bool fadeToBlack, int fadeSpeed)
    {
        blackPanel.SetActive(true);
        Color objectColor = blackPanel.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackPanel.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackPanel.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else if (!fadeToBlack)
        {
            while (blackPanel.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackPanel.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            blackPanel.SetActive(false);
        }


    }

    public void playButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
