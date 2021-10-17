using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject colourManagerEasy;
    //public GameObject colourManagerMedium;
    //public GameObject colourManagerHard;
    //public GameObject colourManagerAdvanced;

    public GameObject spawner;
    public GameObject UIManager;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Easy") == 1)
        {
            //colourManagerEasy.SetActive(true);
            //colourManagerMedium.SetActive(false);
            //colourManagerHard.SetActive(false);
            //colourManagerAdvanced.SetActive(false);


            spawner.GetComponent<Colour_Manager>().enabled = true;
            spawner.GetComponent<ColourManagerAdvanced>().enabled = false;
            spawner.GetComponent<ColourManagerColourOnly>().enabled = false;
            spawner.GetComponent<ColourManager_HardAdvanced>().enabled = false;

        }
        else if (PlayerPrefs.GetInt("Medium") == 1)
        {
            //colourManagerEasy.SetActive(false);
            //colourManagerMedium.SetActive(true);
            //colourManagerHard.SetActive(false);
            //colourManagerAdvanced.SetActive(false);

            spawner.GetComponent<Colour_Manager>().enabled = false;
            spawner.GetComponent<ColourManagerAdvanced>().enabled = true;
            spawner.GetComponent<ColourManagerColourOnly>().enabled = false;
            spawner.GetComponent<ColourManager_HardAdvanced>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Hard") == 1)
        {
            //colourManagerEasy.SetActive(false);
            //colourManagerMedium.SetActive(false);
            //colourManagerHard.SetActive(true);
            //colourManagerAdvanced.SetActive(false);


            spawner.GetComponent<Colour_Manager>().enabled = false;
            spawner.GetComponent<ColourManagerAdvanced>().enabled = false;
            spawner.GetComponent<ColourManagerColourOnly>().enabled = true;
            spawner.GetComponent<ColourManager_HardAdvanced>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Advanced") == 1)
        {
            //colourManagerEasy.SetActive(false);
            //colourManagerMedium.SetActive(false);
            //colourManagerHard.SetActive(false);
            //colourManagerAdvanced.SetActive(true);


            spawner.GetComponent<Colour_Manager>().enabled = false;
            spawner.GetComponent<ColourManagerAdvanced>().enabled = false;
            spawner.GetComponent<ColourManagerColourOnly>().enabled = false;
            spawner.GetComponent<ColourManager_HardAdvanced>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
