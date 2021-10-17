using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject blackPanel;
    public AudioSource audioSource;
    public AudioClip buttonSound;
    public Text easyHS;
    public Text mediumHS;
    public Text hardHS;
    public Text advancedHS;

    void Start()
    {
        StartCoroutine(FaceBlackOutSquare(false, 2));
        PlayerPrefs.SetInt("Easy", 0);
        PlayerPrefs.SetInt("Medium", 0);
        PlayerPrefs.SetInt("Hard", 0);
        PlayerPrefs.SetInt("Advanced", 0);
        RateGame.Instance.ShowRatePopup();
        easyHS.text = PlayerPrefs.GetInt("easyHS", 0).ToString();
        mediumHS.text = PlayerPrefs.GetInt("mediumHS", 0).ToString();
        hardHS.text = PlayerPrefs.GetInt("hardHS", 0).ToString();
        advancedHS.text = PlayerPrefs.GetInt("advancedHS", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void loadEasy()
    {

        playButtonSound();
        StartCoroutine(loadEasyC());

    }

    public IEnumerator loadEasyC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MoleMemory", LoadSceneMode.Single);
        PlayerPrefs.SetInt("Easy", 1);
    }

    public void loadMedium()
    {
        playButtonSound();
        StartCoroutine(loadMediumC());
    }

    public IEnumerator loadMediumC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MoleMemory", LoadSceneMode.Single);
        PlayerPrefs.SetInt("Medium", 1);
    }

    public void loadHard()
    {
        playButtonSound();
        StartCoroutine(loadHardC());
    }

    public IEnumerator loadHardC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MoleMemory", LoadSceneMode.Single);
        PlayerPrefs.SetInt("Hard", 1);
    }

    public void loadAdvanced()
    {
        playButtonSound();
        StartCoroutine(loadAdvancedC());
    }

    public IEnumerator loadAdvancedC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MoleMemory", LoadSceneMode.Single);
        PlayerPrefs.SetInt("Advanced", 1);
    }

    public void closeGame()
    {
        Application.Quit();
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
}
