using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(goToEntry());
    }

    public IEnumerator goToEntry()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Entry", LoadSceneMode.Single);
    }

}
