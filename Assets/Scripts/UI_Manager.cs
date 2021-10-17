using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{

    public GameObject memorisePanel;
    public GameObject rememberedPanel;
    public Text rememberedText;
    public Text counterText;
    public Text loseRememberedText;

    public AudioManager audioManager;

    public GameObject losePanel;

    public GameObject[] correctCards;
    public GameObject[] incorrectCards;


    public GameObject[] confettiText;

    public Text remember_txt;
    public Text whack_txt;

    public int easyHS = 0;
    public int mediumHS = 0;
    public int hardHS = 0;
    public int advancedHS = 0;

    public GameObject backButton;

    public GameObject blackPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FaceBlackOutSquare(false, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateEasyHS(int points)
    {
        PlayerPrefs.SetInt("easyHS", points);
    }

    public void updateMediumHS(int points)
    {
        PlayerPrefs.SetInt("mediumHS", points);
    }

    public void updateHardHS(int points)
    {
        PlayerPrefs.SetInt("hardHS", points);
    }

    public void updateAdvancedHS(int points)
    {
        PlayerPrefs.SetInt("advancedHS", points);
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


    public void swapPanels()
    {
        if (memorisePanel.activeSelf)
        {
            memorisePanel.SetActive(false);
            rememberedPanel.SetActive(true);
        }
        else if (rememberedPanel.activeSelf)
        {
            memorisePanel.SetActive(true);
            rememberedPanel.SetActive(false);
        }
    }

    public void activateConfettiText()
    {
        confettiText[Random.Range(0,4)].gameObject.SetActive(true);
    }
    public void deactivateConfettiText()
    {
        for (int i = 0; i < confettiText.Length; i++)
        {
            confettiText[i].SetActive(false);
        }
    }

    public void activateCounterText()
    {
        //counterText.gameObject.SetActive(true);
    }
    public void deactivateCounterText()
    {
        counterText.gameObject.SetActive(false);
    }

    public void activateMemorisePanel()
    {
        memorisePanel.SetActive(true);
        rememberedPanel.SetActive(false);
    }

    public void activateRememberedPanel()
    {
        memorisePanel.SetActive(false);
        rememberedPanel.SetActive(true);
    }

    public void activateRememberText()
    {
        remember_txt.gameObject.SetActive(true);
    }


    public void activateWhackText()
    {
        whack_txt.gameObject.SetActive(true);
    }


    public void deactivateRememberText()
    {
        remember_txt.gameObject.SetActive(false);
        rememberedPanel.SetActive(false);
    }


    public void deactivateWhackText()
    {
        whack_txt.gameObject.SetActive(false);
    }

    public void activatelosePanel()
    {
        deactivateBackButton();
        losePanel.SetActive(true);
    }

    public void deactivatelosePanel()
    {
        losePanel.SetActive(false);
    }

    public void deactivateBackButton()
    {
        backButton.SetActive(false);
    }


    public void restartScene()
    {
        StartCoroutine(restartSceneC());
        losePanel.SetActive(false);
        audioManager.playButtonSound();
    }

    public IEnumerator restartSceneC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void loadMainMenu()
    {
        StartCoroutine(loadMainMenuC());
        losePanel.SetActive(false);
        audioManager.playButtonSound();

    }

    public IEnumerator loadMainMenuC()
    {
        StartCoroutine(FaceBlackOutSquare(true, 2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
