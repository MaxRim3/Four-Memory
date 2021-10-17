
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Colour_Manager : MonoBehaviour
{
    public AudioManager audioManager;

    public GameObject[] colours;
    public int numberOfColours = 4;
    public GameObject showPoint;
    public int chosenNumber = 0;
    public GameObject[] spawnArray;

    public GameObject[] curtains;

    public int entryTime;
    public float destroyTime;

    public GameObject UIManager;

    public List<GameObject> activeColours = new List<GameObject>();

    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> spawnPointList = new List<GameObject>();

    public List<GameObject> colourList = new List<GameObject>();
    public List<GameObject> keptList = new List<GameObject>();

    public List<GameObject> chosenList = new List<GameObject>();

    public bool downAfterRise = false;


    public List<int> spawnList = new List<int>();


    public bool spawnerClosed = false;

    public int score = 0;
    public int highScore = 0;
    public int roundHighScore = 0;


    IEnumerator colourShower()
    {
        UIManager.GetComponent<UI_Manager>().activateCounterText();
        UIManager.GetComponent<UI_Manager>().deactivateRememberText();
        UIManager.GetComponent<UI_Manager>().counterText.text = "3";
        audioManager.playCounterSound();
        yield return new WaitForSeconds(1);
        UIManager.GetComponent<UI_Manager>().counterText.text = "2";
        audioManager.playCounterSound();
        yield return new WaitForSeconds(1);
        UIManager.GetComponent<UI_Manager>().counterText.text = "1";
        audioManager.playCounterSound();
        yield return new WaitForSeconds(1);
        UIManager.GetComponent<UI_Manager>().deactivateCounterText();
        UIManager.GetComponent<UI_Manager>().activateMemorisePanel();
        audioManager.playMemoriseBanner();
        yield return new WaitForSeconds(1);
        shuffleSpawnPoints();
        for (int i = 0; i < numberOfColours; i++)
        {
            //yield return new WaitForSeconds(0.5f);
            if (spawnPointList[i].GetComponent<Animator>())
            {
                ////print("Opening curtain");
                resetAllCurtainAnimTriggers(spawnPointList[i]);
                spawnPointList[i].GetComponent<Animator>().SetTrigger("Open");
                //spawnPointList[i].GetComponent<Spawner_Controller>().closed = true;

            }
            GameObject newColour = Instantiate(colourList[i], spawnPointList[i].transform.position, spawnPointList[i].transform.rotation) as GameObject;
            audioManager.playMoleAppear();
            int rotRef = Random.Range(0, 2);
            if (rotRef == 1)
            {
                newColour.gameObject.transform.rotation = Quaternion.Euler(newColour.transform.rotation.x, newColour.transform.rotation.y + 180, newColour.transform.rotation.z);


            }
            if (newColour.gameObject.transform.GetChild(0).GetComponent<Mole>().accessory)
            {
                newColour.gameObject.transform.GetChild(0).GetComponent<Mole>().accessory.SetActive(true);
            }

            float animUpTime = newColour.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(animUpTime - 0.2f);
            if (newColour.transform.childCount > 0)
            {
                if (newColour.transform.GetChild(0).GetComponent<Mole>())
                {
                    //if (downAfterRise)
                    {
                        newColour.transform.GetChild(0).GetComponent<Mole>().down();
                    }
                    if (spawnPointList[i].GetComponent<Animator>())
                    {
                        ////print("Closing curtain");
                        resetAllCurtainAnimTriggers(spawnPointList[i]);
                        spawnPointList[i].GetComponent<Animator>().SetTrigger("Close");
                        //spawnPointList[i].GetComponent<Spawner_Controller>().closed = true;

                    }
                    StartCoroutine(moleDownSound());
                    ////print("Telling mole to go down");
                }
            }
            yield return new WaitForSeconds(1.5f);
            Destroy(newColour.gameObject);
        }

        for (int i = 0; i < numberOfColours; i++)
        {
            keptList.Add(colourList[i]);
        }

        //UIManager.GetComponent<UI_Manager>().activateWhackText();
        yield return new WaitForSeconds(1);
        //UIManager.GetComponent<UI_Manager>().deactivateWhackText();

       StartCoroutine(revealColours(0));
    }

    public void shuffleSpawnPoints()
    {
        spawnPointList.Clear();
        shuffleArray(spawnArray);
        for (int j = 0; j < (numberOfColours / 4) + 1; j++)
        {
            shuffleArray(spawnArray);
            for (int i = 0; i < 4; i++)
            {
                spawnPointList.Add(spawnArray[i]);
            }
        }
    }

    public void resetAllCurtainAnimTriggers(GameObject curtain)
    {
        if (curtain.GetComponent<Animator>())
        {
            curtain.GetComponent<Animator>().ResetTrigger("Open");
            curtain.GetComponent<Animator>().ResetTrigger("Close");
            curtain.GetComponent<Animator>().ResetTrigger("CloseFull");
            curtain.GetComponent<Animator>().ResetTrigger("Idle");
        }
    }
   


    public IEnumerator revealColours(float time) //spawn in random spawnpoints not random element from list -- one to remember
    {
        yield return new WaitForSeconds(time);
        UIManager.GetComponent<UI_Manager>().activateRememberedPanel();
        audioManager.playBackgroundMusicOne();
        StartCoroutine(semiCloseCurtains());

        shuffleColoursIn(4 - (chosenNumber % 4)); //the first colour may be a colour already added

        for (int i = 0; i < spawnArray.Length; i++)
        {
            spawnPoints.Add(spawnArray[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, spawnPoints.Count);
            Transform pos = spawnPointList[chosenNumber + i].gameObject.transform;

            if (colourList[chosenNumber + i])                                                //instantiate 4 colours but start instantiation from colourList[chosenNumber]
            {                                                                 //add colours based on chosenNumber % 4,  5 % 4 = 1 so you need 3 more colors, 6%4 = 2 so you need 2 more colors
                if (spawnPointList[chosenNumber + i].GetComponent<Animator>())
                {
                    //resetAllCurtainAnimTriggers(spawnPointList[chosenNumber + i]);
                    spawnPointList[chosenNumber + i].gameObject.GetComponent<Animator>().SetTrigger("Open");
                    //spawnPointList[i].GetComponent<Spawner_Controller>().closed = false;

                }
                    GameObject c = Instantiate(colourList[chosenNumber + i], pos.transform.position, pos.transform.rotation) as GameObject;
                    c.transform.GetChild(0).GetComponent<Mole>().idle();


                int rotRef = Random.Range(0, 2);
                if (rotRef == 1)
                {
                    c.gameObject.transform.rotation = Quaternion.Euler(c.transform.rotation.x, c.transform.rotation.y + 180, c.transform.rotation.z);


                }


                activeColours.Add(c);
                spawnPoints.RemoveAt(index);  //need to put the spawnpoints back in

             
            }

            else
            {
                ////print("no more to instantiate");
            }

       
        }
        audioManager.playMoleAppear();
        //enableAllActiveColliders();


    }

    public IEnumerator semiCloseCurtains()
    {

        yield return new WaitForSeconds(4);
        for (int i = 0; i < 4; i++)
        {
            if (chosenNumber + i < spawnPointList.Count)
            {
                if (spawnPointList[chosenNumber + i])
                {
                    if (spawnPointList[chosenNumber + i].GetComponent<Animator>())
                    {
                        if (!spawnPointList[chosenNumber + i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close") && !spawnPointList[chosenNumber + i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CloseFull"))
                        {
                            resetAllCurtainAnimTriggers(spawnPointList[chosenNumber + i]);
                            spawnPointList[chosenNumber + i].gameObject.GetComponent<Animator>().SetTrigger("Idle");
                        }
                    }
                }
            }
        }
    }

    public void shuffleColoursIn(int numOfCol) // need to shuffle in 4 colours each time then shuffle array again and get another 4
    {
        for (int i = 0; i < (numOfCol / 4) + 1; i++)
        {
            shuffleArray(colours);
            for (int j = 0; j < 4; j++)
            {
                colourList.Add(colours[j]);
            }
        }
    }

    public void shuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    public void startNewRound()
    {
        StartCoroutine(startNewRoundEnumerator());
    }

    public IEnumerator startNewRoundEnumerator()
    {

        for (int i = 0; i < activeColours.Count; i++)
        {
            activeColours[i].gameObject.transform.GetChild(0).GetComponent<Mole>().resetAllAnims();
            activeColours[i].gameObject.transform.GetChild(0).GetComponent<Mole>().down();
        }


        yield return new WaitForSeconds(1f);
        
        

        for (int i = 0; i < 4; i++)
        {
            //spawnArray[i].GetComponent<Animator>().ResetTrigger("CloseFull");
            if (chosenNumber != 0)
            {
                if (spawnArray[i].GetComponent<Animator>())
                {
                    if (spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (!spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close") && !spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CloseFull"))
                        {
                            ////print("closing" + spawnArray[i]);
                            resetAllCurtainAnimTriggers(spawnArray[i]);
                            spawnArray[i].GetComponent<Animator>().SetTrigger("CloseFull");
                            //spawnArray[i].GetComponent<Animator>().ResetTrigger("CloseFull");
                        }

                    }

                    if (spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open"))
                    {
                        if (!spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close") && !spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CloseFull"))
                        {
                            ////print("closing" + spawnArray[i]);
                            resetAllCurtainAnimTriggers(spawnArray[i]);
                            spawnArray[i].GetComponent<Animator>().SetTrigger("Close");
                            //spawnArray[i].GetComponent<Animator>().ResetTrigger("CloseFull");
                        }
                    }
                }
            }
        }

        //UIManager.GetComponent<UI_Manager>().activateRememberText();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < activeColours.Count; i++)
        {
            Destroy(activeColours[i].gameObject);

            
        }
        activeColours.Clear();

        yield return new WaitForSeconds(2);
        //UIManager.GetComponent<UI_Manager>().deactivateRememberText();

        chosenNumber = 0;
        spawnList.Clear();
        keptList.Clear();
        colourList.Clear();
        shuffleColoursIn(numberOfColours);
        StartCoroutine(colourShower());
        UIManager.GetComponent<UI_Manager>().deactivateConfettiText();
    }


    public void spawnNext()
    {
        Transform pos = spawnPointList[numberOfColours + chosenNumber].gameObject.transform;
        GameObject c = Instantiate(colourList[numberOfColours + chosenNumber], pos.transform.position, pos.transform.rotation) as GameObject;
    }

    public IEnumerator moleDown(GameObject gameObject, float time, string animName)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.GetChild(0).GetComponent<Mole>().resetAllAnims();
        gameObject.transform.GetChild(0).GetComponent<Mole>().down();
        
    }

    public IEnumerator curtainCloseFull(GameObject gameObject, Animator anim)
    {
        yield return new WaitForSeconds(destroyTime / 2);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Close") && !anim.GetCurrentAnimatorStateInfo(0).IsName("CloseFull"))
            {
                resetAllCurtainAnimTriggers(anim.gameObject);
                anim.SetTrigger("CloseFull");
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Close") && !anim.GetCurrentAnimatorStateInfo(0).IsName("CloseFull"))
            {
                resetAllCurtainAnimTriggers(anim.gameObject);
                anim.SetTrigger("Close");
            }
        }
        //gameObject.transform.GetChild(0).GetComponent<Mole>().mySpawnerLocation.GetComponent<Spawner_Controller>().closed = true;
        yield return new WaitForSeconds(destroyTime / 2);
        Destroy(gameObject);
    }

    public IEnumerator moleDownSound()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.playMoleDisappear();
    }

    public IEnumerator moleDownSoundAfterHit()
    {
        yield return new WaitForSeconds(1f);
        audioManager.playMoleDisappear();
    }

    public void disableAllActiveColliders()
    {
        for (int i = 0; i < activeColours.Count; i ++)
        {
            activeColours[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void enableAllActiveColliders()
    {
        for (int i = 0; i < activeColours.Count - 1; i++)
        {
            activeColours[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    public IEnumerator checkChosen(GameObject cColour)
    {
        yield return new WaitForSeconds(0);

        //only run this if you have a hit animation in animator
        if (cColour.transform.childCount > 0)
        {
            if (cColour.transform.GetChild(0).GetComponent<Mole>())
            {
                cColour.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
                audioManager.playHit();
            }
        }

        if (string.Equals(cColour.name, keptList[chosenNumber].name + "(Clone)"))
        {
            ////print("the colour is correct");


            if (cColour.transform.childCount > 0)
            {
                if (cColour.transform.GetChild(0).GetComponent<Mole>())
                {
                    float animHitTime = cColour.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
                    StartCoroutine(moleDown(cColour, 0.2f, "down"));
                    cColour.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(moleDownSoundAfterHit());
   
                    ////print("Telling mole to go down");
                }
            }


            //yield return new WaitForSeconds(destroyTime / 2);
            if (cColour.transform.childCount > 0)
            {
                if (cColour.transform.GetChild(0).GetComponent<Mole>())
                {
                    if (cColour.transform.GetChild(0).GetComponent<Mole>().mySpawnerLocation)
                    {
                        Animator anim = cColour.transform.GetChild(0).GetComponent<Mole>().mySpawnerLocation.GetComponent<Animator>();
                        StartCoroutine(curtainCloseFull(cColour, anim));
                    }
                    
                }

               
            }


            //yield return new WaitForSeconds(destroyTime / 2);

            //Destroy(cColour);
            activeColours.Remove(cColour);
            StartCoroutine(destroyOverTime(cColour, 2f));
            chosenNumber++;
            score = chosenNumber;
            if (score > highScore)
            {
                highScore = score;
            }
            if (score > roundHighScore)
            {
                roundHighScore = score;
            }

            

            if (chosenNumber == keptList.Count)
            {
                //if (chosenNumber != 0)
                //{
                //    ////print("closing curtains from idle");
                //    for (int i = 0; i < 4; i++)
                //    {
                //        if (!spawnArray[i].GetComponent<Spawner_Controller>().closed)
                //        if (spawnArray[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                //        {

                //            //print("closing" + spawnArray[i]);
                //            spawnArray[i].GetComponent<Animator>().SetTrigger("CloseFull");
                //            spawnArray[i].GetComponent<Animator>().ResetTrigger("CloseFull");

                //        }
                //    }
                //}
                
                numberOfColours++;
                UIManager.GetComponent<UI_Manager>().activateConfettiText();
                audioManager.playCorrectSound();
                audioManager.playBackgroundMusicTwo();

                startNewRound();
            }


            else if (numberOfColours > 4 && chosenNumber%4 == 0)
            {
               
                StartCoroutine(revealColours(3));
            }
        }
        else
        {
            //print("the colour is incorrect");
            //print(cColour + cColour.name);
            ////print(keptList[chosenNumber] + keptList[chosenNumber].name);
            cColour.gameObject.transform.GetChild(0).GetComponent<Mole>().idle();

            UIManager.GetComponent<UI_Manager>().activatelosePanel();
            disableAllActiveColliders();
            UIManager.GetComponent<UI_Manager>().loseRememberedText.text = roundHighScore.ToString();
            UIManager.GetComponent<UI_Manager>().rememberedPanel.SetActive(false);
            audioManager.playWrongSound();

            switch(cColour.name)
            {
                case "Baker_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[0].SetActive(true);
                    break;
                case "Farmer_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[1].SetActive(true);
                    break;
                case "Jazz_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[2].SetActive(true);
                    break;
                case "Child_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[3].SetActive(true);
                    break;
                case "PoshLady_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[4].SetActive(true);
                    break;
                case "Zoo_Parent(Clone)":
                    UIManager.GetComponent<UI_Manager>().incorrectCards[5].SetActive(true);
                    break;
            }

            switch (keptList[chosenNumber].name)
            {
                case "Baker_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[0].SetActive(true);
                    break;
                case "Farmer_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[1].SetActive(true);
                    break;
                case "Jazz_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[2].SetActive(true);
                    break;
                case "Child_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[3].SetActive(true);
                    break;
                case "PoshLady_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[4].SetActive(true);
                    break;
                case "Zoo_Parent":
                    UIManager.GetComponent<UI_Manager>().correctCards[5].SetActive(true);
                    break;
            }
        }
    }

    public IEnumerator destroyOverTime(GameObject go,float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(go);
    }

    public void Update()
    {
        UIManager.GetComponent<UI_Manager>().rememberedText.text = chosenNumber.ToString();

        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = -layerMask;
        if (Input.GetMouseButtonDown(0) && activeColours.Count > 0)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, layerMask);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                if (hit.collider.tag == "Green")
                {
                    StartCoroutine(checkChosen(hit.collider.gameObject));
                }
            }

        }
    }

    public void Start()
    {
        startNewRound();
        highScore = PlayerPrefs.GetInt("easyHS", 0);
    }
    public void OnDisable()
    {
        PlayerPrefs.SetInt("easyHS", highScore);
        PlayerPrefs.Save();
    }

}
