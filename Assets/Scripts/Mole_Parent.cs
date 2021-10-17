using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_Parent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //print(col.gameObject.tag);
        if (col.gameObject.tag == "position_1" || col.gameObject.tag == "position_2" || col.gameObject.tag == "position_3" || col.gameObject.tag == "position_4")
        {
            this.gameObject.transform.GetChild(0).GetComponent<Mole>().mySpawnerLocation = col.gameObject;
        }
    }
}
