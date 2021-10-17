using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{
    public int timesDeactivated;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(destroyMe());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyMe()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public void deactivate()
    {
        this.gameObject.SetActive(false);
        timesDeactivated++;
    }
}
