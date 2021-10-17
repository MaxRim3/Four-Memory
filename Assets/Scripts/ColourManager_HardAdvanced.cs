
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameMoveHA
{
    public int[] positions;
    public int WiningPosition;
}

public static class SpawnSettingsHA
{
    public static GameObject[] spawnArray;
    public static GameObject[] listToSpawn;
    public static GameObject[] spawnedList;

}

public class ColourManager_HardAdvanced : MonoBehaviour
{
    public GameObject[] spawnArray; //transforms for spawn
    public GameObject[] colourArray;
    public List<GameObject> listToSpawn = new List<GameObject>();
    public List<GameObject> listOfSpawnTransforms = new List<GameObject>();
    public AudioManager audioManager;

    public Text correctMoleText;
    public Text wrongMoleText;

    public int coinFlip;


    public List<Vector3> defaultSpawnTransforms = new List<Vector3>();


    public List<GameObject> spawnedList = new List<GameObject>(4);


    public int levelNum;
    public bool goingDown;
    public int rememberedCount;
    public GameObject UIManager;

    List<GameMoveHA> _moves;
    GameMoveHA _lastMove;
    int _activemove;

    public int score = 0;
    public int highScore = 0;
    public int roundHighScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("advancedHS", 0);

        SpawnSettingsHA.spawnArray = spawnArray;
        SpawnSettingsHA.listToSpawn = colourArray;
        SpawnSettingsHA.spawnedList = new GameObject[4];

        for (int i = 0; i < spawnArray.Length; i++)
        {
            spawnArray[i].layer = 0;
        }

        for (int i = 0; i < SpawnSettingsHA.spawnArray.Length; i++)
        {
            defaultSpawnTransforms.Add(SpawnSettingsHA.spawnArray[i].transform.position);
        }

        _lastMove = new GameMoveHA();
        _lastMove.positions = new int[] { 99, 99, 99, 99 };
        _lastMove.WiningPosition = 0;

        _randomizeMoves(levelNum);

        for (int i = 0; i < spawnArray.Length; i++)
        {
            spawnArray[i].GetComponent<BoxCollider2D>().enabled = false;
        }


        StartCoroutine(startGame());
        //StartCoroutine(showColours());

    }

    public IEnumerator startGame()
    {

        yield return new WaitForSeconds(2);
        StartCoroutine(_displayMoves());

    }
    //randomize winning position 0-3
    //
    void _randomizeMoves(int level)
    {
        GameMoveHA move;
        //int numOffEmptyRemoved = 0;
        _moves = new List<GameMoveHA>(level);
        for (int i = 0; i < level; i++)
        {
            move = new GameMoveHA
            {
                positions = new int[4],
                WiningPosition = Random.Range(0, 4)
            };

            ////print(move.WiningPosition);

            List<int> list = new List<int>();

            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            //list.Add(6);



            if (move.WiningPosition != 0)
            {
                int index = Random.Range(0, list.Count);
                int t = list[index];

                move.positions[0] = t;
                if (t != 6)
                {
                    list.RemoveAt(index);
                }
            }
            else
            {
                int index = Random.Range(0, list.Count - 1);
                int t = list[index];

                move.positions[0] = t;

                list.RemoveAt(index);
            }


            if (move.WiningPosition != 1)
            {
                int indexTwo = Random.Range(0, list.Count);
                int j = list[indexTwo];

                move.positions[1] = j;

                if (j != 6)
                {
                    list.RemoveAt(indexTwo);
                }
            }
            else
            {
                int indexTwo = Random.Range(0, list.Count - 1);
                int j = list[indexTwo];

                move.positions[1] = j;

                list.RemoveAt(indexTwo);
            }

            if (move.WiningPosition != 2)
            {
                int indexThree = Random.Range(0, list.Count);

                int k = list[indexThree];

                move.positions[2] = k;

                if (k != 6)
                {
                    list.RemoveAt(indexThree);
                }
            }
            else
            {
                int indexThree = Random.Range(0, list.Count - 1);
                int k = list[indexThree];

                move.positions[2] = k;

                list.RemoveAt(indexThree);
            }


            if (move.WiningPosition != 3)
            {
                int indexFour = Random.Range(0, list.Count);


                int s = list[indexFour];

                move.positions[3] = s;

                if (s != 6)
                {
                    list.RemoveAt(indexFour);
                }
            }
            else
            {
                int indexFour = Random.Range(0, list.Count - 1);



                int s = list[indexFour];

                move.positions[3] = s;

                list.RemoveAt(indexFour);
            }

            _moves.Add(move);
        }
    }

    IEnumerator _displayMoves()
    {
        
        UIManager.GetComponent<UI_Manager>().activateCounterText();
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
        UIManager.GetComponent<UI_Manager>().deactivateRememberText();
        UIManager.GetComponent<UI_Manager>().activateMemorisePanel();
        audioManager.playMemoriseBanner();
       
        goingDown = false;
        yield return new WaitForSeconds(2);
        foreach (var move in _moves)
        {
            StartCoroutine(SpawnElementUpDown(move.positions[move.WiningPosition], move.WiningPosition));
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(DestroyElement(move.WiningPosition));
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(SpawnMove(_moves[0], 0f));

        yield return null;
    }


    public IEnumerator SpawnMove(GameMoveHA Move, float timer) //show round
    {

        goingDown = false;
        bool swapMole = false;

        for (int i = 0; i < SpawnSettingsHA.spawnArray.Length; i++)
        {
            Vector3 pos = defaultSpawnTransforms[i];
            SpawnSettingsHA.spawnArray[i].transform.position = pos;
            //spawnArray[i].transform.position = defaultSpawnTransforms[i].transform.position;
        }

        coinFlip = Random.Range(0, 2);

        if (coinFlip == 1)
        {
            //print("1");
            shuffleArrayObjectPositions(SpawnSettingsHA.spawnArray); //same mole in different position
            //swapMole = true;
        }
        else if (coinFlip == 0)
        {
            ////print("0");
            swapMole = true; //same position different mole

        }
        //SpawnSettingsHA.listToSpawn[_moves[_activemove].WiningPosition] = colourArray[3];

        for (int i = 0; i < spawnArray.Length; i++)
        {
            spawnArray[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        UIManager.GetComponent<UI_Manager>().activateRememberedPanel(); //pulled this out check if problems
        audioManager.playRememberBanner();
        audioManager.playBackgroundMusicOne();

        for (int i = 0; i < 4; i++)
        {
            //if (Move.positions[i] < 1)
            {
                //UIManager.GetComponent<UI_Manager>().activateRememberText();
                
            }
            if (Move.positions[i] != _lastMove.positions[i])
                StartCoroutine(DestroyElement(i));
            //possible issue
        }

        yield return new WaitForSeconds(0);
        //int molesRemoved = 0;
        if (swapMole)
        {
           
            for (int i = 0; i < 4; i++)
            {
                //if (Move.positions[i] < 6)
                {
                    if (i == _moves[_activemove].WiningPosition)
                    {
                        //molesRemoved++;
                        List<int> numList = new List<int>();
                        numList.Add(0);
                        numList.Add(1);
                        numList.Add(2);
                        numList.Add(3);
                        numList.Add(4);
                        numList.Add(5);


                        //for (int k = 0; k < numList.Count; k++)
                        //{
                        //    for (int j = 0; j < Move.positions.Length; j++)
                        //    {
                        //        if (numList[k] == Move.positions[j])
                        //        {
                        //            numList.RemoveAt(i);
                        //        }
                        //    }
                        //}

                        for (int x = 0; x < Move.positions.Length; x++)
                        {
                            numList.Remove(Move.positions[x]);
                        }

                        int spawnNum = numList[Random.Range(0, numList.Count)];


                        SpawnElement(spawnNum, i);
                    }
                    else
                    {
                        SpawnElement(Move.positions[i], i);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                //if (Move.positions[i] < 6)
                {
                    SpawnElement(Move.positions[i], i);
                }
            }
        }
    }

    public void SpawnElement(int MoleID, int position)
    {
        if (SpawnSettingsHA.spawnedList[position] != null)
        {
            Destroy(SpawnSettingsHA.spawnedList[position].gameObject);
        }

        GameObject newMole = Instantiate(SpawnSettingsHA.listToSpawn[MoleID].gameObject, SpawnSettingsHA.spawnArray[position].transform.position, transform.rotation) as GameObject;
        audioManager.playMoleAppear();
        if (newMole.GetComponent<BoxCollider2D>())
        {
            newMole.GetComponent<BoxCollider2D>().enabled = false;
        }
        SpawnSettingsHA.spawnArray[position].GetComponent<Spawner_Controller>().currentMole = newMole;

        if (newMole.transform.childCount > 0)
        {
            if (newMole.transform.GetChild(0).GetComponent<Mole>())
            {
                newMole.transform.GetChild(0).GetComponent<Mole>().up();
                newMole.transform.GetChild(0).GetComponent<Mole>().idle();
                ////print("Telling mole to go up");
            }
        }
        SpawnSettingsHA.spawnedList[position] = newMole;
    }

    public IEnumerator SpawnElementUpDown(int MoleID, int position)
    {
        if (SpawnSettingsHA.spawnedList[position] != null)
        {
            Destroy(SpawnSettingsHA.spawnedList[position].gameObject);
        }

        GameObject newMole = Instantiate(SpawnSettingsHA.listToSpawn[MoleID].gameObject, SpawnSettingsHA.spawnArray[position].transform.position, transform.rotation) as GameObject;

        if (newMole.transform.childCount > 0)
        {
            if (newMole.transform.GetChild(0).GetComponent<Mole>())
            {
                newMole.transform.GetChild(0).GetComponent<Mole>().up();
                ////print("Telling mole to go up");
                audioManager.playMoleAppear();
            }
        }
        yield return new WaitForSeconds(1.1f);
        if (newMole.transform.childCount > 0)
        {
            if (newMole.transform.GetChild(0).GetComponent<Mole>())
            {
                newMole.transform.GetChild(0).GetComponent<Mole>().down();
                ////print("Telling mole to go down");
                StartCoroutine(moleDownSound());
            }
        }
        SpawnSettingsHA.spawnedList[position] = newMole;
    }

    public IEnumerator DestroyElement(int position)
    {


        if (SpawnSettingsHA.spawnedList[position] == null)
            yield break;

        GameObject mole = SpawnSettingsHA.spawnedList[position];

        if (mole.transform.childCount > 0)
        {
            if (mole.transform.GetChild(0).GetComponent<Mole>())
            {
                mole.transform.GetChild(0).GetComponent<Mole>().down();
                ////print("Telling mole to go down");
                //StartCoroutine(moleDownSound());
            }
        }


        yield return new WaitForSeconds(0);

        Destroy(mole.gameObject);
        SpawnSettingsHA.spawnedList[position] = null;
    }


    public void shuffleArrayObjectPositions(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            var tmp = array[i].transform.position;
            array[i].transform.position = array[j].transform.position;
            array[j].transform.position = tmp;
        }
    }

    IEnumerator _checkMove(int position)
    {

        for (int i = 0; i < spawnArray.Length; i++)
        {
            spawnArray[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        GameObject mole = SpawnSettingsHA.spawnedList[position];

        if (mole.transform.childCount > 0)
        {
            if (mole.transform.GetChild(0).GetComponent<Mole>())
            {
                mole.transform.GetChild(0).GetComponent<Mole>().hit();
                ////print("Telling mole to get hit");
                audioManager.playHit();
            }
        }

        yield return new WaitForSeconds(0.4f);

        if (_moves[_activemove].WiningPosition == position)
        {
            if (_activemove + 1 == levelNum)
            {
                UIManager.GetComponent<UI_Manager>().activateConfettiText();
                audioManager.playCorrectSound();
                audioManager.playBackgroundMusicTwo();
            }
            if (mole.transform.childCount > 0)
            {
                if (mole.transform.GetChild(0).GetComponent<Mole>())
                {
                    //mole.transform.GetChild(0).GetComponent<Mole>().down();
                    ////print("Telling mole to go down");
                    //yield return new WaitForSeconds(0.2f);
                    sendAllMolesDown();
                    rememberedCount++;
                    StartCoroutine(moleDownSound());
                    score = rememberedCount;
                    if (score > highScore)
                    {
                        highScore = score;
                    }
                    if (score > roundHighScore)
                    {
                        roundHighScore = score;
                    }


                }
            }

            yield return new WaitForSeconds(2f);

            StartCoroutine(DestroyElement(position));
            ////print("correct color");
            _lastMove = _moves[_activemove];
            _activemove++;

            if (_activemove < levelNum)
            {
                StartCoroutine(SpawnMove(_moves[_activemove], 0f));
            }
            else
            {
                rememberedCount = 0;
                _activemove = 0;
                levelNum++;
                _randomizeMoves(levelNum);
                StartCoroutine(_displayMoves());
            }
        }
        else
        {
            ////print("incorrect color");
            losePanel(spawnArray[position].GetComponent<Spawner_Controller>().currentMole, spawnArray[_moves[_activemove].WiningPosition].GetComponent<Spawner_Controller>().currentMole);
        }
    }

    public void losePanel(GameObject wrongMole, GameObject correctMole)
    {
        disableAllActiveColliders();
        UIManager.GetComponent<UI_Manager>().activatelosePanel();
        UIManager.GetComponent<UI_Manager>().loseRememberedText.text = roundHighScore.ToString();
        UIManager.GetComponent<UI_Manager>().rememberedPanel.SetActive(false);
        if (coinFlip == 1)
        {
            correctMoleText.text = "I've moved to a different hole!";
            wrongMoleText.text = "Oops, wrong choice. Sometimes we move or take each others place!";
            wrongMoleText.fontSize = 12;
        }
        else if (coinFlip == 0)
        {
            correctMoleText.text = "I've taken the place of my friend! I was the correct choice.";
            correctMoleText.fontSize = 12;
            wrongMoleText.text = "Oops, wrong choice. Sometimes we move or take each others place";
            wrongMoleText.fontSize = 12;
        }
        audioManager.playWrongSound();
        switch (wrongMole.name)
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

        switch (correctMole.name)
        {
            case "Baker_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[0].SetActive(true);
                break;
            case "Farmer_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[1].SetActive(true);
                break;
            case "Jazz_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[2].SetActive(true);
                break;
            case "Child_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[3].SetActive(true);
                break;
            case "PoshLady_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[4].SetActive(true);
                break;
            case "Zoo_Parent(Clone)":
                UIManager.GetComponent<UI_Manager>().correctCards[5].SetActive(true);
                break;
        }
    }

    public IEnumerator moleDownSound()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.playMoleDisappear();
    }

    public void sendAllMolesDown()
    {
        for (int i = 0; i < SpawnSettingsHA.spawnedList.Length; i++)
        {
            GameObject mole = SpawnSettingsHA.spawnedList[i].gameObject;
            if (mole.transform.childCount > 0)
            {
                if (mole.transform.GetChild(0).GetComponent<Mole>())
                {
                    mole.transform.GetChild(0).GetComponent<Mole>().resetAllAnims();
                    mole.transform.GetChild(0).GetComponent<Mole>().down();
                }
            }
        }
        goingDown = true;
        //for (int i = 0; i < SpawnSettingsHA.spawnedList.Length; i++)
        //{
        //    if (SpawnSettingsHA.spawnedList[i] != null)
        //    {
        //        GameObject mole = SpawnSettingsHA.spawnedList[i].gameObject;
        //        if (mole.transform.childCount > 0)
        //        {
        //            if (mole.transform.GetChild(0).GetComponent<Mole>())
        //            {
        //                Animator animator = mole.transform.GetChild(0).GetComponent<Animator>();
        //                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Baker_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Farmer_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Jazz_Idle")
        //                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Child_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Zoo_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("PL_Idle"))
        //                {
        //                    animator.speed = 1.4f;
        //                }
        //                else
        //                {
        //                    animator.speed = 1;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public void checkHitPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {

            switch (hit.collider.gameObject.tag)
            {
                case "position_1":
                    StartCoroutine(_checkMove(0));
                    break;
                case "position_2":
                    StartCoroutine(_checkMove(1));
                    break;
                case "position_3":
                    StartCoroutine(_checkMove(2));
                    break;
                case "position_4":
                    StartCoroutine(_checkMove(3));
                    break;
                default:
                    break;
            }
        }
    }

    public void Update()
    {
        UIManager.GetComponent<UI_Manager>().rememberedText.text = rememberedCount.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            checkHitPosition();

        }

        //if (goingDown)
        //{
        //    for (int i = 0; i < SpawnSettingsHA.spawnedList.Length; i++)
        //    {
        //        if (SpawnSettingsHA.spawnedList[i] != null)
        //        {
        //            GameObject mole = SpawnSettingsHA.spawnedList[i].gameObject;
        //            if (mole.transform.childCount > 0)
        //            {
        //                if (mole.transform.GetChild(0).GetComponent<Mole>())
        //                {
        //                    Animator animator = mole.transform.GetChild(0).GetComponent<Animator>();
        //                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Baker_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Farmer_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Jazz_Idle")
        //                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Child_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Zoo_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("PL_Idle"))
        //                    {
        //                        animator.speed = 3;
        //                    }
        //                    else
        //                    {
        //                        animator.speed = 1;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("advancedHS", highScore);
        PlayerPrefs.Save();
    }

    public void disableAllActiveColliders()
    {
        for (int i = 0; i < SpawnSettingsHA.spawnedList.Length; i++)
        {
            if (SpawnSettingsHA.spawnedList[i].gameObject.GetComponent<BoxCollider2D>())
            {
                SpawnSettingsHA.spawnedList[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}



////DEPRACATED UNDER ---------------------------------------------------------------------------------------------------------------------------


//public int chosenNumber = 0;
//public void addColours()
//{
//    shuffleColours();
//    //for (int i = 0; i < chosenNumber; i++)
//    {
//        listToSpawn.Add(colourArray[0]);
//    }

//}


//public void addSpawnPoint()
//{
//    shuffleArray(spawnArray);
//    listOfSpawnTransforms.Add(spawnArray[0]);
//}

//public IEnumerator showColours()
//{


//    for (int i = 0; i < colourArray.Length; i++)
//    {
//        addColours();
//        addSpawnPoint();
//        GameObject newMole = Instantiate(listToSpawn[i].gameObject, spawnArray[i].transform.position, transform.rotation) as GameObject;
//        spawnedList.Add(newMole);
//    }

//    for (int i = 0; i < listToSpawn.Count; i++)
//    {
//        for (int j = 0; j < i; j++)
//        {
//            if (spawnedList[i] != spawnedList[j] && spawnedList[i].transform.position == spawnedList[j].transform.position)
//            {
//                ////print(spawnedList[i].transform.position + spawnedList[i].name);
//                ////print(spawnedList[j].transform.position + spawnedList[j].name);
//                //if (i != j)
//                {
//                    if (spawnedList[i].GetComponent<Colour>().timesDeactivated < 1)
//                    {
//                        spawnedList[i].GetComponent<Colour>().deactivate();
//                    }
//                }
//            }
//        }
//    }

//    yield return null;

//}

//public void shuffleColours()
//{
//    shuffleArray(colourArray);
//}

//public void shuffleArray(GameObject[] array)
//{
//    for (int i = array.Length - 1; i > 0; i--)
//    {
//        int j = Random.Range(0, i);
//        var tmp = array[i];
//        array[i] = array[j];
//        array[j] = tmp;
//    }
//}


//public void checkChosen(GameObject cColour)
//{

//    if (string.Equals(cColour.name, listToSpawn[chosenNumber].name + "(Clone)"))
//    {
//        //print("the colour is correct");
//        for (int i = 0; i < listToSpawn.Count; i++)
//        {
//            if (spawnedList[i].gameObject.transform.position == cColour.gameObject.transform.position && spawnedList[i].gameObject != cColour.gameObject)
//            {
//                if (spawnedList[i].GetComponent<Colour>().timesDeactivated == 1)
//                {
//                    spawnedList[i].gameObject.SetActive(true);
//                }
//            }
//        }
//        //Destroy(cColour);
//        cColour.GetComponent<Colour>().deactivate();
//        cColour.GetComponent<Colour>().timesDeactivated++;
//        chosenNumber++;

//        /*for (int i = 0; i < listToSpawn.Count; i++)
//        {
//            for (int j = 0; j < i; j++)
//            {
//                if (spawnedList[i] != spawnedList[j] && spawnedList[i].transform.position == spawnedList[j].transform.position)
//                {
//                    //print(spawnedList[i].transform.position + spawnedList[i].name);
//                    //print(spawnedList[j].transform.position + spawnedList[j].name);
//                    //if (i != j)
//                    {
//                        if (spawnedList[i].GetComponent<Colour>().timesDeactivated > 1)
//                        {
//                            spawnedList[i].GetComponent<Colour>().timesDeactivated--;
//                            spawnedList[i].GetComponent<Colour>().deactivate();
//                        }
//                    }
//                }
//            }
//        }*/
//        //activeColours.Remove(cColour);
//        //chosenNumber++;
//        //if (numberOfColours > 4)
//        //{
//        //    spawnNext();
//        //}

//        //if (chosenNumber == keptList.Count)
//        //{
//        //    numberOfColours++;
//        //    startNewRound();
//        //}

//        //else if (chosenNumber == numberOfColours)
//        //{
//        //revealColours();
//        //}

//        //else if (numberOfColours > 4 && chosenNumber % 4 == 0)
//        //{

//        //    revealColours();
//        //}


//    }
//    else
//    {
//        //print("the colour is incorrect");
//        //print(cColour + cColour.name);
//        //print(listToSpawn[chosenNumber] + listToSpawn[chosenNumber].name);
//    }
//}
